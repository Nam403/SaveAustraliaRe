using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] string menuSceneName = "MainMenu";

    [SerializeField] SceneFader sceneFader;

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
