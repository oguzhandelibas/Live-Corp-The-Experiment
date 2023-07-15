using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMentalHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mentalHealthText;
    private int mentalHealth = 99;
    private void Start()
    {
        InvokeRepeating("DecreaseMentalHealth", 0, 10);
    }

    private void DecreaseMentalHealth()
    {
        mentalHealth--;
        if (mentalHealth < 70) mentalHealth = 70;
        mentalHealthText.text = mentalHealth.ToString() + "%";
    }
}
