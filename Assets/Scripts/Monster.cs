using UnityEngine;

public class Monster : MonoBehaviour
{
    public float startSpeed = 2f;
    
    [HideInInspector]    
    public float speed;

    public float health = 100;

    public int worth = 20;

    public GameObject deathEffect;

    private bool isDead = false;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float perCes)
    {
        speed = startSpeed * (1 - perCes);
    }

    private void Die()
    {
        isDead = true;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        WaveSpawner.MonstersAlive--;

        PlayerStats.Money += worth;
        Destroy(gameObject);
    }
}
