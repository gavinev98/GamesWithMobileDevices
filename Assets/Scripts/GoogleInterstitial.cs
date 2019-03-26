using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleInterstitial : MonoBehaviour
{
    public string AppID;
    public string InterstitialAdUnitID;

    private InterstitialAd interstitialAd;

    void Start()
    {

        MobileAds.Initialize(AppID);

        interstitialAd = new InterstitialAd(InterstitialAdUnitID);
        interstitialAd.LoadAd(new AdRequest.Builder().Build());

        // Called when an ad request has successfully loaded.
        this.interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialAd.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;


    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLeavingApplication event received");
    }


    public void showAds()
    {
        if(interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
    }

    public void destroyInterstitial()
    {
        interstitialAd.Destroy();
    }
}
