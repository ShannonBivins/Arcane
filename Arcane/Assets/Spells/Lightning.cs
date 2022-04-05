using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour, ICast
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
        transform.localScale += new Vector3(0, 1000, 0) * Time.deltaTime;
    }

    private void Start()
    {
        Destroy(gameObject, stats.ttl);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
