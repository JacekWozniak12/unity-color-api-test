using UnityEngine;

public class Popup : MonoBehaviour
{
    public static Popup Instance;

    [SerializeField]
    ColorChanger colorChanger;

    [SerializeField]
    ErrorMessage errorMessage;

    [SerializeField]
    Loading loading;

    [SerializeField]
    GameObject view;

    void Awake() => Instance = this;

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
    /// Hides popup and request giver, if hideRequestGiver is not specified or true
    /// </summary>
    public void RequestHide(GameObject requestGiver, bool hideRequestGiver = true)
    {
        if (hideRequestGiver) requestGiver.SetActive(false);
        view.SetActive(false);
    }

    /// <summary>
    /// Displays loading (if not already) and changes the value slider
    /// </summary>
    public void RequestLoading(float progress)
    {
        if (!loading.gameObject.activeInHierarchy)
        {
            view.SetActive(true);
            loading.gameObject.SetActive(true);
        }
        loading.UpdateSlider(progress);
    }

    /// <summary>
    /// Hides popup and zero loading
    /// </summary>
    public void RequestHideLoading()
    {
        loading.UpdateSlider(0);
        view.SetActive(false);
        loading.gameObject.SetActive(false);
    }
}