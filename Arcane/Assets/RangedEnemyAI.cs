using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;
    public float exp = 100f;
    public GameObject gameManager;
    public float damage = 10f;
    public float maxRange = 5f;
    public float minRange = 3f;
    public GameObject projectile;
    public float attackSpeed = 3f;
    public bool attacking = false;
    public EnemyStats stats;
    public Rigidbody2D rigidBody;
    public GameObject remains;

    private void Update()
    {
        if(target)
        {
            transform.up = target.position - transform.position;
        }

        float distance = Vector3.Distance(target.transform.position, gameObject.transform.position);

        if(target && distance > maxRange)
        {
            float step = speed * Time.deltaTime;
            //transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            rigidBody.velocity = transform.up * speed;
        }
        else if(target && minRange <= distance && distance <= maxRange && !attacking)
        {
            rigidBody.velocity = Vector2.zero;
            StartCoroutine(Attack());
        }
        else if(target && distance <= minRange)
        {
            float step = speed * Time.deltaTime;
            //Vector3 direction = transform.position - target.transform.position;
            //transform.position = Vector2.MoveTowards(transform.position, direction, step);
            rigidBody.velocity = -transform.up * speed;
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

    IEnumerator Attack()
    {
        attacking = true;
        GameObject newProjectile = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        newProjectile.SetActive(true);
        yield return new WaitForSeconds(attackSpeed);
        attacking = false;
    }
}