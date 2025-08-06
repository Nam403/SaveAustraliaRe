using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Bonus = -1;
    [SerializeField] int startMoney = 400;

    public static int Lives;
    public static float LivePercent;
    public static float LiveMinus;
    [SerializeField] int startLives = 20;

    public static int Rounds;

    void Start()
    {
        if (Bonus > 0)
        {
            Money = startMoney + Bonus;
            Bonus = 0; // Reset bonus
        }
        else
        {
            Money = startMoney;
        }
        Lives = startLives;
        LivePercent = 1f;
        LiveMinus = 1f / (startLives * 1f);

        Rounds = 0;
    }
}
