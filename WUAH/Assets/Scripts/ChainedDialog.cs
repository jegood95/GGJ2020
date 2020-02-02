using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChainedDialog : ScriptableObject
{
	[System.Serializable]
	public class DialogInfo
	{
		[TextArea]
		public string Dialog;
		public Color Color;
	}

	public List<DialogInfo> Dialogs;
}
