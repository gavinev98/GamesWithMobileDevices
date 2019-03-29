using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAds : MonoBehaviour
{

    // private BannerView bannerView;


    //string for banner
    public string bannerPlacement = "banner";
    //setting testMode to true.
    public bool testMode = true;

    //google admob testing Identifiers.
    [SerializeField] private string appID = "ca-app-pub-9018596307029717~6800607028";
    [SerializeField] private string bannerID = "ca-app-pub-9018596307029717/3372621615";
    [SerializeField] private string regularId = "ca-app-pub-9018596307029717/7594157403";

    // variables for coin rewards.
    public Text CoinText;
    public int Coin = 100;
    public Transform CollectReward;


    private void Start()
    {

        //google admobs initialization.
        // MobileAds.Initialize((appID));

        //unitybanner initialization will displayed after particular period of time.
      //  StartCoroutine(ShowBannerWhenReady());
    }

    public void Update()
    {
        CoinText.text = "Coins :" + Coin.ToString();
    }

    public void onClickShowBanner()
    {
        this.RequestBanner();
    }

    public void onClickShowRegAdd()
    {
        this.RequestRegAd();
    }

    public void RequestRegAd()
    {
        //  InterstitialAd regAd = new InterstitialAd(regularId);

        //  AdRequest request = new AdRequest.Builder().Build();

        //  regAd.LoadAd(request);
    }


    public void RequestBanner()
    {
        // bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Center);

        //  AdRequest request = new AdRequest.Builder().Build();

        //  bannerView.LoadAd(request);
    }


    // Using unity ads

    public void showUnityAd()
    {

        //if in the unity editor yield the game when the ad is running.
#if UNITY_EDITOR
        StartCoroutine(waitforAd());
#endif
        //Adding condition to check for stable connection
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandAdResult });
        }
    }

    public void HandAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //give the user 100 coins on sucessful watch of full ad.
                Coin += 100;
                break;
            case ShowResult.Skipped:
                Debug.Log("Player has not watched full ad");
                break;
            case ShowResult.Failed:
                Debug.Log("Advertisement has failed to load");
                break;
        }
    }

    //method to stop game when method is loaded
    IEnumerator waitforAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }


    //method to show unity ads banner at a few seconds into the load of game.
    /*
    IEnumerator ShowBannerWhenReady()
    {
        // if banner is not ready wait few seconds
        while (!Advertisement.IsReady("banner"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerPlacement);

    }
    */
}


//gamesfordevices2@gmail.com
