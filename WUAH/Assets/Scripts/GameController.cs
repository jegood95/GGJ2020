using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> Paintings;
    public ChainedDialog EndGameDialog;

    private List<GameObject> _ActivePaintings;

    public static GameController Instance;

    private void Awake()
    {
        _ActivePaintings = new List<GameObject>(Paintings);
        Instance = this;
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
        UIManager.Instance.Dialog.Queue(EndGameDialog);
    }
}
