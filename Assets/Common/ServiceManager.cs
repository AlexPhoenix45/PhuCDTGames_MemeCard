
using com.adjust.sdk;
using Firebase;
using Firebase.Analytics;
using System;
using System.Collections.Generic;
using UnityEngine;
using static MaxSdkBase;

namespace Assets.Core.Scripts.Core.Managers
{
    public class ServiceManager : MonoBehaviour
    {
        public static ServiceManager Instance { get; private set; }
        public float interDelayTime = 46f;
        public float minDelayApterReward = 30f;
#if false
        public bool IsStaging => true;
#else
        public bool IsStaging => isStaging;
#endif
        public bool isStaging = false;
        public static ApplovinAds applovinAds;
        public static FirebaseApp firebaseApp;
        public static bool failedInitFirebase = false;

        public static Action<bool> OnRewardAction;
        public static Action OnInterAction;
        public static Action OnAppOpenAction;


        public static bool IsInterReady => applovinAds.IsInterReady();
        public static bool IsRewardReady => applovinAds.IsRewardReady();
        public static bool IsFirebaseReady => firebaseApp != null;

        public void Init()
        {
            Instance = this;
            try
            {
                AdInit();

                FirebaseInit();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }


        }
        void FirebaseInit()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    firebaseApp = Firebase.FirebaseApp.DefaultInstance;
                    OnFirebaseReady();

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    failedInitFirebase = true;
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
        void AdInit()
        {
            applovinAds = new ApplovinAds();
            ApplovinAds.DelayExecuteMethod = DelayMethod;
            applovinAds.Init(false, OnAppOpenAds, OnInterHide, OnRewardSuccess, OnRewardFail, OnRevenuePaid,
                () =>
                {
                    Debug.Log("Applovin Ads Initialized");
                });
        }

        public void DelayMethod(Action action, float delay)
        {
            action.Invoke();
        }

        public static void OnAppOpenAds()
        {
            OnAppOpenAction?.Invoke();
        }

        public static void OnInterHide()
        {
            OnInterAction?.Invoke();
            SetCounterInter();
        }

        public static void OnRewardSuccess() => OnReward(true);

        public static void OnRewardFail() => OnReward(false);

        public static void OnReward(bool isSuccess)
        {
            OnRewardAction?.Invoke(isSuccess);
            interReadyTime = Mathf.Max(interReadyTime, Time.unscaledTime + Instance.minDelayApterReward);
        }

        public static void OnRevenuePaid(AdInfo adInfo)
        {

            Analytics_SendEvent_ViewAds(adInfo);
            try
            {
                //AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue("source");
                //adjustAdRevenue.setRevenue(adInfo.Revenue, "USD");
                //adjustAdRevenue.setAdRevenueNetwork(adInfo.NetworkName);
                //adjustAdRevenue.setAdRevenuePlacement(adInfo.Placement);
                //adjustAdRevenue.setAdRevenueUnit(adInfo.AdUnitIdentifier);
                //Adjust.trackAdRevenue(adjustAdRevenue);
                AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
                adjustAdRevenue.setRevenue(adInfo.Revenue, "USD");
                adjustAdRevenue.setAdRevenueNetwork(adInfo.NetworkName);
                adjustAdRevenue.setAdRevenueUnit(adInfo.AdUnitIdentifier);
                adjustAdRevenue.setAdRevenuePlacement(adInfo.Placement);
                Adjust.trackAdRevenue(adjustAdRevenue);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            //TODO
        }

        static float interReadyTime = 0;

        public static void ShowInter(Action onInterHide = null)
        {
            if (Instance.IsStaging)
            {
                onInterHide?.Invoke();
                return;
            }
            bool canShow = IsTimerInterReady;
            canShow = canShow && applovinAds.IsInterReady();
            if (!canShow)
            {
                onInterHide?.Invoke();
                return;
            }
            OnInterAction = onInterHide;
                applovinAds.ShowInter();
            SetCounterInter();
            ServiceManager.TryLog("inter_shown");
        }

        public static void SetCounterInter()
        {
            interReadyTime = Time.unscaledTime + Instance.interDelayTime;
        }

        public static bool IsTimerInterReady => interReadyTime < Time.unscaledTime;

        public static bool ShowReward(Action<bool> onReward = null)
        {
            if (Instance.IsStaging)
            {

                return true;
            }
            if (!applovinAds.IsRewardReady())
            {

                return false;
            }
            OnRewardAction = onReward;
            applovinAds.ShowReward();
            ServiceManager.TryLog("reward_shown");
            return true;
        }

        public static void ShowAppOpen(Action onAppOpen = null)
        {
            OnAppOpenAction = onAppOpen;
            applovinAds.ShowAppOpenAds();
        }


        public static void ShowBanner()
        {
            if (Instance.IsStaging)
                return;
            applovinAds.ShowBanner();
        }

        static Queue<Action> waitingEvents = new Queue<Action>();

        public static void TryLog(string message, params Parameter[] parameters)
        {
            if (failedInitFirebase)
                return;
            if (IsFirebaseReady)
            {
                FirebaseAnalytics.LogEvent
                    (message, parameters);
            }
            else
            {
                waitingEvents.Enqueue(() => FirebaseAnalytics.LogEvent(message, parameters));
            }
        }

        public static void TryLog(string message, string key, int value)
        {
            if (failedInitFirebase)
                return;
            if (IsFirebaseReady)
            {
                FirebaseAnalytics.LogEvent(message, key, value);
            }
            else
            {
                waitingEvents.Enqueue(() => FirebaseAnalytics.LogEvent(message, key, value));
            }
        }
        public static void TryLog(string message, string key, string value)
        {
            if (failedInitFirebase)
                return;
            if (IsFirebaseReady)
            {
                FirebaseAnalytics.LogEvent(message, key, value);
            }
            else
            {
                waitingEvents.Enqueue(() => FirebaseAnalytics.LogEvent(message, key, value));
            }
        }

        public static void TryLog(string message)
        {
            if (failedInitFirebase)
                return;
            if (IsFirebaseReady)
            {
                FirebaseAnalytics.LogEvent(message);
            }
            else
            {
                waitingEvents.Enqueue(() => FirebaseAnalytics.LogEvent(message));
            }
        }

        public static void OnFirebaseReady()
        {
            while (waitingEvents.Count > 0)
            {
                waitingEvents.Dequeue().Invoke();
            }
        }

        public static void Analytics_SendEvent_ViewAds(AdInfo arg2)
        {
            //Debug.Log("_Revenue: " + arg2.Revenue);
            try
            {

                var impressionParameters = new[]
                {
                new Parameter("ad_platform", com.adjust.sdk.AdjustConfig.AdjustAdRevenueSourceAppLovinMAX),
                new Parameter("ad_source", arg2.NetworkName),
                new Parameter("ad_unit_name", arg2.AdUnitIdentifier),
                new Parameter("ad_format", arg2.AdFormat),
                new Parameter("value", arg2.Revenue),
                new Parameter("currency", "USD"), // All AppLovin revenue is sent in USD
            };
                TryLog("ad_impression", impressionParameters);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

    }


}