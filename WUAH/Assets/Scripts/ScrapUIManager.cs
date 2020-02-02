using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapUIManager : MonoBehaviour
{
    public GameObject ScrapUIPrefab;
    public GameObject ScrapInventoryContents;

    public void InitializeScrapUI(ScrapData inScrapData)
    {
        GameObject scrapUIInstance = Instantiate(ScrapUIPrefab);

        UIScrapData scrapUIInfo = scrapUIInstance.GetComponent<UIScrapData>();
        scrapUIInfo.PaintingScrapData = inScrapData;

        RawImage scrapImage = scrapUIInstance.GetComponent<RawImage>();
        scrapImage.texture = inScrapData.Texture;

        scrapUIInstance.transform.SetParent(ScrapInventoryContents.transform, worldPositionStays: false);
    }
}
