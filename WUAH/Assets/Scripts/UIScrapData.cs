using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrapData : MonoBehaviour
{
    public Text Name;

    private ScrapData _PaintingScrapData;
    
    public ScrapData PaintingScrapData
    {
        get { return _PaintingScrapData; }
    }

    public void Initilaize(ScrapData inData)
    {
        _PaintingScrapData = inData;
        Name.text = _PaintingScrapData.Name;
    }
}
