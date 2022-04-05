using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour, ICast
{
    public SpellStats stats;
    public GameObject explosion;

    public void Cast(Transform transform)
    {
        if(!stats.cooldown)
        {
            GameObject projectile = Instantiate(gameObject, transform);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);

        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().health -= stats.damage * transform.parent.GetComponent<PlayerMovement>().attackMultiplier;
        }
    }
}