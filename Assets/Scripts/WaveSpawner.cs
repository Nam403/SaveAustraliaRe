using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public static int MonstersAlive = 0;

    [SerializeField] Wave[] waves;

    [SerializeField] Transform spawnPoint;

    [SerializeField] float timeBetweenWaves = 5f;
    private float countdown = 5f;

    private int waveIndex = 0;

    [SerializeField] TextMeshProUGUI waveCountDownText;

    [SerializeField] GameManager gameManager;

    void Update()
    {
        if (MonstersAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length && GameManager.GameIsOver == false)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        
        // Make sure time run
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountDownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        MonstersAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnMonster(wave.monster);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void SpawnMonster(GameObject monster)
    {
        Instantiate(monster, spawnPoint.position, spawnPoint.rotation);
    }
}
