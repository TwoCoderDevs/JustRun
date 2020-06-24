using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scores : MonoBehaviour
{

    public TextMeshProUGUI scoreText; 

    public int scores;
    public Animator scoreAnim;
    
    void Awake()
    {
        scoreText.text = "0";        
    }

    public void AddScore(int s){

        scores = scores + s;

        scoreAnim.SetTrigger("ScoreAdded");

        scoreText.text = scores.ToString();    
        PlayerPrefs.SetInt("Scores", scores);

        //GoogleLeaderboards.SetScore(PlayerPrefs.GetInt("Scores"));       

    }
}
