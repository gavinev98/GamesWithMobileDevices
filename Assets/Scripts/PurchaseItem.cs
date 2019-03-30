using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItem : MonoBehaviour
{

    public enum ItemType
    {
        Gold50,
        Gold100,
        NoAds
    }


    //to be set on button.
    public ItemType itemType;

    public Text priceText;

    private string defaultext;


    // Start is called before the first frame update
    void Start()
    {

        defaultext = priceText.text;
        StartCoroutine(LoadPriceCoru());
        
    }

    
    public void ClickBuy()
    {
        switch(itemType)
        {
            case ItemType.Gold50:
                AppPurchaseManger.Instance.Buy50Gold();
                break;
            case ItemType.Gold100:
                AppPurchaseManger.Instance.Buy100Gold();
                break;
            case ItemType.NoAds:
                AppPurchaseManger.Instance.BuyNoAds();
                break;
        }
    }

    private IEnumerator LoadPriceCoru()
    {
        while (AppPurchaseManger.Instance.IsInitialized())
            yield return null;

        string loadedprice = "";

        switch (itemType)
        {
            case ItemType.Gold50:
                loadedprice = AppPurchaseManger.Instance.GetProductPrice(AppPurchaseManger.Instance.GOLD_50);
                break;
            case ItemType.Gold100:
                loadedprice = AppPurchaseManger.Instance.GetProductPrice(AppPurchaseManger.Instance.GOLD_100);
                break;
            case ItemType.NoAds:
                loadedprice = AppPurchaseManger.Instance.GetProductPrice(AppPurchaseManger.Instance.NO_ADS);
                break;
        }

        priceText.text = defaultext + "" + loadedprice;
    }
}
