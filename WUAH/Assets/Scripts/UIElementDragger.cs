using UnityEngine;
using UnityEngine.EventSystems;

public class UIElementDragger : EventTrigger
{
    private bool _Dragging = false;
    private Vector2 _InitialPos;

    public void Update()
    {
        if (_Dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        // Record the starting position before dragging
        _InitialPos = transform.position;
        _Dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // Restore original position (for now)
        transform.position = _InitialPos;
        _Dragging = false;
    }

    // TODO: Draggable hit should raycast to detect scrap. Either do this as part of the
    // PlayerController call, or do it exclusively in the character controller
}
