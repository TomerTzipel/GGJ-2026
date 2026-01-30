using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    
    [SerializeField] private Slider slider;

    [ContextMenu("SetValue")]
    public void SetHalf()
    {
        ModifySlider(0.5f);
    }
    // ↑ just to test ↑
    
    
    public void ModifySlider(float value)
    {
        slider.value = value;
    }
}
