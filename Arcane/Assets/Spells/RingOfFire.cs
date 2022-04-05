using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfFire : MonoBehaviour, ICast
{
    public SpellStats stats;

    public void Cast(Transform transform)
    {
        GameObject projectile = Instantiate(gameObject, transform);
        projectile.SetActive(true);
    }

    public void LevelUp()
    {
        stats.level++;
    }

    public void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale += new Vector3(10, 10, 0) * Time.deltaTime;

        if(transform.localScale.x >= 3f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().health -= stats.damage * transform.parent.GetComponent<PlayerMovement>().attackMultiplier;
        }
    }
}
