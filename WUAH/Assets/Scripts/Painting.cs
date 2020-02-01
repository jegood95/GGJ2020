using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour, Selectable
{
    public List<PaintingScrap> Scraps;
    public GameObject Hover;
    public Vector3 ViewingPositon;

    void Start()
    {
        Hover.SetActive(false);
        SetScrapsCollisionActive(false);
    }

    private void SetScrapsCollisionActive(bool inActive)
    {
        foreach (PaintingScrap scrap in Scraps)
        {
            scrap.Collider.enabled = inActive;
        }
    }

    public void OnHover()
    {
        Hover.SetActive(true);
    }

    public void OnUnhover()
    {
        Hover.SetActive(false);
    }

    public void OnSelect(RaycastHit inHit, PlayerController inPlayer)
    {
        inPlayer.ChangeMode(InputMode.Painting);
        SetScrapsCollisionActive(true);
    }

    public void OnDeselect()
    {
        SetScrapsCollisionActive(false);
    }

    public Vector3 GetViewingPosition()
    {
        return transform.position + ViewingPositon;
    }

    public Quaternion GetViewingRotation()
    {
        return transform.parent.rotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + ViewingPositon, 0.5f);
    }
}
