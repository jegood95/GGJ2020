using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> Paintings;
    public ChainedDialog EndGameDialog;
    public Transform DoorLeft;
    public Transform DoorRight;

    private List<GameObject> _ActivePaintings;

    public static GameController Instance;

    private void Awake()
    {
        _ActivePaintings = new List<GameObject>(Paintings);
        Instance = this;

        DoorLeft.localRotation = Quaternion.Euler(0f, 0f, 90f);
        DoorRight.localRotation = Quaternion.Euler(0f, 0f, -90f);
    }

    public void PaintingSubmitted(GameObject inPainting)
    {
        _ActivePaintings.Remove(inPainting);

        if (_ActivePaintings.Count <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        DoorLeft.localRotation = Quaternion.Euler(0f, 0f, 170f);
        DoorRight.localRotation = Quaternion.Euler(0f, 0f, -170f);
        
        UIManager.Instance.Dialog.Queue(EndGameDialog);
    }
}
