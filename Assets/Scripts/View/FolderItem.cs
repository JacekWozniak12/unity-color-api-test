using System.Collections.Generic;
using UnityEngine;

public class FolderItem : MonoBehaviour
{
    ColorItem[] colorItems;

    void Start()
    {
        SetColorItems();
        ApiConnector.Instance.ColorReady.AddListener(UpdateColors);
    }

    public void GetColors()
    {
        Dictionary<int, Color> dictionary = new Dictionary<int, Color>();

        foreach (ColorItem colorItem in colorItems)
            if (colorItem.Dirty) dictionary.Add(colorItem.Index, colorItem.Color);

        Debug.Log(dictionary);

        if (dictionary.Count > 0)
            ApiConnector.Instance.RequestColorScheme(dictionary);
        else
            ApiConnector.Instance.RequestColorScheme();
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
            colorItems[i].SetColor(colorArray);
    }
}
