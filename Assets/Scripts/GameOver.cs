using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

    public void Retry()
    {
        AdsManager.Instance.interstitialAds.ShowInterstitialAds();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        WaveSpawner.MonstersAlive = 0;
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
        WaveSpawner.MonstersAlive = 0;
    }
}
