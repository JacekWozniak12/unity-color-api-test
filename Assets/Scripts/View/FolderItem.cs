using UnityEngine;

public class FolderItem : MonoBehaviour
{
    ColorItem[] colorItems;

    void Start()
    {
        SetColorItems();
        ApiConnector.Instance.ColorReady.AddListener(UpdateColors);
    }

    void SetColorItems()
    {
        colorItems = gameObject.GetComponentsInChildren<ColorItem>();

        for (int i = 0; i < colorItems.Length; i++)
            colorItems[i].Index = i;
    }

    void UpdateColors(Color[] colorArray)
    {
        for (int i = 0; i < colorItems.Length; i++)
        {
            colorItems[i].SetColor(colorArray);
        }
    }
}
