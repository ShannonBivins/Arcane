using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LavaZone : MonoBehaviour, ICast
{
    public SpellStats stats;

    public void Cast(Transform transform)
    {
        GameObject projectile = Instantiate(gameObject, transform);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        projectile.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        projectile.SetActive(true);
    }

    public void LevelUp()
    {
        stats.level++;
    }

    public void Update()
    {
        if(transform.localScale.x < 3f)
        {
            transform.localScale += new Vector3(10, 10, 0) * Time.deltaTime;
        }
    }

    private void Start()
    {
        Destroy(gameObject, stats.ttl);
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
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().health -= stats.damage * transform.parent.GetComponent<PlayerMovement>().attackMultiplier * Time.deltaTime;
        }
    }
}
