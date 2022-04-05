using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatBurst : MonoBehaviour, ICast
{
    public SpellStats stats;
    public GameObject explosion;

    public void Cast(Transform transform)
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject projectile = Instantiate(gameObject, transform);
            projectile.transform.RotateAround(transform.position, Vector3.forward, Random.Range(0f, 360f));
            projectile.SetActive(true);
        }
        //Instantiate(gameObject, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z + 45));
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
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().health -= stats.damage * transform.parent.GetComponent<PlayerMovement>().attackMultiplier;
        }

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
    }
}
