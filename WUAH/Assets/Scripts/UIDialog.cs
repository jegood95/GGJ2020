using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{
	public Text Dialog;

	private List<ChainedDialog> _ChainedDialogs = new List<ChainedDialog>();
    private ChainedDialog _ActiveChain;
	private int _Index;

	public void Queue(ChainedDialog inChainedDialog)
	{
        bool emptyInitialChain = _ChainedDialogs.Count <= 0;
		_ChainedDialogs.Add(inChainedDialog);

        if (emptyInitialChain == true)
        {
            ShowNextDialog();
        }

		PlayerController.Instance.ChangeMode(InputMode.Dialog);
        gameObject.SetActive(true);
    }

	public void ShowNextDialog()
	{
        if (_ChainedDialogs.Count > 0)
        {
            // If we don't have an active dialogue, dequeue the next one
            if (_ActiveChain == null)
            {
                ChainedDialog nextChain = _ChainedDialogs[0];
                _ChainedDialogs.Remove(nextChain);

                _Index = 0;
                _ActiveChain = nextChain;
            }

            int activeChainCount = _ActiveChain.Dialogs.Count;

            if (_Index < activeChainCount)
            {
                ChainedDialog.DialogInfo dialogInfo = _ActiveChain.Dialogs[_Index++];
                Dialog.text = dialogInfo.Dialog;
                Dialog.color = dialogInfo.Color;
            }
            else
            {
                // Mark the next dialog chain ready
                _ActiveChain = null;
            }
        }
        else
        {
            _ChainedDialogs.Clear();
            _ActiveChain = null;

            // No more dialog to show, so switch modes
            gameObject.SetActive(false);
            PlayerController.Instance.ChangeMode(InputMode.Moving);
            return;
        }
	}
}
