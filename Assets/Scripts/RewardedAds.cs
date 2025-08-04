using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour,  IUnityAdsLoadListener, IUnityAdsShowListener
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
    }

    public void LoadRewardedAds()
    {
        Advertisement.Load(adUnitId, this);
    }

    public void ShowRewardedAds()
    {
        Advertisement.Show(adUnitId, this);
        LoadRewardedAds();
    }

    #region ShowCallbacks
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial Ad loaded");
        Time.timeScale = 0f;
        PlayerStats.Bonus = 0;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId == adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Ads fully watched...");
            PlayerStats.Bonus = 20;
        }
        Time.timeScale = 1f;
    }
    #endregion
}
