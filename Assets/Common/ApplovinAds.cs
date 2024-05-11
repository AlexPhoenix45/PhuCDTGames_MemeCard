using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplovinAds : IAd
{

    private string MaxSdkKey;
    private string InterstitialAdUnitId;
    private string RewardedAdUnitId;
    private string BannerAdUnitId;
    private string AppOpenAdUnitId;

    private bool isShowingAds = false;
    private int interstitialRetryAttempt;
    private int rewardedRetryAttempt;
    private int appopenRetryAttempt;


    public static Action<Action, float> DelayExecuteMethod;

    public bool IsShowingAds => isShowingAds;

    public void Init(bool isremoveads, Action onAppOpenAd, Action onInter, Action onRewardSuccess, Action onRewardFail, Action<MaxSdk.AdInfo> onRevenuePaid, Action onCompleteInitAd = null)
    {
        Resources.Load<AdConfig>("AdConfig").OnChangeAdsConfig(out BannerAdUnitId, out InterstitialAdUnitId, out RewardedAdUnitId, out AppOpenAdUnitId, out MaxSdkKey);
        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
            if (!isremoveads)
            {
                //InitializeAppOpen(onAppOpenAd, onRevenuePaid);
                // delay
                DelayExecuteMethod(() =>
                {
                    //InitializeBannerAds(onRevenuePaid);
                    InitializeInterstitialAds(onInter, onRevenuePaid);
                    InitializeRewardedAds(onRewardSuccess, onRewardFail, onRevenuePaid);
                    //InitializeBannerAds(onRevenuePaid);
                }, 3f);
            }
            else
            {
                InitializeRewardedAds(onRewardSuccess, onRewardFail, onRevenuePaid);
            }
        };

        MaxSdk.SetSdkKey(MaxSdkKey);
        MaxSdk.InitializeSdk();

        onCompleteInitAd?.Invoke();
    }
    #region AppOpenAds
    public void InitializeAppOpen(Action OnHidden, Action<MaxSdk.AdInfo> OnRevenuePaid)
    {
        MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += (arg1, arg2) =>
        {
            OnRevenuePaid(arg2);
        };
        MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += (arg1, arg2) =>
        {

            isShowingAds = false;
            OnHidden?.Invoke();
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        };
        MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += (arg1, arg2) =>
        {
            appopenRetryAttempt = 0;
        };
        MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += (arg1, arg2) =>
        {
            appopenRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, appopenRetryAttempt));
            try
            {
                DelayExecuteMethod(() =>
                {
                    MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
                }, (float)retryDelay);
            }
            catch (Exception)
            { }

        };
        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
    }

    public bool IsAppOpenAdsReady()
    {
        if (isShowingAds == true)
            return false;

        if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId) == false)
        {
            return false;
        }
        return true;
    }

    public void ShowAppOpenAds()
    {
        isShowingAds = true;
        MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
    }

    #endregion

    #region Interstitial Ad Methods

    public void InitializeInterstitialAds(Action onHiddentCallBack, Action<MaxSdk.AdInfo> OnRevenuePaid)
    {
        // Attach callbacks
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += (arg1, arg2) =>
        {
            interstitialRetryAttempt = 0;
        };
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += (arg1, arg2) =>
        {
            interstitialRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));
            try
            {
                //AdMgr.Instance.CallDeLayAction((float)retryDelay, LoadInterstitial);
                DelayExecuteMethod(() =>
                {
                    LoadInterstitial();
                }, (float)retryDelay);
            }
            catch (Exception) { }
        };
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += (arg1, arg2, arg3) =>
        {
            isShowingAds = false;
            // Interstitial ad failed to display. We recommend loading the next ad
            onHiddentCallBack?.Invoke();
            LoadInterstitial();
        };
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += (arg1, arg2) =>
        {
            isShowingAds = false;
            // Interstitial ad is hidden. Pre-load the next ad
            onHiddentCallBack?.Invoke();
            LoadInterstitial();
        };
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += (arg1, arg2) =>
        {
            OnRevenuePaid?.Invoke(arg2);
        };
        // Load the first interstitial
        LoadInterstitial();
    }

    void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(InterstitialAdUnitId);
    }

    public void ShowInter()
    {
        isShowingAds = true;
        MaxSdk.ShowInterstitial(InterstitialAdUnitId);
    }

    public bool IsInterReady()
    {
        return MaxSdk.IsInterstitialReady(InterstitialAdUnitId);
    }
    #endregion

    #region Rewarded Ad Methods

    bool isRewardReceived = false;
    public void InitializeRewardedAds(Action OnSuccess, Action OnFail, Action<MaxSdk.AdInfo> OnRevenuePaid)
    {
        // Attach callbacks
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += (arg1, arg2) =>
        {
            rewardedRetryAttempt = 0;
        };
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += (arg1, arg2) =>
        {
            rewardedRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));
            try
            {
                //AdMgr.Instance.CallDeLayAction((float)retryDelay, LoadRewardedAd);
                DelayExecuteMethod(() =>
                {
                    LoadRewardedAd();
                }, (float)retryDelay);
            }
            catch (Exception)
            { }
        };
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += (arg1, arg2, arg3) =>
        {
            isShowingAds = false;
            OnFail?.Invoke();
            LoadRewardedAd();
        };
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += (arg1, arg2) =>
        {
            isRewardReceived = false;
        };
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += (arg1, arg2) => { };
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += (arg1, arg2) =>
        {
            isShowingAds = false;
            LoadRewardedAd();
            if (isRewardReceived == false)
            {
                OnFail?.Invoke();
            }
        };
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += (arg1, arg2, arg3) =>
        {
            OnSuccess?.Invoke();
            isRewardReceived = true;
        };
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += (arg1, arg2) =>
        {
            OnRevenuePaid?.Invoke(arg2);
        };
        // Load the first RewardedAd
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        if (MaxSdk.IsRewardedAdReady(RewardedAdUnitId))
            return;
        MaxSdk.LoadRewardedAd(RewardedAdUnitId);
    }

    public bool IsRewardReady()
    {
        return MaxSdk.IsRewardedAdReady(RewardedAdUnitId);
    }

    public void ShowReward()
    {
        isShowingAds = true;
        MaxSdk.ShowRewardedAd(RewardedAdUnitId);
    }

    #endregion

    #region Banner Ad Methods

    public void InitializeBannerAds(Action<MaxSdk.AdInfo> OnRevenuePaid)
    {
        isBannerShowed = false;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += (arg1, arg2) =>
        {
            OnRevenuePaid?.Invoke(arg2);
        };
        // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
        MaxSdk.CreateBanner(BannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);
        //MaxSdk.SetBannerExtraParameter(BannerAdUnitId, "adaptive_banner", "true");
        // Set background or background color for banners to be fully functional.
        //MaxSdk.SetBannerBackgroundColor(BannerAdUnitId, new Color(0, 0, 0, 1));
        // set banner size
        //MaxSdk.SetBannerExtraParameter(BannerAdUnitId, "banner_size", "adaptive");
        MaxSdk.SetBannerWidth(BannerAdUnitId, 250);
        return;

        
    }

    static bool isBannerShowed = false;

    public void ShowBanner()//Action<float> callBack)
    {
        if (isBannerShowed)
            return;
        MaxSdk.ShowBanner(BannerAdUnitId);
        isBannerShowed = true;
    }

    public void HideBanner()
    {
        MaxSdk.HideBanner(BannerAdUnitId);
        isBannerShowed = false;
    }

    public void SetBannerPosition(MaxSdkBase.BannerPosition position)
    {
        MaxSdk.UpdateBannerPosition(BannerAdUnitId, position);
    }

    #endregion

    #region MRect 
    //#if UNITY_ANDROID
    //    void InitializeMRecAds()
    //    {
    //#if UNITY_ANDROID
    //        MaxSdk.CreateMRec(MRectAdUnitId_Anroid, MaxSdkBase.AdViewPosition.Centered);

    //        MaxSdkCallbacks.MRec.OnAdLoadedEvent += OnMRecAdLoadedEvent;
    //        MaxSdkCallbacks.MRec.OnAdLoadFailedEvent += OnMRecAdLoadFailedEvent;
    //        MaxSdkCallbacks.MRec.OnAdClickedEvent += OnMRecAdClickedEvent;
    //        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnMRecAdRevenuePaidEvent;
    //        MaxSdkCallbacks.MRec.OnAdExpandedEvent += OnMRecAdExpandedEvent;
    //        MaxSdkCallbacks.MRec.OnAdCollapsedEvent += OnMRecAdCollapsedEvent;

    //        //MaxSdk.LoadMRec(MRectAdUnitId_Anroid);
    //#endif
    //    }

    //    public void OnShowMRect()
    //    {
    //#if UNITY_ANDROID
    //        Debug.Log("Show");
    //        MaxSdk.ShowMRec(MRectAdUnitId_Anroid);
    //#endif
    //    }

    //    public void OnHideMRect()
    //    {
    //#if UNITY_ANDROID
    //        MaxSdk.HideMRec(MRectAdUnitId_Anroid);
    //#endif
    //    }

    //    //public float GetMRectScale()
    //    //{
    //    //    return MaxSdk.mre
    //    //}

    //    public void OnMRecAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    //    public void OnMRecAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo error) { }

    //    public void OnMRecAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }

    //    public void OnMRecAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
    //        OnViewAd(adInfo);
    //        OnSendAdRevenue(adInfo);
    //    }

    //    public void OnMRecAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {

    //    }

    //    public void OnMRecAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    //#endif
    #endregion

}

public interface IAd
{
    void Init(bool isremoveads, Action onAppOpenAd, Action onInter, Action onRewardSuccess, Action onRewardFail, Action<MaxSdk.AdInfo> onRevenuePaid, Action onCompleteInitAd);

    bool IsRewardReady();
    bool IsInterReady();

    void ShowInter();
    void ShowBanner();
    void ShowReward();
    void ShowAppOpenAds();

    void HideBanner();

    bool IsAppOpenAdsReady();

    bool IsShowingAds { get; }
}