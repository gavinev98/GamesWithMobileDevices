using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleBanner : MonoBehaviour
{

    public string AppID;
    public string BannerAdUnitID;

    private BannerView bannerView;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(AppID);

        bannerView = new BannerView(BannerAdUnitID, AdSize.Banner, AdPosition.Bottom);
        bannerView.LoadAd(new AdRequest.Builder().Build());

        // creating handler events for banners
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // creating handler for failed to load banner
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        //creating handler when ad is opened
        bannerView.OnAdOpening += HandleOnAdOpended;
        //creating handler when the ad is closed.
        bannerView.OnAdClosed += HandleOnAdClosed;
       // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
    }


    //handle the load of the advertisement.
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLoaded event recieved");
    }
    //handle the failure of loading an ad.
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("Message recieved" + args.Message);
    }
    // handling the opened of an ad.
    public void HandleOnAdOpended(object sender, EventArgs args)
    {
        Debug.Log("Opened event recieved");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("Closed event recieved");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLeavingApplication event received");
    }

    public void destroyBanner()
    {
        bannerView.Destroy();
    }

}
