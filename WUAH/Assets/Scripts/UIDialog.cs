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

        if (_ActiveChain == null)
        {
	        _ActiveChain = _ChainedDialogs[0];
	        _ChainedDialogs.RemoveAt(0);
	        _Index = -1;
            ShowNextDialog();
        }

		PlayerController.Instance.ChangeMode(InputMode.Dialog);
        gameObject.SetActive(true);
    }

	public void ShowNextDialog()
	{
		_Index++;

		if (_Index < _ActiveChain.Dialogs.Count)
		{
			ChainedDialog.DialogInfo dialogInfo = _ActiveChain.Dialogs[_Index];
			Dialog.text = dialogInfo.Dialog;
			Dialog.color = dialogInfo.Color;
		}
		else if (_ChainedDialogs.Count > 0)
		{
			_ActiveChain = _ChainedDialogs[0];
			_ChainedDialogs.RemoveAt(0);
			_Index = 0;
			
			ChainedDialog.DialogInfo dialogInfo = _ActiveChain.Dialogs[_Index];
			Dialog.text = dialogInfo.Dialog;
			Dialog.color = dialogInfo.Color;
		}
		else
		{
			_ActiveChain = null;
			gameObject.SetActive(false);
			PlayerController.Instance.ChangeMode(InputMode.Moving);
		}
	}
}
