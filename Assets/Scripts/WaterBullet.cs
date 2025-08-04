using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    private Transform target;

    public float speed = 4f;

    public int damage = 20;

    public GameObject impactEffect;

    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        //GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);

        Damage(target);
        Destroy(gameObject);
    }

    void Damage(Transform monster)
    {
        Monster m = monster.GetComponent<Monster>();

        if (m != null)
        {
            m.TakeDamage(damage);
        }
    }
}
