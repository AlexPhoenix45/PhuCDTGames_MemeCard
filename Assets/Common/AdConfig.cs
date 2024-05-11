#define Admob
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AdConfig", menuName = "Config/AdConfigMax", order = 1)]
public class AdConfig : ScriptableObject
{
    private const string MobileAdsSettingsResDir = "Assets/GoogleMobileAds/Resources";
    private const string MobileAdsSettingsFile = "GoogleMobileAdsSettings";
    private const string AppLovinSettingsFile = "AppLovinSettings";

    //[SerializeField] private bool IsContainAdmob;
    [SerializeField] private int AdTimeDelay;
    public int AdDelay
    {
        get
        {
            return AdTimeDelay;
        }
    }

    [SerializeField] private string MaxSDKKey;
    [SerializeField] private string AdmobSDKKey_Android;
    [SerializeField] private string AdmobSDKKey_IOS;

    [Header("Android-Key")]
    [SerializeField] private string Android_Banner;
    [SerializeField] private string Android_Inter;
    [SerializeField] private string Android_Reward;
    [SerializeField] private string Android_AppOpenAds;

    public string AndroidBanner
    {
        get
        {
            return Android_Banner;
        }
    }

    public string AndroidInter
    {
        get
        {
            return Android_Inter;
        }
    }

    public string AndroidReward
    {
        get
        {
            return Android_Reward;
        }
    }

    public string AndroidAppOpenAds
    {
        get
        {
            return Android_AppOpenAds;
        }
    }
    [Header("IOS-Key")]
    [SerializeField] private string IOS_Banner;
    [SerializeField] private string IOS_Inter;
    [SerializeField] private string IOS_Reward;
    [SerializeField] private string IOS_AppOpenAds;
    public string IOSBanner
    {
        get
        {
            return IOS_Banner;
        }
    }

    public string IOSInter
    {
        get
        {
            return IOS_Inter;
        }
    }

    public string IOSReward
    {
        get
        {
            return IOS_Reward;
        }
    }

    public string IOSAppOpenAds
    {
        get
        {
            return IOS_AppOpenAds;
        }
    }

    public void OnChangeAdsConfig(out string _banner, out string _inter, out string _reward, out string _appopen, out string _key)
    {
        _key = MaxSDKKey;
#if UNITY_IOS
        _banner = IOS_Banner;
        _inter = IOS_Inter;
        _reward = IOS_Reward;
        _appopen = IOS_AppOpenAds;
#else
        _banner = Android_Banner;
        _inter = Android_Inter;
        _reward = Android_Reward;
        _appopen = Android_AppOpenAds;
#endif
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
#if UNITY_EDITOR
        //if(IsContainAdmob)
        //{

        //}
#endif
        AppLovinSettings maxSetting = Resources.Load<AppLovinSettings>(AppLovinSettingsFile);
        maxSetting.UserTrackingUsageLocalizationEnabled = true;
        maxSetting.SdkKey = MaxSDKKey;

        maxSetting.AdMobAndroidAppId = AdmobSDKKey_Android;
        maxSetting.AdMobIosAppId = AdmobSDKKey_IOS;
    }
#endif
}
