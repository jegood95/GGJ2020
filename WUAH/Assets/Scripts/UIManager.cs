using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RectTransform CanvasRectTransform;
	public GameObject ScrapInventory;
	public GameObject PaintingPallete;
	public GameObject PaintingTopBar;
	public UIDialog Dialog;
	public GameObject CrossHair;

	public static UIManager Instance;

	private void Awake()
	{
		Instance = this;
		
		ScrapInventory.SetActive(false);
		PaintingPallete.SetActive(false);
		PaintingTopBar.SetActive(false);
		Dialog.gameObject.SetActive(false);
		CrossHair.SetActive(false);
	}
}
