using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapUIManager : MonoBehaviour
{
    public GameObject ScrapUIPrefab;
    public GameObject ScrapInventoryContents;

    private List<UIScrapData> _UIScrapInfos = new List<UIScrapData>();

    public static ScrapUIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        List<int> paintingScrapValidIDs = new List<int>();
        foreach (Painting.DialogByScrapID data in PlayerController.Instance.Painting.DialogsByScrapID)
        {
            paintingScrapValidIDs.Add(data.ID);
        }
        
        foreach(UIScrapData uiScrapInfo in _UIScrapInfos)
        {
            bool isAvailable = PaintingScrapManager.Instance.AvailableScraps.Contains(uiScrapInfo.PaintingScrapData);
            bool isValid = paintingScrapValidIDs.Contains(uiScrapInfo.PaintingScrapData.ID);
            uiScrapInfo.gameObject.SetActive(isAvailable && isValid);
        }
    }

    public void InitializeScrapUI(ScrapData inScrapData)
    {
        GameObject scrapUIInstance = Instantiate(ScrapUIPrefab);

        UIScrapData scrapUIInfo = scrapUIInstance.GetComponent<UIScrapData>();
        scrapUIInfo.Initilaize(inScrapData);
        _UIScrapInfos.Add(scrapUIInfo);

        RawImage scrapImage = scrapUIInstance.GetComponent<RawImage>();
        scrapImage.texture = inScrapData.Texture;

        scrapUIInstance.transform.SetParent(ScrapInventoryContents.transform, worldPositionStays: false);
    }

    public UIScrapData GetUIScrapInfoWithScrapData(ScrapData inData)
    {
        return _UIScrapInfos.Find(scrapUIInfo => scrapUIInfo.PaintingScrapData == inData);
    }
}
