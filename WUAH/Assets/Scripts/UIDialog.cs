using UnityEngine;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{
	public Text Dialog;

	private ChainedDialog _ChainedDialog;
	private int _Index;

	public void Show(ChainedDialog inChainedDialog)
	{
		_ChainedDialog = inChainedDialog;
		_Index = -1;
		gameObject.SetActive(true);
		ShowNextDialog();
		PlayerController.Instance.ChangeMode(InputMode.Dialog);
	}

	public void ShowNextDialog()
	{
		_Index++;

		if (_ChainedDialog == null ||
		    _Index >= _ChainedDialog.Dialogs.Count)
		{
			gameObject.SetActive(false);
			PlayerController.Instance.ChangeMode(InputMode.Moving);
			return;
		}
		
		ChainedDialog.DialogInfo dialogInfo= _ChainedDialog.Dialogs[_Index];
		Dialog.text = dialogInfo.Dialog;
		Dialog.color = dialogInfo.Color;
	}
}
