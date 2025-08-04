using UnityEngine;
using System.Collections;
using System.Threading;

[RequireComponent(typeof(Monster))]
public class MonsterMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Monster monster;

    void Start()
    {
        monster = GetComponent<Monster>();
        target = Waypoints.points[0];
    }
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        transform.Translate(dir.normalized * monster.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

        monster.speed = monster.startSpeed;
    }

    void GetNextWayPoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        PlayerStats.LivePercent -= PlayerStats.LiveMinus;
        WaveSpawner.MonstersAlive--;
        Destroy(gameObject);
    }
}
