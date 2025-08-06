using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject ui;

    [SerializeField] string menuSceneName = "MainMenu";

    [SerializeField] SceneFader sceneFader;

    public void LoadPauseMenu()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        LoadPauseMenu();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        WaveSpawner.MonstersAlive = 0;
    }

    public void Menu()
    {
        LoadPauseMenu();
        sceneFader.FadeTo(menuSceneName);
        WaveSpawner.MonstersAlive = 0;
    }
}
