using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour, Selectable
{
    public List<PaintingScrap> Scraps;
    public ParticleSystem Hover;
    public Vector3 ViewingPositon;

    void Start()
    {
        Hover.Stop();
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
        Hover.Play();
    }

    public void OnUnhover()
    {
        Hover.Stop();
        Hover.Clear();
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
}
