using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField]
    ColorChanger colorChanger;

    [SerializeField]
    GameObject view;

    public static Popup Instance;

    void Awake()
    {
        Instance = this;
    }

    public void RequestColorChanger(ColorItem item)
    {
        colorChanger.Connect(item);
    }

    /// <summary>
    /// Hides popup and request giver, if request giver is a child of the popup system
    /// </summary>
    public void RequestHide(GameObject requestGiver)
    {
        if (requestGiver.transform.parent == this.transform)
            requestGiver.SetActive(false);

        this.gameObject.SetActive(false);
    }
}