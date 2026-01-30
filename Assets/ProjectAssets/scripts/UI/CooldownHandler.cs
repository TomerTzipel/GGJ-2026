using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownHandler : MonoBehaviour
{
    [SerializeField]Image maskImage;
    [SerializeField]TextMeshProUGUI cooldownText;

    private float _timeLeft;
    

    public void StartCooldown(float cooldown)
    {
        StartCoroutine(Cooldown(cooldown));
    }
    
    public void MaskTypeUI(MaskType maskType)
    {
        
    }

    private IEnumerator Cooldown(float cooldown)
    {
        //Show the timer
        cooldownText.gameObject.SetActive(true);
        _timeLeft =  cooldown;
        while (_timeLeft > 0)
        {
            //Set text the current
            cooldownText.text = $"{_timeLeft}s";
            yield return new WaitForSeconds(1f);
            _timeLeft--;
        }
        //Hide the timer
        cooldownText.gameObject.SetActive(false);
    }  
    
    
}
