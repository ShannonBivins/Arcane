using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public GameObject dmgTxt;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 7)
        {
            GameObject text = Instantiate(dmgTxt, transform.position, Quaternion.identity);
            text.transform.parent = transform;
            text.SetActive(true);
            Destroy(text, 1f);
        }
    }
}