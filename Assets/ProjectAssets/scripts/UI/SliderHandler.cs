using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] private FloatEventChannel playerHurtEC;
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        playerHurtEC.OnEvent += HandlePlayerHurt;
    }
    private void OnDisable()
    {
        playerHurtEC.OnEvent -= HandlePlayerHurt;
    }


    private void HandlePlayerHurt(float value)
    {
        ModifySlider(value);
    }

    [ContextMenu("SetValue")]
    public void SetHalf()
    {
        ModifySlider(0.5f);
    }
    // ↑ just to test ↑
    
    
    private void ModifySlider(float value)
    {
        slider.value = value;
    }
}
