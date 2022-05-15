using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    public void UpdateSlider(float progress)
    {
        progress = Mathf.Clamp01(progress);
        slider.value = progress;
    }
}