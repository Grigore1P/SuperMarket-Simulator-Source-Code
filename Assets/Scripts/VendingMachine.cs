using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yelbouziani;

public class VendingMachine : MonoBehaviour
{
    [SerializeField] private float moneyPerTime;
    [Header("Time For Add Money In Seconds")]
    [SerializeField] private float timeForAddMoney;

    private float currentTime;
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeForAddMoney)
        {
            currentTime = 0;
            List<string> information = new List<string>
            {
            "Vending Machine  +" + moneyPerTime.ToString()
            };
            YelbController.Instance.SpawnNotification(information, null);
            SaveBridge.SetFloatPP(YelbRef.CashValue, YelbBackend.GetValueFromFloat(YelbRef.CashValue) + moneyPerTime);
        }
    }
}
