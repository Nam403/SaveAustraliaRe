using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiveUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    public Image healthBar;

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStats.Lives.ToString() + " LIVES";
        healthBar.fillAmount = PlayerStats.LivePercent;
    }
}
