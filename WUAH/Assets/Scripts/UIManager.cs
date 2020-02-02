using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RectTransform CanvasRectTransform;
	public GameObject ScrapInventory;
	public GameObject PaintingPallete;

	public static UIManager Instance;

	private void Awake()
	{
		Instance = this;
		
		ScrapInventory.SetActive(false);
		PaintingPallete.SetActive(false);
	}
}
