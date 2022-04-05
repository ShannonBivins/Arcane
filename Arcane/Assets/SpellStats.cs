using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStats : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;
    public Rigidbody2D rigidBody;
    public float damage = 25f;
    public float ttl = 1f;
    public int level = 1;
    public int maxLevel = 1;
    public bool passive = false;
    public bool cooldown = false;
    public float coolTime = 0f;
}
