using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapManager : MonoBehaviour
{
    public List<ScrapData> StartingScraps = new List<ScrapData>();
    public GameObject ScrapInventoryUI;

    void Awake()
    {
        ScrapUIManager scrapUIMgr = ScrapInventoryUI.GetComponent<ScrapUIManager>();

        if (ScrapInventoryUI != null)
        {
            // Initialize scrap UI
            foreach (ScrapData scrap in StartingScraps)
            {
                scrapUIMgr.InitializeScrapUI(scrap.Texture);
            }
        }
        else
        {
            Debug.Log("Couldn't find ScrapUIManager in scene");
        }
    }

    // TODO: collection logic
}