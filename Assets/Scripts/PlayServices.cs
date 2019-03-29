using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;


public class PlayServices : MonoBehaviour
{
    public static PlayServices instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Build().Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
        SignIn();
    }

    public void SignIn()
    {
        // authenticate user:
        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
    }

    public void UnlockAchievement(string id)
    {
        // unlock achievement 
        Social.ReportProgress(id, 100.0f, (bool success) => {
            // handle success or failure
        });
    }

    public void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(
        id, stepsToIncrement, (bool success) => {
            // handle success or failure
        });
    }

    public void showAchievementUI()
    {
     //show ui screen   
    }


    public void AddScoretoleaderboard(string leaderboardid, long score)
    {
        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        Social.ReportScore(score, leaderboardid, (bool success) => {
           
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
