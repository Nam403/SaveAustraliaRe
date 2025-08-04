using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Tower : MonoBehaviour
{
    private Transform target;
    private Monster targetMonster;

    [Header("General")]

    public float range = 2f;

    [Header("Use bullets")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Use laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowPerSec = 0.1f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;

    [Header("Unity Setup Fields")]

    public string monsterTag = "Monster";

    public Transform firePoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestMonster = null;

        foreach(GameObject monster in monsters)
        {
            float distanceToMonster = Vector3.Distance(transform.position, monster.transform.position);
            if(distanceToMonster < shortestDistance)
            {
                shortestDistance = distanceToMonster;
                nearestMonster = monster;
            }
        }

        if (nearestMonster != null && shortestDistance <= range)
        {
            target = nearestMonster.transform;
            targetMonster = target.GetComponent<Monster>();
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                }
            }

            return;
        }

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
    }

    void Laser()
    {
        targetMonster.TakeDamage(damageOverTime * Time.deltaTime);
        targetMonster.Slow(slowPerSec);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.blue;

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.3f;

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized * .5f;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        WaterBullet waterbullet = bulletGO.GetComponent<WaterBullet>();

        if (waterbullet != null)
        {
            waterbullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
