using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour, Selectable
{ 
    [System.Serializable]
    public class DialogByScrapID
    {
        public int ID;
        public ChainedDialog Dialog;
    }
    
    public List<PaintingScrap> Scraps;
    public GameObject Hover;
    public ParticleSystem Particles;
    public Vector3 ViewingPositon;
    public List<DialogByScrapID> DialogsByScrapID;
    public Vector3 SubmittedPosition;
    public Vector3 SubmittedLookTowards;
    public Texture Texture;
    public MeshRenderer Renderer;
    [TextArea]
    public string Description;

    private bool _Submitted;

    void Start()
    {
        Hover.SetActive(false);
        SetScrapsCollisionActive(false);
        Renderer.material.mainTexture = Texture;
        Particles.Play();
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
        if (_Submitted == true)
        {
            return;
        }
        
        Hover.SetActive(true);
    }

    public void OnUnhover()
    {
        if (_Submitted == true)
        {
            return;
        }
        
        Hover.SetActive(false);
    }

    public void OnSelect(RaycastHit inHit, PlayerController inPlayer)
    {
        if (_Submitted == true)
        {
            return;
        }
        
        inPlayer.ChangeMode(InputMode.Painting);
        SetScrapsCollisionActive(true);
        Particles.Stop();
    }

    public void OnDeselect()
    {
        if (_Submitted == true)
        {
            return;
        }
        
        SetScrapsCollisionActive(false);
        if (_Submitted == false)
        {
            Particles.Play();
        }
    }

    public Vector3 GetViewingPosition()
    {
        return transform.position + ViewingPositon;
    }

    public Quaternion GetViewingRotation()
    {
        return transform.parent.rotation;
    }

    public bool IsDone()
    {
        foreach (PaintingScrap scrap in Scraps)
        {
            if (scrap.IsDone == false)
            {
                return false;
            }
        }

        return true;
    }

    public void CheckIsDone()
    {
        foreach (PaintingScrap scrap in Scraps)
        {
            scrap.CheckIsDone();
        }
    }

    public float GetPercentComplete()
    {
        float percent = 0f;
        float percentPerScrap = 1f / (float)Scraps.Count;
        foreach (PaintingScrap scrap in Scraps)
        {
            percent += scrap.IsDone ? percentPerScrap : 0f;
        }

        return percent;
    }

    public void Submit()
    {
        _Submitted = true;
        transform.parent.position = SubmittedPosition;
        transform.parent.LookAt(SubmittedLookTowards);

        // Queue dialog for submitted scraps
        foreach (PaintingScrap scrap in Scraps)
        {
            ScrapData scrapData = scrap.Scrap;
            DialogByScrapID curatorDialog = DialogsByScrapID.Find(scrapDialogs => scrapDialogs.ID == scrapData.ID);
            UIManager.Instance.Dialog.Queue(curatorDialog.Dialog);
        }

        GameController.Instance.PaintingSubmitted(transform.parent.gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + ViewingPositon, 0.5f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(SubmittedPosition, 0.5f);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(SubmittedLookTowards, 0.5f);
    }
}
