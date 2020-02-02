using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingScrapManager : MonoBehaviour
{
    public List<ScrapData> AvailableScraps = new List<ScrapData>();

    public static PaintingScrapManager Instance;

    private ScrapUIManager _ScrapUIMgr;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _ScrapUIMgr = UIManager.Instance.PaintingView.GetComponentInChildren<ScrapUIManager>();

        if (_ScrapUIMgr != null)
        {
            // Initialize scrap UI
            foreach (ScrapData scrap in AvailableScraps)
            {
                _ScrapUIMgr.InitializeScrapUI(scrap);
            }
        }
        else
        {
            Debug.Log("Couldn't find ScrapUIManager in scene");
        }
    }

    // TODO: collection logic
}