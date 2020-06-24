using System.Collections;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class Admob : MonoBehaviour
{
    public bool testing;
    [SerializeField] private string AppID;
    public BannerView bannerView;
    public InterstitialAd interstitialAd;
    public RewardedAd rewardedAd;
    public static bool gameStarted;
    public GameObject adIsntLoaded;

    [SerializeField] private string bannerAdID = "ca-app-pub-5025574143212384/3498341514";
    [SerializeField] private string interstitialAdID = "ca-app-pub-5025574143212384/6296088762";
    [SerializeField] private string rewardedAdID = "ca-app-pub-5025574143212384/8198335553";

    public static Admob instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);

        DontDestroyOnLoad(instance);
        
        MobileAds.Initialize(initStatus => {});   

        RequestInerstitialAd();
        RequestRewardedAd();     
    }

    private void OnEnable() {
        ShowInterstitial();
    }

    /*private void RequestBanner(){

        if(bannerView != null) bannerView.Destroy();

        if(!testing) bannerView = new BannerView(bannerAdID, AdSize.Banner, AdPosition.Top);
        else bannerView = new BannerView("ca-app-pub-3940256099942544/6300978111", AdSize.Banner, AdPosition.Top);

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);     
    }

    public void ShowBannerAd(){
        
    }*/

    private void RequestInerstitialAd(){

        if(interstitialAd != null) interstitialAd.Destroy();

        if(!testing) interstitialAd = new InterstitialAd(interstitialAdID);
        else interstitialAd = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");

        interstitialAd.OnAdClosed += HandleOnAdClosedIntersttial;

        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);          
    }

    public void ShowInterstitial(){
        if(interstitialAd.IsLoaded()){
            interstitialAd.Show();
        }
        else{
            RequestInerstitialAd();
        }
    }

    public void HandleOnAdClosedIntersttial(object sender, EventArgs args)
    {
        RequestInerstitialAd();    
    }

    private void RequestRewardedAd(){ 

        if(!testing) rewardedAd = new RewardedAd(rewardedAdID);
        else rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");

        rewardedAd.OnAdClosed += HandleOnAdClosedRewarded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);          
    }

    public void HandleOnAdClosedRewarded(object sender, EventArgs args)
    {
        RequestRewardedAd();    
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if(gameStarted){
            GameOverScreen.instance.RewardResult();
        }   
        else {
            ShowAds.instance.GetReward();// = true;            
        }     
    }

    public void ShowRewardedAd(){
        if(rewardedAd.IsLoaded()){
            rewardedAd.Show();
        }
        else{
            RequestRewardedAd();
        }
    }

   
}
