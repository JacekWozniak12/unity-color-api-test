using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    Button button;

    void Start() => button.onClick.AddListener(Close);
    public void DisplayMessage(string text) => textComponent.text = text;
    void Close() => Popup.Instance.RequestHide(this.gameObject);
}