/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedVideoAdsManager : MonoBehaviour, IShowAds
{
    public string gameId = "3459757";
    public bool testMode = true;

    public string adsID = "rewardedVideo";

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }

    public void InitializeAds()
    {
        if (Advertisement.isInitialized == false)
        {
            while (Advertisement.isInitialized == false) Advertisement.Initialize(gameId, testMode);
        }
    }

    public void ShowAds()
    {
        InitializeAds();
        Advertisement.Show(adsID);
    }

    public bool GetAdsResult()
    {
        return Advertisement.isShowing;
    }
}
