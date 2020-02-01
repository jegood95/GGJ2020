using UnityEngine;

public enum InputMode
{
	None,
	Moving,
	Painting,
	PaintingScrap,
}

public class PlayerController : MonoBehaviour
{
	public Camera Camera;
    public Transform MyTransform;
	public CharacterController Controller;
	public float Speed;
	public Vector2 Sensitivity;
	public ScrapData TestScrapData;

    public GameObject ScrapInventory;

	private float _MinY = -60f;
	private float _MaxY = 60f;
	private float _RotationY = 0f;

	private Selectable _Selectable;
	private PaintingScrap _Scrap;
	private InputMode _Mode;
	private Vector3 _StartingCameraPosition;
	private Quaternion _StartingCameraRotation;
	private float _InputDelay;
	private const float InputDelayDueToModeChange = 0.25f;
	private Quaternion _RotationWhenSelectingPainting;
	private Color _Color;
	private int _BrushSize;

	public PaintingScrap Scrap
	{
		get { return _Scrap; }
	}

	public Color Color
	{
		get { return _Color; }
	}
	
	public int Brush
	{
		get { return _BrushSize; }
	}

	private void Start()
	{
		_StartingCameraPosition = Camera.transform.localPosition;
		_StartingCameraRotation = Camera.transform.localRotation;
		_RotationWhenSelectingPainting = transform.rotation;
		ChangeMode(InputMode.Moving);
	}

	// Update is called once per frame
	void Update ()
	{
		if (_InputDelay > 0f)
		{
			_InputDelay -= Time.deltaTime;
			return;
		}
		
		switch (_Mode)
		{
			case InputMode.Moving:
				float rotationX = MyTransform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity.x;
				_RotationY += Input.GetAxis("Mouse Y") * Sensitivity.y;
				_RotationY = Mathf.Clamp(_RotationY, _MinY, _MaxY);
				MyTransform.localEulerAngles = new Vector3(-_RotationY, rotationX, 0);
				
				// float horizontal = Input.GetAxis("Horizontal");
				float vertical = Input.GetAxis("Vertical");
				Vector3 forward = MyTransform.TransformDirection(Vector3.forward);
				forward.Normalize();
				float curSpeed = Speed * vertical;
				Controller.SimpleMove(forward * curSpeed);

				forward.y = 0f;
				forward.Normalize();
				float horizontal = Input.GetAxis("Horizontal");
				curSpeed = Speed * horizontal;
				Vector3 left = -Vector3.Cross(forward, Vector3.up).normalized;
				Controller.SimpleMove(left * curSpeed);

				Selectable selectable = null;
				Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out RaycastHit hit) == true)
				{
					selectable = hit.collider.GetComponent<Selectable>();
				}

				if (selectable != _Selectable)
				{
					_Selectable?.OnUnhover();
					_Selectable = selectable;
					_Selectable?.OnHover();
				}

				if (Input.GetMouseButton(0) == true &&
				    _Selectable != null)
				{
					_Selectable.OnSelect(hit, this);
				}
				break;
			case InputMode.Painting:
				PaintingScrap scrap = null;
				Ray paintingRay = Camera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(paintingRay, out RaycastHit paintingHit) == true)
				{
					scrap = paintingHit.collider.GetComponent<PaintingScrap>();
				}

				if (scrap != _Selectable)
				{
					_Scrap?.OnUnhover();
					_Scrap = scrap;
					_Scrap?.OnHover();
				}
				
				if (scrap != null &&
					Input.GetMouseButtonUp(0) == true)
				{
					_Scrap.SetScrap(TestScrapData);
					_Scrap.OnSelect(paintingHit, this);
					ChangeMode(InputMode.PaintingScrap);
				}

				if (Input.GetKeyDown(KeyCode.Escape))
				{
					ChangeMode(InputMode.Moving);
					_Selectable.OnDeselect();
				}
				break;
			case InputMode.PaintingScrap:
				
				Ray scrapRay = Camera.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(scrapRay, out RaycastHit scrapHit) == true)
				{
					PaintingScrap paintingScrap = scrapHit.collider.GetComponent<PaintingScrap>();
					if (paintingScrap == _Scrap &&
						Input.GetMouseButton(0) == true)
					{
						_Scrap.Paint(scrapHit.textureCoord, _Color, _BrushSize);
					}
				}

				if (Input.GetKeyDown(KeyCode.Escape))
				{
					ChangeMode(InputMode.Painting);
					_Scrap.OnDeselect();
				}
				break;
		}
	}

	public void ChangeMode(InputMode inMode)
	{
		if (_Mode == inMode)    
		{
			return;
		}
		
		_Mode = inMode;

		switch (_Mode)
		{
			case InputMode.Moving:
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				transform.rotation = _RotationWhenSelectingPainting;
				Camera.transform.localPosition = _StartingCameraPosition;
				Camera.transform.localRotation = _StartingCameraRotation;
				UIManager.Instance.ScrapInventory.SetActive(false);
				_Scrap?.OnUnhover();
				_Scrap?.OnDeselect();
                break;
			case InputMode.Painting:
				_RotationWhenSelectingPainting = transform.rotation;
				transform.rotation = Quaternion.identity;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				Camera.transform.position = _Selectable.GetViewingPosition();
				Camera.transform.rotation = _Selectable.GetViewingRotation();
				UIManager.Instance.ScrapInventory.SetActive(true);
				UIManager.Instance.PaintingPallete.SetActive(false);
				_Scrap?.OnUnhover();
				_Scrap?.OnDeselect();
				break;
			case InputMode.PaintingScrap:
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				Camera.transform.position = _Scrap.GetViewingPosition();
				UIManager.Instance.ScrapInventory.SetActive(false);
				UIManager.Instance.PaintingPallete.SetActive(true);
				break;
		}

		_InputDelay = InputDelayDueToModeChange;
	}

	public void SetColor(Color inColor)
	{
		_Color = inColor;
	}

	public void SetBrushSize(int inBrushSize)
	{
		_BrushSize = inBrushSize;
	}
}
