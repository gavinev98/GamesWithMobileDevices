using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiFunctions : MonoBehaviour
{



    #region Variables
    //Public
    public bool gamePlaying = false;


    //Private
    [SerializeField]
    private int score;
    [SerializeField]
    private Text scoreCounter;
    [SerializeField]
    private Button tapToPlay;
    [SerializeField]
    private Button leaderboards;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Text scoreCount;
    [SerializeField]
    private Text highScore;
    //store the high score
    private int score0 = 0;


    //give player coins.
    public int coin;
    public Text CoinText;
    public Transform rewardedCoins;

    //acquire the google interstitial
    public GoogleInterstitial admobInterstitial;
    //acquire the google rewarded video.
    public GoogleRewardVideo rewardedVideo;
    //show unity ad
    public UnityAds showUnityVideo;





    #endregion

    #region UnityFunctions
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("score"));
        InvokeRepeating("scoreUpdate", 1.0f, 1.0f);
         restartButton.gameObject.SetActive(false);

    }

    void Update()
    {
       
    }

    private void scoreUpdate()
    {
     if(gamePlaying == true)
        {
            score++;
            scoreCounter.text = "" + score;
        }
    }

    public void tapPlay()
    {
        Debug.Log("Button Pressed");
        scoreCounter.gameObject.SetActive(true);
        leaderboards.gameObject.SetActive(false);
        tapToPlay.gameObject.SetActive(false);
        highScore.gameObject.SetActive(false);
        scoreCount.gameObject.SetActive(false);

        gamePlaying = true;
    }

    public void leaderboard()
    { 

    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameEnded()
    {
        Debug.Log("Game Ended");
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score); // highscore;

            //If new highscore is set then show rewarded video and give user coins.
            showRewardVideo();
        }
   
        highScore.gameObject.SetActive(true);
        highScore.text = "High Score : " + PlayerPrefs.GetInt("score"); // setting high score.
        scoreCount.gameObject.SetActive(true);
        scoreCount.text = "Your Score: " + score;
        scoreCounter.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        leaderboards.gameObject.SetActive(true);
        //tapToPlay.gameObject.SetActive(true);
        gamePlaying = false; 
        PlayerPrefs.Save();
    }


    //creating methods for showing google abmods interstitial and rewarded video
    public void showRewardPopup()
    {
        rewardedCoins.gameObject.SetActive(true);
    }

    public void hideRewardPopup()
    {
        rewardedCoins.gameObject.SetActive(true);
    }


    public void showRewardVideo()
    {
        rewardedVideo.showAds();
    }

    public void showInterstitial()
    {
        admobInterstitial.showAds();
    }

    public void unityVideo()
    {
        showUnityVideo.showUnityAd();
    }

    #endregion



}
