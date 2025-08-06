using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject completeLevelUI;

    void Start()
    {
        GameIsOver = false;

        StartCoroutine(DisplayBannerWithDelay());
    }

    private IEnumerator DisplayBannerWithDelay()
    {
        yield return new WaitForSeconds(1f);
        AdsManager.Instance.bannerAds.ShowBannerAds();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameIsOver)
        {
            return;
        }

        /*if (Input.GetKeyDown("e")){
            EndGame();
        }*/

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }

        if(WaveSpawner.MonstersAlive > 0)
        {
            AdsManager.Instance.bannerAds.HideBannerAds();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = false;
        completeLevelUI.SetActive(true);
    }
}
