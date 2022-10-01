using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_Movement : Walker_AI
{
    public Rigidbody2D rb2d;
    public int _moveSpeed;
    public int _dz;
    bool isFacingRight;
    float timer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        setMoveSpeed(_moveSpeed);
        setdz(_dz);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = Vector2.right * _moveSpeed;

        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 0.6f, playerLayer);
        RaycastHit2D left = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.6f, playerLayer);
        RaycastHit2D bottom = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.6f, groundLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 0.6f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 0.6f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 0.6f, Color.red);

        if(right || left){ //Player detection to kill player
            Debug.Log("KILL PLAYER");
        }
        if(!bottom && timer <= 0f){ //Edge detection to rotate back
            _moveSpeed *= -1;
            timer = 0.3f;
        }
        timer -= Time.deltaTime;
    }
}
