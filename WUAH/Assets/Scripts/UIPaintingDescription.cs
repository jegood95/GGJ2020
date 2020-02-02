using UnityEngine;
using UnityEngine.UI;

public class UIPaintingDescription : MonoBehaviour
{
    public Text Description;

    private void OnEnable()
    {
        Description.text = PlayerController.Instance.Painting.Description;
    }
}
