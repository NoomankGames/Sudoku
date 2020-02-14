/*
 * author : Kirakosyan Nikita
 * e-mail : noomank.games@gmail.com
 */
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class VideoAdsManager : MonoBehaviour, IShowAds
{
    public string gameId = "3459757";
    public bool testMode = true;

    public string adsID = "video";
    private bool _shown = false;

    private void Awake()
    {
        StartCoroutine(InitializeAds());
    }

    private void Update()
    {
        if (_shown == false)
        {
            ShowAds();
            _shown = true;
        }
    }

    public IEnumerator InitializeAds()
    {
        if (Advertisement.isInitialized == false)
        {
            Advertisement.Initialize(gameId, testMode);
        }
        else
        {
            StopCoroutine(InitializeAds());
        }

        yield return new WaitForSeconds(3.0f);
    }

    public void ShowAds()
    {
        StartCoroutine(InitializeAds());
        Advertisement.Show(adsID);
    }
}