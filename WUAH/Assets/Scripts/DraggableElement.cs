using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DraggableElement : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject _DraggedScrap;
    private UIScrapData _UIScrapInfo;
    private Vector2 _InitialScrapPosition;

    /** Register the active element being dragged */
    public void OnBeginDrag(PointerEventData eventData) 
    {
        GameObject rayCastHitObj = eventData.pointerCurrentRaycast.gameObject;

        _DraggedScrap = rayCastHitObj;
        _InitialScrapPosition = rayCastHitObj.transform.position;
        _UIScrapInfo = _DraggedScrap.GetComponent<UIScrapData>();

        PlayerController.Instance.TestScrapData = _UIScrapInfo.PaintingScrapData;
    }

    /** Move element across the screen based on mouse position */
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransform canvasTransform = UIManager.Instance.CanvasRectTransform;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, eventData.position, Camera.main, out pos);

        _DraggedScrap.transform.position = canvasTransform.TransformPoint(pos);
    }

    /** Set the texture of the dragged element at the given  */
    public void OnEndDrag(PointerEventData eventData)
    {
        // Raycast to see if we hit a PaintScrap
        Ray scrapRay = PlayerController.Instance.Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(scrapRay, out RaycastHit scrapHit) == true)
        {
            PaintingScrap paintingScrap = scrapHit.collider.GetComponent<PaintingScrap>();

            if (paintingScrap != null)
            {
                // Remove the current Scrap from the player's inventory
                ScrapData usedScrap = _UIScrapInfo.PaintingScrapData;
                List<ScrapData> availableScraps = PaintingScrapManager.Instance.AvailableScraps;
                
                if (paintingScrap.Scrap != null)
                {
                    availableScraps.Add(paintingScrap.Scrap);
                    ScrapUIManager.Instance.GetUIScrapInfoWithScrapData(paintingScrap.Scrap).gameObject.SetActive(true);
                }

                // Find ID associated with the dragged piece and remove it from the AvailableInventory
                ScrapData scrapToRemove = availableScraps.Find(scrap => scrap.ID == usedScrap.ID);
                availableScraps.Remove(scrapToRemove);
                
                _DraggedScrap.SetActive(false);
            }
            else
            {
                // Restore the scraps display position
                _DraggedScrap.transform.position = _InitialScrapPosition;
            }
        }
        else
        {
            // Restore the scraps display position
            _DraggedScrap.transform.position = _InitialScrapPosition;
        }

        _DraggedScrap = null;
    }
}
    