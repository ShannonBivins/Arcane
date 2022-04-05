using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSattelite : MonoBehaviour
{
    //public SpellStats stats;
    /*
    public void Cast(Transform transform)
    {
        //center = transform;
        GameObject projectile = Instantiate(gameObject, transform);
        projectile.transform.localPosition = new Vector3(0f, 2f, 0f);
        projectile.SetActive(true);
    }
    */
    private void Start()
    {
        ///Destroy(gameObject);
        //stats.rigidBody.AddForce(transform.up * stats.speed, ForceMode2D.Impulse);
    }

    void LateUpdate()
    {
        //transform.position = center.position + (transform.position - center.position).normalized * 1.0f;
        transform.RotateAround(transform.parent.position, Vector3.forward, 100.0f * Time.deltaTime);
        //Vector3 desiredPosition = (transform.position - transform.parent.position).normalized * 1.0f + transform.parent.position;
        //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, 1.0f);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyStats>().health -= transform.parent.GetComponent<SpellStats>().damage * transform.parent.transform.parent.GetComponent<PlayerMovement>().attackMultiplier;
        }
    }
}
