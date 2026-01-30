using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownHandler : MonoBehaviour
{
    [SerializeField]Image maskImage;
    [SerializeField]TMP_Text cooldownText;
    [SerializeField]Image cooldownImage;
    [SerializeField] MaskTypeEventChannel maskChangedEC;

    private float _timeLeft;

    private void OnEnable()
    {
        maskChangedEC.OnEvent += HandleMaskChanged;
    }

    private void OnDisable()
    {
        maskChangedEC.OnEvent -= HandleMaskChanged;
    }

    private void Start()
    {
        StartCooldown(5);
    }
    //↑ just to test ↑

    public void StartCooldown(float cooldown)
    {
        StartCoroutine(Cooldown(cooldown));
    }
    
    public void HandleMaskChanged(MaskType maskType)
    {
        Sprite sprite = MaskSettings.GetSpriteByType(maskType);
        maskImage.sprite = sprite;
    }

    private IEnumerator Cooldown(float cooldown)
    {
        //Show the timer
        cooldownText.gameObject.SetActive(true);
        cooldownImage.gameObject.SetActive(true);
        _timeLeft =  cooldown;
        while (_timeLeft >= 0)
        {
            //Set text the current
            cooldownText.text = $"{_timeLeft}s";
            yield return new WaitForSeconds(1f);
            _timeLeft--;
        }
        //Hide the timer
        cooldownText.gameObject.SetActive(false);
        cooldownImage.gameObject.SetActive(false);
    }  
    
    
}
