

using System;
using UnityEngine;
using Zenject;
using GoogleMobileAds.Api;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Daxi.InfrastructureLayer.Ads
{
    public class AdsManager : IInitializable
    {

        #region Fields
        private RewardedAd _rewardedAd;

        private InterstitialAd _interstitialAd;

        [Inject(Id ="Production")]
        private bool _production;

        private bool _rewardedAdClosed;

        private bool _interstitialClosed;

        public bool RewardedAdClosed => _rewardedAdClosed; 
        public bool InterstitialClosed=> _interstitialClosed; 
        #endregion

        #region Methods
        public   void Initialize()
        {        
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            RequestConfiguration requestConfiguration = new RequestConfiguration
            {
                MaxAdContentRating = MaxAdContentRating.G,
                TagForChildDirectedTreatment = TagForChildDirectedTreatment.True,                
            };     
            MobileAds.SetRequestConfiguration(requestConfiguration);
            
            MobileAds.Initialize((status) => 
            {
                Debug.Log("ads initialized");
                LoadInterstitialAd();
                Load_rewardedAd();

            });
           
           

        }       
       
        #region Interstitial

        public void LoadInterstitialAd()
        {

          

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
        public bool ShowInterstitialAd()
        {
            if (_interstitialAd == null || !_interstitialAd.CanShowAd())
            {
                return false;
            }
            _interstitialClosed = false;           
            _interstitialAd.Show();  
            return true;
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
                _interstitialClosed = true;
                _interstitialAd.Destroy();
                LoadInterstitialAd();
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

        public void Load_rewardedAd()
        {

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
                _rewardedAdEvents(_rewardedAd);
                
            });
        }
        public bool Show_rewardedAd(Action OnRewardRecived)
        {
            if(_rewardedAd==null||! _rewardedAd.CanShowAd())
            {
                return false;
            }
            _rewardedAdClosed = false;
            _rewardedAd.Show((Reward reward) =>
            {               
                OnRewardRecived.Invoke();
            });
            return true;
        }
        public void _rewardedAdEvents(RewardedAd ad)
        {
            // Raised when the ad is estimated to have earned money.
            ad.OnAdPaid += (AdValue adValue) =>
            {
              
              
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
               
                ad.Destroy();
                Load_rewardedAd();
                _rewardedAdClosed = true;
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
