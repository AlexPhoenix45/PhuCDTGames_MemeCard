using Assets.Core.Scripts;
using Assets.Core.Scripts.Core.Managers;
using Firebase.Analytics;
using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdmobBanner
{
    public class BannerState : MonoBehaviour
    {

        public static List<Tuple<GameObject, bool, AdPosition>> bannerController = new List<Tuple<GameObject, bool, AdPosition>>();
#if UNITY_ANDROID
        const string _adUnitId = "ca-app-pub-1061613090902017/1598355149";
#elif UNITY_IPHONE
  const string _adUnitId = "ca-app-pub-1061613090902017/9093701780";
#else
        const string _adUnitId = "unused";
#endif
        static BannerView bannerView;
        public bool bannerState;
        public AdPosition AdPosition = AdPosition.Bottom;


        private void OnEnable()
        {
            AddState(gameObject, bannerState, AdPosition);
        }

        public void OnDisable()
        { 
                RemoveState(gameObject); 
        }

        static void LoadLastState()
        {
            if (bannerController.Count == 0)
            {
                HideBanner();
                return;
            }
            Tuple<GameObject, bool, AdPosition> currentState = bannerController[bannerController.Count - 1];
            if (currentState.Item2)
            {
                //ServiceManager.applovinAds.ShowBanner();
                LoadAds();
                //ServiceManager.applovinAds.SetBannerPosition(currentState.Item3);
                bannerView.SetPosition(currentState.Item3);
            }
            else
                //ServiceManager.applovinAds.HideBanner();
                HideBanner();
        }
        public static void AddState(GameObject key, bool state, AdPosition position)
        {
            bannerController.Add(new Tuple<GameObject, bool, AdPosition>(key, state, position));
            LoadLastState();
        }

        public static void RemoveState(GameObject key)
        {
            for (int i = 0; i < bannerController.Count; i++)
            {
                if (bannerController[i].Item1 == key)
                {
                    bannerController.RemoveAt(i);
                    break;
                }
            }
            LoadLastState();
        }

        public static void LoadAds()
        {
            if (bannerView != null)
            {
                //bannerView.Destroy();
                //bannerView = null;
                bannerView.Show();
                return;
            }
            // create an instance of a banner view first. 
            bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
            //      bannerView.OnAdPaid -= Analytics_SendEvent_ViewAds;
            bannerView.OnAdPaid += Analytics_SendEvent_ViewAds;
            // create our request used to load the ad.
            var adRequest = new AdRequest(); 

            // send the request to load the ad.
            Debug.Log("Loading banner ad.");
            adRequest.Extras.Add("collapsible", "bottom");
            adRequest.Extras.Add("collapsible_request_id", Guid.NewGuid().ToString());
            bannerView.LoadAd(adRequest); 
        }

        public static void Analytics_SendEvent_ViewAds(AdValue arg2)
        {
            try
            {
                var impressionParameters = new[]
                {
                    new Parameter("ad_platform", "GoogleAdMobSDK"),
                    new Parameter("ad_source", "Google AdMob"),
                    new Parameter("ad_unit_name", _adUnitId),
                    new Parameter("ad_format", "collapsible banner"),
                    new Parameter("value", arg2.Value / 1000000f),
                    new Parameter("currency", arg2.CurrencyCode), // All AppLovin revenue is sent in USD
                };

                FirebaseAnalytics.LogEvent("ad_impression", impressionParameters);
            }

            catch (Exception)
            {
            }
        }

        public static void HideBanner()
        {
            if (bannerView != null)
            {
                bannerView.Hide();
                //bannerView.Destroy();
            }
        }

        public void ShowBanner()
        {
            LoadAds();
            //bannerView.Show();
        }
    }
}