using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    DamageAble playerDamageable;
    public TMP_Text healthBartext;
    public Slider healthSlider;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
      
        if(Player == null)
        {
            Debug.Log("No player Detect");
        }
        playerDamageable = Player.GetComponent<DamageAble>();
    }
    void Start()
    {     
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health , playerDamageable.MaxHealth);
        healthBartext.text = "HP" + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }
    private void OnEnable()
    {
        playerDamageable.healthChange.AddListener(OnPlayerHealthChange);
    }

    private void OnDisable()
    {
        playerDamageable.healthChange.RemoveListener(OnPlayerHealthChange);
    }
    private float CalculateSliderPercentage(float currecntHealth , float maxHealth)
    {
        return currecntHealth / maxHealth;
    }
    private void OnPlayerHealthChange(int newHealth , int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBartext.text = "HP" + newHealth + " / " + maxHealth;
    }


}
