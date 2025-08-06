using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader;

    [SerializeField] string menuSceneName = "MainMenu";
    [SerializeField] string nextLevel = "Level02";

    [SerializeField] int levelToUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        AdsManager.Instance.rewardedAds.ShowRewardedAds();
        sceneFader.FadeTo(nextLevel);
        WaveSpawner.MonstersAlive = 0;
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
        WaveSpawner.MonstersAlive = 0;
    }
}
