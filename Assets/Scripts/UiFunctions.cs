using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using System.IO;

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
    private Button socialMediaButton;
    [SerializeField]
    private Text socialMediaCaption;
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




    public static UiFunctions instance;


    #endregion

    #region UnityFunctions
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log(PlayerPrefs.GetInt("score"));
        InvokeRepeating("scoreUpdate", 1.0f, 1.0f);
         restartButton.gameObject.SetActive(false);
        StartCoroutine(showAdAfterSeconds());

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
        socialMediaButton.gameObject.SetActive(false);
        socialMediaCaption.gameObject.SetActive(false);
        //give achievement if play plays game
        PlayServices.instance.UnlockAchievement(GPGSIds.achievement_dailyplayer);
        gamePlaying = true;
    }


    public void ShowAchievements()
    {
        PlayServices.instance.showAchievementUI();
    }
    // leaderboards with scores
    public void ShowLeaderboard()
    {
        PlayServices.instance.showLeaderboard();
    }
    //connect to facebook
    public void facebook()
    {

    }
    //connect to twitter.
    public void twitter()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GameEnded()
    {
      
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score); // highscore;

            //give achievement if score is highscore
            PlayServices.instance.UnlockAchievement(GPGSIds.achievement_high_score);

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
        socialMediaButton.gameObject.SetActive(true);
        socialMediaCaption.gameObject.SetActive(true);
        //tapToPlay.gameObject.SetActive(true);
        gamePlaying = false; 
        PlayerPrefs.Save();
        
        // add score to leaderboard when game ends.
        PlayServices.instance.AddScoretoleaderboard(GPGSIds.leaderboard_cube_dodger_leaderboard, PlayerPrefs.GetInt("score"));
    }


    //sharing to social media.
    public void clickShare()
    {
        StartCoroutine(TakeSSAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject("CubeDodger").SetText("I scored a new highscore").Share();
    }

    public IEnumerator showAdAfterSeconds()
    {
        //wait for 10 seconds
        yield return new WaitForSeconds(10);
        //show interstitial advert.
        showInterstitial();
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
