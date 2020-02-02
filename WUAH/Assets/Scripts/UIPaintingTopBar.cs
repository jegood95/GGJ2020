using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPaintingTopBar : MonoBehaviour
{
    public Button Submit;
    public Text PercentComplete;

    private void OnEnable()
    {
        if (PaintingManager.Instance == null)
        {
            Submit.interactable = false;
            return;
        }
        
        if (PaintingManager.Instance.Player == null)
        {
            Submit.interactable = false;
            return;
        }
        
        Painting painting = PaintingManager.Instance.Player.Painting;
        
        painting.CheckIsDone();
        PercentComplete.text = $"{painting.GetPercentComplete():P}";
        PercentComplete.color = painting.IsDone() ? Color.green : Color.red;
        Submit.interactable = painting != null && painting.IsDone();
    }

    public void OnSubmit()
    {
        PaintingManager.Instance.Player.ChangeMode(InputMode.Moving);
    }
}
