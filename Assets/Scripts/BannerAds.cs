using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;

    private string adUnitId;

    private void Awake()
    {
        #if UNITY_IOS
            adUnitId = iosAdUnitId;
        #elif UNITY_ANDROID
            adUnitId = androidAdUnitId;
        #elif UNITY_EDITOR
            adUnitId = androidAdUnitId;
        #endif

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void LoadBannerAds()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = BannerLoaded,
            errorCallback = BannerLoadError
        };

        Advertisement.Banner.Load(adUnitId, options);
    }

    public void ShowBannerAds()
    {
        BannerOptions options = new BannerOptions
        {
            showCallback = BannerShown,
            clickCallback = BannerClicked,
            hideCallback = BannerHidden
        };
        Advertisement.Banner.Show(adUnitId, options);
    }

    public void HideBannerAds()
    {
        Advertisement.Banner.Hide();
    }

    private void BannerHidden() {}

    private void BannerClicked() {}

    private void BannerShown() {}

    private void BannerLoadError(string message) {}

    private void BannerLoaded()
    {
        Debug.Log("Banner ad loaded");
    }
}
