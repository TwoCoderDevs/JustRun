using System.Collections;
using CloudOnce;
using UnityEngine;
using TMPro;

public class GoogleLeaderboards : MonoBehaviour
{
    public static GoogleLeaderboards instance;
    // Start is called before the first frame update
    void Awake()
    {
        //Cloud.OnInitializeComplete += CloudServiceInitialized;
        Cloud.Initialize(false, true);        
    }

    private void Start() {
        if(instance == null) instance = this;
        if(instance != this) Destroy(gameObject);

        //SetScore(PlayerPrefs.GetInt("Scores"));
    }

    public void CloudServiceInitialized(){
        //Cloud.OnInitializeComplete -= CloudServiceInitialized;
        Debug.LogWarning("Initialized");
    }

    public void SetScore(int score){
        //Debug.Log("Fuck!");
        Leaderboards.HighScores.SubmitScore(score);

        if(score == 5) Achievements.Beginner.Unlock();
        if(score == 10) Achievements.Intermediate.Unlock();
        if(score == 15) Achievements.Prodigy.Unlock();
        if(score == 20) Achievements.Savant.Unlock();
        if(score == 30) Achievements.EscapeMaster.Unlock();
        if(score == 50) Achievements.SuperHero.Unlock();
        if(score == 75) Achievements.NextToGod.Unlock();
        if(score == 100) Achievements.TheGodRunner.Unlock();
    }
}
