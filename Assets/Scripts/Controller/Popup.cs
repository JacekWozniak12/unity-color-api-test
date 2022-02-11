using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField]
    ColorChanger colorChanger;

    [SerializeField]
    ErrorMessage errorMessage;

    [SerializeField]
    GameObject view;

    public static Popup Instance;

    void Awake()
    {
        Instance = this;
    }

    public void RequestColorChanger(ColorItem item)
    {
        view.SetActive(true);
        colorChanger.gameObject.SetActive(true);
        colorChanger.Connect(item);
    }

    public void RequestErrorMessage(string information)
    {
        view.SetActive(true);
        errorMessage.gameObject.SetActive(true);
        errorMessage.DisplayMessage(information);
    }

    /// <summary>
    /// Hides popup and request giver, if request giver is a child of the popup system
    /// </summary>
    public void RequestHide(GameObject requestGiver)
    {
        if (requestGiver.transform.parent == this.transform)
            requestGiver.SetActive(false);

        view.SetActive(false);
    }
}