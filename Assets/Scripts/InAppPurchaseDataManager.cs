using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InAppPurchaseDataManager : Singleton<InAppPurchaseDataManager>
{

    public Text goldAmount;

    public GameObject noAdsBtn;

    private int goldAmt = 0;

    private bool noads = false;



    public void AddGold(int amount)
    {
        goldAmt += amount;
        goldAmount.text = goldAmt.ToString();
    }


    public void RemoveAdverts()
    {
        noads = true;
        noAdsBtn.gameObject.SetActive(false);
    }
}
