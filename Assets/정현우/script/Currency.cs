// 작성자 : 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] int sellingAmount;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] DayTimeController dayTimeController;

    private void Start()
    {
        {
            amount = 1000;
            UpdateText();
        }
    }

    private void UpdateText()
    {
        text.text = amount.ToString();  
    }

    public void Add(int moneyGain)
    {
        sellingAmount += moneyGain;
        //amount += moneyGain;
        //UpdateText();
    }

    internal bool Check(int totalPrice) 
    {
        return amount >= totalPrice;
    }

    internal void Decrease(int totalPrice)
    {
        amount -= totalPrice;
        if (amount < 0) { amount = 0; }
        UpdateText();
    }

    private void Update()
    {
        if ((int)dayTimeController.time / 3600f == 0) 
        {
            amount += sellingAmount;
            sellingAmount = 0;
            UpdateText();
        }
    }
}
