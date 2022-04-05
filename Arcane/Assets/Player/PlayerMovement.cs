using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rigidBody;
    Vector2 movement;
    Vector2 mousePos;
    public float dashDistance = 15f;
    public bool isDashing = false;
    public bool isWalking = true;
    public Tilemap tilemap;
    [HideInInspector] public float speedMultiplier = 1f;
    [HideInInspector] public float attackMultiplier = 1f;
    public float rotSpeed = 500f;
    public float dashCooldown = 1f;
    private bool dashCool = false;

    void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rigidBody.rotation = angle;
        Quaternion qTo = Quaternion.Euler (new Vector3 (0, 0, angle));
        rigidBody.rotation = Quaternion.RotateTowards(transform.rotation, qTo, rotSpeed * Time.deltaTime).eulerAngles.z;

        if(!isDashing)
        {
            rigidBody.velocity = new Vector2(movement.x * (moveSpeed * speedMultiplier), movement.y * (moveSpeed * speedMultiplier));
        }
    }

    IEnumerator OnDash()
    {
        if(!isDashing && !dashCool)
        {
            isDashing = true;
            rigidBody.AddForce(new Vector2(movement.x * dashDistance, movement.y * dashDistance), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            isDashing = false;
            dashCool = true;
            yield return new WaitForSeconds(dashCooldown);
            dashCool = false;
        }
    }
}
