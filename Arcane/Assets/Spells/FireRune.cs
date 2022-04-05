using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRune : MonoBehaviour, ICast
{
    public SpellStats stats;

    public void Cast(Transform transform)
    {
        GameObject projectile = Instantiate(gameObject, transform);
        projectile.transform.localPosition = new Vector3(0, 0, 0);
        projectile.SetActive(true);
    }

    public void LevelUp()
    {
        stats.level++;
    }

    public void Update()
    {

    }

    private void Start()
    {
        //Destroy(gameObject, stats.ttl);
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyAI>().health -= stats.damage * transform.parent.GetComponent<PlayerMovement>().attackMultiplier;
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().health -= stats.damage * transform.parent.GetComponent<PlayerMovement>().attackMultiplier;
            Destroy(gameObject);
        }
    }
}
