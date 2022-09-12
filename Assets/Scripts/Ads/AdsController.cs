using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AdsController //: BaseController
{
    private AdsType ADS_TYPE_ENABLED = Application.systemLanguage == SystemLanguage.Russian ? AdsType.Yandex : AdsType.Admob;
    //APPLOVIN
    private const string ADS_BANNER_APPLOVIN = "b34139deba3d5c39";
    private const string ADS_WITH_REWARD_APPLOVIN = "72a8d8cdc16cd557";
    private const string ADS_INTERSTITIAL_APPLOVIN = "c276d69d36225c07";

    //ADMOB
    private const string ADS_BANNER = "ca-app-pub-3270284591433633/9944830986";
    private const string ADS_WITH_REWARD = "ca-app-pub-3270284591433633/6225325241";
    private const string ADS_INTERSTITIAL = "ca-app-pub-3270284591433633/2783692117";

    //YANDEX
    private const string ADS_BANNER_YANDEX = "R-M-1939154-1";
    private const string ADS_WITH_REWARD_YANDEX = "R-M-1939154-2";
    private const string ADS_INTERSTITIAL_YANDEX = "R-M-1939154-3";

    private YandexMobileAds.Banner _yandexBanner;

    private BannerView _banner;
    private InterstitialAd _interstitialAd;
    private RewardedInterstitialAd _interstitialAdWithReward;
    private RewardedAd _rewardedAd;
    private bool _isAdsEnable = true;
    private bool _isAppLovinBannerCreated;

    public bool IsAdsEnable
    {
        get
        {
            return _isAdsEnable;
        }
        set
        {
            _isAdsEnable = value;

            if (!value)
                DestroyAdsBanner();
        }
    }
    public  void Initialise()
    {
        switch (ADS_TYPE_ENABLED)
        {
            case AdsType.Admob:
                AdmobInitialization();
                break;

            //case AdsType.AppLovin:
            //    AppLovinInitialisation();
            //    break;
            case AdsType.Yandex:
                YandexAdsInitialization();
                break;
            default:
                break;
        }
      //  base.Initialise();
    }
    //public override void Execute()
    //{
    //}
    public  void Clear()
    {
        DestroyAdsBanner();
        _interstitialAd?.Destroy();
        _interstitialAd = null;
    }

    //public override void Dispose()
    //{
    //}

    public void AdmobCreateBanner()
    {
        if (!IsAdsEnable)
            return;
        _banner = new BannerView(ADS_BANNER, AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth / 2), AdPosition.Bottom);
        var request = new AdRequest.Builder().Build();
        _banner.LoadAd(request);
        _banner.Hide();
    }

    public void BannerHide()
    {
        switch (ADS_TYPE_ENABLED)
        {
            case AdsType.Admob:
                AdMobBannerHide();
                break;
            //case AdsType.AppLovin:
            //    ApplovinBannerHide();
            //    break;
            case AdsType.Yandex:
                YandexHideBanner();
                break;
            default:
                break;
        }
    }

    public void BannerShow()
    {
        if (!IsAdsEnable)
            return;

        switch (ADS_TYPE_ENABLED)
        {
            case AdsType.Admob:
                AdmobBannerShow();
                break;
            //case AdsType.AppLovin:
            //    ApplovinBannerShow();
            //    break;
            case AdsType.Yandex:
                YandexBannerShow();
                break;
            default:
                break;
        }
    }

    public void DestroyAdsBanner()
    {
        switch (ADS_TYPE_ENABLED)
        {
            case AdsType.Admob:
                AdmobDestroyBanner();
                break;
            //case AdsType.AppLovin:
            //    ApplovinDestroyBanner();
            //    break;
            case AdsType.Yandex:
                YandexDestroyBanner();
                break;
            default:
                break;
        }
    }

    public void ShowInterstitial()
    {
        if (!IsAdsEnable)
            return;

        switch (ADS_TYPE_ENABLED)
        {
            case AdsType.Admob:
                AdmobShowInterstitial();
                break;
            //case AdsType.AppLovin:
            //    ApplovinShowInterstitial();
            //    break;

            case AdsType.Yandex:
                YandexShowInterstitial();
                break;
            default:
                break;
        }
    }
    public void ShowRewaded(Action onDone)
    {
        if (!IsAdsEnable)
            return;

        switch (ADS_TYPE_ENABLED)
        {
            case AdsType.Admob:
                AdmobRandomRewardedOrInterstitialRewarded();
                break;
            //case AdsType.AppLovin:
            //    ApplovinShowRewarded(() =>
            //    {
            //        onDone?.Invoke();
            //    });
            //    break;
            case AdsType.Yandex:
                YandexShowRewarded(onDone);
                break;
            default:
                break;
        }
        void AdmobRandomRewardedOrInterstitialRewarded()
        {
            //var rnd = Random.Range(0f, 1f);

            //if (rnd < 0.5f)
            //{
            //    AdmobShowInterstitialRewarded(() =>
            //    {
            //        onDone?.Invoke();
            //    });
            //}
            //else
            //{
                AdmobShowRewarded(() =>
                {
                    onDone?.Invoke();
                });
            //}
        }
    }

    ///////////////////////////////////////////////
    #region Admob Ads

    private void AdmobInitialization()
    {
        MobileAds.Initialize(status => { });
        CreateAdsInterstitial();
     //   CreateAdsInterstitialWithReward();
        CreateAdsRewarded();
    }

    private void AdMobBannerHide()
    {
        if (_banner != null)
            _banner.Hide();
    }

    public void AdmobBannerShow()
    {
        if (_banner == null)
            AdmobCreateBanner();

        _banner?.Show();
    }

    public void AdmobDestroyBanner()
    {
        _banner?.Destroy();
        _banner = null;
    }

    public void CreateAdsInterstitial()
    {
        _interstitialAd = new InterstitialAd(ADS_INTERSTITIAL);
        var request = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(request);
    }


    public void AdmobShowInterstitial()
    {
        if (_interstitialAd == null)
            CreateAdsInterstitial();
        if (_interstitialAd.IsLoaded())
            _interstitialAd.Show();
    }

    //public void CreateAdsInterstitialWithReward()
    //{
    //    var request = new AdRequest.Builder().Build();
    //    RewardedInterstitialAd.LoadAd(ADS_INTERSTITIAL_WITH_REWARD, request, (RewardedInterstitialAd, error) =>
    //   {
    //       if (error == null)
    //       {
    //           _interstitialAdWithReward = RewardedInterstitialAd;
    //       }
    //   });
    //}
    //public void AdmobShowInterstitialRewarded(Action onDone = null)
    //{
    //    _interstitialAdWithReward.Show((Reward) =>
    //    {
    //        IsAdsEnable = false;
    //        onDone?.Invoke();
    //    });
    //}
    public void CreateAdsRewarded()
    {
        _rewardedAd = new RewardedAd(ADS_WITH_REWARD);
        var request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);

    }

    public void AdmobShowRewarded(Action onDone = null)
    {
        _rewardedAd.Show();

        _rewardedAd.OnUserEarnedReward += (object sender, Reward e) =>
        {
            IsAdsEnable = false;
            onDone?.Invoke();
        };
    }

    #endregion

    #region AppLovinAds
    //private void AppLovinInitialisation()
    //{
    //    MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
    //    {
    //        Debug.Log("AppLovin SDK is initialized, start loading ads");
    //        MaxSdk.ShowMediationDebugger();

    //    };

    //    MaxSdk.SetSdkKey("0TlcSSJF7sT89g1HJ1QK-LzurmEMX47Thf8aUlXeKb-_HR6ttAagg_xqcRTz_3lScXyf9Q-DFQQNn5t2b2_ATN");
    //    MaxSdk.SetUserId("FigureCards");
    //    MaxSdk.InitializeSdk();

    //    MaxSdk.LoadInterstitial(ADS_INTERSTITIAL_APPLOVIN);
    //    MaxSdk.LoadRewardedAd(ADS_WITH_REWARD_APPLOVIN);

    //    MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += Rewarded_OnAdLoadedEvent;
    //    MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += Rewarded_OnAdLoadFailedEvent;
    //    MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += Interstitial_OnAdLoadFailedEvent;
    //    MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += Banner_OnAdLoadFailedEvent;

    //}

    //private void Banner_OnAdLoadFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2)
    //{
    //    Debug.LogError($"fail : {arg1}, CODE: {arg2.Code}, Message{arg2.Message}, info: {arg2.AdLoadFailureInfo}");
    //}

    //private void Interstitial_OnAdLoadFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2)
    //{
    //    Debug.LogError($"fail : {arg1}, CODE: {arg2.Code}, Message{arg2.Message}, info: {arg2.AdLoadFailureInfo}");
    //}

    //private void Rewarded_OnAdLoadFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2)
    //{
    //    Debug.LogError($"fail {arg1}, CODE: {arg2.Code}, Message{arg2.Message}, info: {arg2.AdLoadFailureInfo}");
    //}

    //private void Rewarded_OnAdLoadedEvent(string arg1, MaxSdkBase.AdInfo arg2)
    //{
    //    Debug.Log("loaded");
    //}

    //public void ApplovinBannerShow()
    //{
    //    if (!_isAppLovinBannerCreated)
    //    {
    //        MaxSdk.CreateBanner(ADS_BANNER_APPLOVIN, MaxSdkBase.BannerPosition.BottomCenter);
    //        MaxSdk.SetBannerExtraParameter(ADS_BANNER_APPLOVIN, "adaptive_banner", "false");
    //    }
    //    MaxSdkCallbacks.Banner.OnAdLoadedEvent += (string arg1, MaxSdkBase.AdInfo arg2) =>
    //    {
    //        _isAppLovinBannerCreated = true;
    //    };
    //    MaxSdk.ShowBanner(ADS_BANNER_APPLOVIN);
    //}

    //public void ApplovinBannerHide()
    //{
    //    MaxSdk.HideBanner(ADS_BANNER_APPLOVIN);
    //}
    //public void ApplovinDestroyBanner()
    //{
    //    MaxSdk.DestroyBanner(ADS_BANNER_APPLOVIN);
    //    _isAppLovinBannerCreated = false;
    //}

    //public void ApplovinShowInterstitial()
    //{
    //    if (MaxSdk.IsInterstitialReady(ADS_INTERSTITIAL_APPLOVIN))
    //    {
    //        MaxSdk.ShowInterstitial(ADS_INTERSTITIAL_APPLOVIN);
    //    }
    //    MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += (string arg1, MaxSdkBase.AdInfo arg2) =>
    //     {
    //         MaxSdk.LoadInterstitial(ADS_INTERSTITIAL_APPLOVIN);
    //     };
    //}

    //public void ApplovinShowRewarded(Action onDone = null)
    //{
    //    if (MaxSdk.IsRewardedAdReady(ADS_WITH_REWARD_APPLOVIN))
    //    {
    //        MaxSdk.ShowRewardedAd(ADS_WITH_REWARD_APPLOVIN);
    //    }

    //    MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += (string arg1, MaxSdkBase.Reward arg2, MaxSdkBase.AdInfo arg3) =>
    //    {
    //        IsAdsEnable = false;
    //        onDone?.Invoke();
    //    };
    //    MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += (string arg1, MaxSdkBase.AdInfo arg) =>
    //    {
    //        if (IsAdsEnable)
    //            MaxSdk.LoadRewardedAd(ADS_WITH_REWARD_APPLOVIN);
    //    };
    //}

    #endregion

    #region Yandex Ads

    private void YandexAdsInitialization()
    {
        //
    }

    private int GetScreenWidthDp()
    {
        int screenWidth = (int)Screen.safeArea.width;
        return YandexMobileAds.ScreenUtils.ConvertPixelsToDp(screenWidth) / 2 ;
    }

    private void YandexBannerShow()
    {
        if (_yandexBanner == null)
            YandexCreateBanner();

        _yandexBanner.Show();
    }
    private void YandexCreateBanner()
    {
        _yandexBanner = new YandexMobileAds.Banner(ADS_BANNER_YANDEX, YandexMobileAds.Base.AdSize.StickySize(GetScreenWidthDp()), YandexMobileAds.Base.AdPosition.BottomCenter);
        YandexMobileAds.Base.AdRequest request = new YandexMobileAds.Base.AdRequest.Builder().Build();
        _yandexBanner.LoadAd(request);
    }

    private void YandexDestroyBanner()
    {
        _yandexBanner?.Destroy();
        _yandexBanner = null;
    }

    private void YandexHideBanner()
    {
        _yandexBanner?.Hide();
    }

    private void YandexShowInterstitial()
    {
        var _yandexInterstitial = new YandexMobileAds.Interstitial(ADS_INTERSTITIAL_YANDEX);
        YandexMobileAds.Base.AdRequest request = new YandexMobileAds.Base.AdRequest.Builder().Build();
        _yandexInterstitial.LoadAd(request);

        _yandexInterstitial.OnInterstitialLoaded += _yandexInterstitial_OnInterstitialLoaded;
        _yandexInterstitial.OnInterstitialFailedToLoad += _yandexInterstitial_OnInterstitialFailedToLoad;

        void _yandexInterstitial_OnInterstitialLoaded(object sender, EventArgs e)
        {
            if (_yandexInterstitial.IsLoaded())
            {
                _yandexInterstitial.Show();
                _yandexInterstitial.OnInterstitialLoaded -= _yandexInterstitial_OnInterstitialLoaded;

            }
        }

        void _yandexInterstitial_OnInterstitialFailedToLoad(object sender, YandexMobileAds.Base.AdFailureEventArgs e)
        {
            _yandexInterstitial.OnInterstitialLoaded -= _yandexInterstitial_OnInterstitialLoaded;
        }

    }


    private void YandexShowRewarded(Action onDone)
    {
        var _yandexRewarded = new YandexMobileAds.RewardedAd(ADS_WITH_REWARD_YANDEX);
        YandexMobileAds.Base.AdRequest request = new YandexMobileAds.Base.AdRequest.Builder().Build();
        _yandexRewarded.LoadAd(request);

        _yandexRewarded.OnRewardedAdLoaded += _yandexRewarded_OnRewardedAdLoaded;
        _yandexRewarded.OnRewardedAdFailedToLoad += _yandexRewarded_OnRewardedAdFailedToLoad;
        _yandexRewarded.OnRewardedAdShown += _yandexRewarded_OnRewardedAdShown;

        void _yandexRewarded_OnRewardedAdFailedToLoad(object sender, YandexMobileAds.Base.AdFailureEventArgs e)
        {
            _yandexRewarded.OnRewardedAdLoaded -= _yandexRewarded_OnRewardedAdLoaded;
            _yandexRewarded.OnRewardedAdFailedToLoad -= _yandexRewarded_OnRewardedAdFailedToLoad;
        }
        void _yandexRewarded_OnRewardedAdLoaded(object sender, EventArgs e)
        {
            if (_yandexRewarded.IsLoaded())
            {
                _yandexRewarded.OnRewardedAdLoaded -= _yandexRewarded_OnRewardedAdLoaded;
                _yandexRewarded.OnRewardedAdFailedToLoad -= _yandexRewarded_OnRewardedAdFailedToLoad;
                _yandexRewarded.Show();
            }
        }

        void _yandexRewarded_OnRewardedAdShown(object sender, EventArgs e)
        {
            _yandexRewarded.OnRewardedAdShown-= _yandexRewarded_OnRewardedAdShown;
            IsAdsEnable = false;
            onDone?.Invoke();
        }
    }

    #endregion
}
