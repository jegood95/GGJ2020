using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RectTransform CanvasRectTransform;
    public GameObject PaintingView;
	public GameObject PaintingPallete;
	public UIDialog Dialog;
	public GameObject CrossHair;

	public static UIManager Instance;

	private void Awake()
	{
		Instance = this;
		PaintingView.SetActive(false);
		PaintingPallete.SetActive(false);
		Dialog.gameObject.SetActive(false);
		CrossHair.SetActive(false);
	}
}
