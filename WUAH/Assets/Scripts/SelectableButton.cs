﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectableButton : MonoBehaviour, Selectable
{
    public enum Result
    {
        Quit,
        Restart,
    }

    public Result ButtonResult;
    public GameObject Hover;
    
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
        switch (ButtonResult)
        {
            case Result.Quit:
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                
                break;
            case Result.Restart:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }

    public void OnDeselect()
    {
        // Do Nothing
    }

    public Vector3 GetViewingPosition()
    {
        return Vector3.zero;
    }

    public Quaternion GetViewingRotation()
    {
        return Quaternion.identity;
    }
}
