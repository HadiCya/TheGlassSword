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
    [SerializeField] private LayerMask enemyLayer;

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
        rb2d.velocity = new Vector2(1 * _moveSpeed, rb2d.velocity.y);

        //NEEDS BETTER SOLUTION (bad)
        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 0.6f, playerLayer);
        RaycastHit2D rightWall = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 0.6f, groundLayer);
        RaycastHit2D left = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.6f, playerLayer);
        RaycastHit2D rightEnemy = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 0.6f, enemyLayer);
        RaycastHit2D leftEnemy = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.6f, enemyLayer);
        RaycastHit2D leftWall = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 0.6f, groundLayer);
        RaycastHit2D bottom = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.6f, groundLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 0.6f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 0.6f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 0.6f, Color.red);

        if(right || left){ //Player detection to kill player
            Destroy(right ? right.collider.gameObject : left.collider.gameObject);
        }
        if((rightWall || leftWall || rightEnemy || leftEnemy) && timer <= 0f){
            _moveSpeed *= -1;
            timer = 0.15f;
        }
        if(!bottom && timer <= 0f){ //Edge detection to rotate back
            _moveSpeed *= -1;
            timer = 0.15f;
        }
        timer -= Time.deltaTime;
    }   
}
