

using System;
using UnityEngine;
using Zenject;
using GoogleMobileAds.Api;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;

namespace Daxi.InfrastructureLayer.Ads
{
    public class AdsManager : IInitializable
    {

        #region Fields
        private RewardedAd _rewardedAd;

        private InterstitialAd _interstitialAd;

        [Inject(Id ="Production")]
        private bool _production;


        public bool RewardedAdLoaded => _rewardedAd==null?false:true;
        public bool InterstitialAdLoaded => _interstitialAd == null ? false : true;
        #endregion

        #region Methods
        public   void Initialize()
        {
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            MobileAds.Initialize((status) => 
            {
                Debug.Log("ads initialized");
                RequestConfiguration requestConfiguration = new RequestConfiguration
                {
                    TagForChildDirectedTreatment = TagForChildDirectedTreatment.True,
                    TagForUnderAgeOfConsent= TagForUnderAgeOfConsent.True
                    
                };
                MobileAds.SetRequestConfiguration(requestConfiguration);
            });
           


        }
        #region Interstitial

        public void LoadInterstitialAd()
        {

            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

           
            var adRequest = new AdRequest();            
            adRequest.Keywords.Add("unity-admob-sample");
            string id = _production ? "ca-app-pub-9057464848725092/3820779178" : "ca-app-pub-3940256099942544/1033173712";

            InterstitialAd.Load(id, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.Log("Interstitial ad failed to load" + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded !!" + ad.GetResponseInfo());

                _interstitialAd = ad;
                InterstitialEvent(_interstitialAd);
            });

        }
        public void ShowInterstitialAd()
        {

            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                _interstitialAd.Show();
            }
            else
            {
                Debug.Log("Intersititial ad not ready!!");
            }
        }
        public void InterstitialEvent(InterstitialAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log("Interstitial ad paid {0} {1}." +
                    adValue.Value +
                    adValue.CurrencyCode);
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                _interstitialAd.Destroy();
                _interstitialAd= null;
                Debug.Log("Interstitial ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
            };
        }

        #endregion

        #region Rewarded

        public void Load_rewardedAd(Action<bool> callback)
        {

            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");
            string id = _production ? "ca-app-pub-9057464848725092/9636992887" : "ca-app-pub-3940256099942544/5224354917";
           RewardedAd.Load(id, adRequest, (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.Log("Rewarded failed to load" + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded !!");
                _rewardedAd = ad;
                _rewardedAdEvents(_rewardedAd,callback);
                
            });
        }
        public void Show_rewardedAd()
        {

            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((Reward reward) =>
                {
                    Debug.Log("Give reward to player !!");


                });
            }
            else
            {
                Debug.Log("Rewarded ad not ready");
            }
        }
        public void _rewardedAdEvents(RewardedAd ad, Action<bool> callback)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
                
                callback.Invoke(true);
            };
            // Raised when an impression is recorded for an ad.
            ad.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Rewarded ad recorded an impression.");
            };
            // Raised when a click is recorded for an ad.
            ad.OnAdClicked += () =>
            {
                Debug.Log("Rewarded ad was clicked.");
            };
            // Raised when an ad opened full screen content.
            ad.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Rewarded ad full screen content opened.");
            };
            // Raised when the ad closed full screen content.
            ad.OnAdFullScreenContentClosed += () =>
            {
                callback.Invoke(false);
                _rewardedAd.Destroy();
                _rewardedAd=null;
                Debug.Log("Rewarded ad full screen content closed.");
            };
            // Raised when the ad failed to open full screen content.
            ad.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Rewarded ad failed to open full screen content " +
                               "with error : " + error);
            };
        }

        #endregion


       
        #endregion
    }
}
