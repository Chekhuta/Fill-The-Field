using System;
using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardAd;
    private AdPauseWindow adPauseWindow;
    private static AdManager instance;
    private const string BANNER_ID = "-";
    private const string INTERSTITIAL_ID = "-";
    private const string REWARDED_ID = "-";

    private int levelCompleted = 0;
    private float adDelayTime = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        adPauseWindow = GetComponent<AdPauseWindow>();
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
        RequestInterstitial();
        RequestRewardVideo();
    }

    private void Update()
    {
        adDelayTime += Time.deltaTime;
    }

    private void RequestInterstitial()
    {
        interstitialAd = new InterstitialAd(INTERSTITIAL_ID);
        interstitialAd.OnAdLoaded += HandleInterstitialOnAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleInterstitialOnAdFailedToLoad;
        interstitialAd.OnAdClosed += HandleInterstitialOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    public void RequestRewardVideo()
    {
        rewardAd = new RewardedAd(REWARDED_ID);
        rewardAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
        rewardAd.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
        rewardAd.OnAdClosed += HandleRewardBasedVideoClosed;

        AdRequest request = new AdRequest.Builder().Build();
        rewardAd.LoadAd(request);
    }

    public void DisplayInterstitial()
    {
        if (!interstitialAd.IsLoaded())
        {
            return;
        }
        if ((adDelayTime > 140 && levelCompleted >= 2) || levelCompleted >= 6)
        {
            StartCoroutine(ShowInterstitialAd());
        }
    }

    private IEnumerator ShowInterstitialAd()
    {
        adPauseWindow.OpenWindow();
        yield return new WaitForSeconds(1f);
        interstitialAd.Show();
        adPauseWindow.CloseWindow();
        adDelayTime = 0;
        levelCompleted = 0;
    }

    public void DisplayRewardVideo()
    {
        if (rewardAd.IsLoaded())
        {
            rewardAd.Show();
        }
    }

    public bool IsLoadRewardVideo()
    {
        if (rewardAd != null)
        {
            return rewardAd.IsLoaded();
        }
        else
        {
            return false;
        }
    }

    public static AdManager GetInstance()
    {
        return instance;
    }

    public void UpdateDelay()
    {
        levelCompleted++;
        DisplayInterstitial();
    }

    private void RequestBanner()
    {
        bannerView = new BannerView(BANNER_ID, AdSize.Banner, AdPosition.Bottom);
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        bannerView.OnAdOpening += HandleOnAdOpened;
        bannerView.OnAdClosed += HandleOnAdClosed;
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
    }

    public void HandleInterstitialOnAdLoaded(object sender, EventArgs args)
    {
    }


    public void HandleInterstitialOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public void HandleInterstitialOnAdClosed(object sender, EventArgs args)
    {
        interstitialAd.OnAdLoaded -= HandleInterstitialOnAdLoaded;
        interstitialAd.OnAdFailedToLoad -= HandleInterstitialOnAdFailedToLoad;
        interstitialAd.OnAdClosed -= HandleInterstitialOnAdClosed;
        interstitialAd.Destroy();
        RequestInterstitial();
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        rewardAd.OnAdLoaded -= HandleRewardBasedVideoLoaded;
        rewardAd.OnUserEarnedReward -= HandleRewardBasedVideoRewarded;
        rewardAd.OnAdClosed -= HandleRewardBasedVideoClosed;
        RequestRewardVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Hint.GetInstance().AddHints();
    }
}
