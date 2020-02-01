using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapUIManager : MonoBehaviour
{
    public GameObject DisplayContents;
    public GameObject ScrapUIPrefab;
    
    public void InitializeScrapUI(Texture inScrapTexture)
    {
        GameObject scrapUI = Instantiate(ScrapUIPrefab);
        RawImage scrapImage = scrapUI.GetComponent<RawImage>();
        scrapImage.texture = inScrapTexture;

        scrapUI.transform.SetParent(DisplayContents.transform, worldPositionStays: false);
    }
}
