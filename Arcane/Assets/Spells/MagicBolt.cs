using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBolt : MonoBehaviour, ICast
{
    public SpellStats stats;

    public void Cast(Transform transform)
    {
        GameObject projectile = Instantiate(gameObject, transform);
        projectile.SetActive(true);

        if(stats.level >= 2)
        {
            projectile = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 45));
            projectile.SetActive(true);
            projectile = Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + -45));
            projectile.SetActive(true);
        }
    }

    public void LevelUp()
    {
        stats.level++;
    }

    private void Start()
    {
        Destroy(gameObject, stats.ttl);
        stats.rigidBody.AddForce(transform.up * stats.speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}