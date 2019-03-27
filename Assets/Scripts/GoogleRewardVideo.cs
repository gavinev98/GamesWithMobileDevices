using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class GoogleRewardVideo : MonoBehaviour
{
    public string AppID;
    public string RewardedAdUnitID;
    public Text CoinText;
    public int Coin = 100;
    public Transform CollectReward;
    

    private RewardBasedVideoAd rewardBasedVideoAd;

    void Start()
    {
        MobileAds.Initialize(AppID);

        rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideoAd.LoadAd(request, RewardedAdUnitID);

        rewardBasedVideoAd.OnAdLoaded += HandlerOnLoaded;
        rewardBasedVideoAd.OnAdRewarded += HandlerOnRewarded;
    
    }

    void Update()
    {
        CoinText.text = "Coins :" + Coin.ToString();
    }

    public void HandlerOnRewarded(object sender,EventArgs args)
    {
        Coin += 100;
       // CollectReward.gameObject.SetActive(true);

    }

    public void HandlerOnLoaded(object sender, EventArgs arg)
    {
        Debug.Log("HandleRewardBasedVideoLoaded event received");
        
    }

    public void destroyRewardPopUp()
    {
        CollectReward.gameObject.SetActive(false);
    }


    public void showAds()
    {
        if(rewardBasedVideoAd.IsLoaded())
        {
            rewardBasedVideoAd.Show();
            Debug.Log("Showing video advert");
        }
    }

}
