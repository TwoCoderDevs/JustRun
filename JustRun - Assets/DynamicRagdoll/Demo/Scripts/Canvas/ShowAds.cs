using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAds : MonoBehaviour
{
    public static ShowAds instance;
    public GameObject rewardPanel;
    public GameObject adPanel;
    public void ShowRewardedAd(){
        if(instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);
        
        Admob.instance.ShowRewardedAd();
    }

    public void GetReward(){
        adPanel.SetActive(false);
        rewardPanel.SetActive(true);
    }

    public void Claim(){
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 10);
    }
}
