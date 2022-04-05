using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;
    public float exp = 100f;
    public GameObject gameManager;
    public float damage = 10f;
    public EnemyStats stats;
    public GameObject weapon;
    public Rigidbody2D rigidBody;
    public GameObject remains;

    private void Update()
    {
        /*
        if(target)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
        */
        if(target)
        {
            float step = speed * Time.deltaTime;
            //transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            transform.up = target.position - transform.position;


            rigidBody.velocity = transform.up * speed;
        }

        if(stats.health <= 0)
        {
            Instantiate(remains, transform.position, Quaternion.identity);
            Destroy(gameObject);
            gameManager.GetComponent<GameMode>().player.GetComponent<PlayerController>().UpdateExp(exp);
            gameManager.GetComponent<UI>().kills++;
            gameManager.GetComponent<UI>().killCount.text = "Kills: " + gameManager.GetComponent<UI>().kills.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Attack());
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Take Damage
    }

    IEnumerator Attack()
    {
        weapon.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        weapon.SetActive(false);
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }
    */
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = null;
        }
    }
    */
    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 7)
        {
            health -= other.gameObject.GetComponent<SpellStats>().damage * gameManager.GetComponent<GameMode>().player.GetComponent<PlayerMovement>().attackMultiplier;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            gameManager.GetComponent<GameMode>().player.GetComponent<PlayerController>().UpdateExp(exp);
        }
    }
    */
}
