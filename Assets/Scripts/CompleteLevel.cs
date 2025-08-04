using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader sceneFader;

    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level02";
    public int levelToUnlock = 2;

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
