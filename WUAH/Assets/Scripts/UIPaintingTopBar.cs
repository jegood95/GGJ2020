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
        if (PaintingManager.Instance == null ||
            PaintingManager.Instance.Player == null ||
            PaintingManager.Instance.Player.Painting == null)
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
        Submit.interactable = false;
        PaintingManager.Instance.Player.Painting.Submit();
    }

    public void OnBack()
    {
        PaintingManager.Instance.Player.ChangeMode(InputMode.Moving);
    }
}
