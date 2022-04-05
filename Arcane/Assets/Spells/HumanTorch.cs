using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTorch : MonoBehaviour, ICast
{
    public SpellStats stats;
    GameObject player;

    public void Cast(Transform transform)
    {
        GameObject projectile = Instantiate(gameObject, transform);
        projectile.SetActive(true);
    }

    void Start()
    {
        player = transform.parent.gameObject;
        player.GetComponent<PlayerMovement>().isWalking = false;
        player.GetComponent<PlayerMovement>().isDashing = true;
        Destroy(gameObject, 5f);
    }

    public void LevelUp()
    {
        stats.level++;
    }
    
    void Update()
    {
        //player.GetComponent<PlayerMovement>().rigidBody.velocity = new Vector2(10f, 10f);
        //player.transform.position += transform.up * Time.deltaTime * 10f;

        player.GetComponent<Rigidbody2D>().velocity = transform.up * 15f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().health -= stats.damage * transform.parent.GetComponent<PlayerMovement>().attackMultiplier;
        }
    }

    void OnDestroy()
    {
        player.GetComponent<PlayerMovement>().isWalking = true;
        player.GetComponent<PlayerMovement>().isDashing = false;
        //player.GetComponent<PlayerMovement>().isWalking = true;
        //player.GetComponent<PlayerMovement>().moveSpeed = 500f;
        //player.GetComponent<PlayerMovement>().speedMultiplier = 1f;
    }
}
