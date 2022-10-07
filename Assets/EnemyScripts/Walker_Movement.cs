using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_Movement : Walker_AI
{
    private Rigidbody2D rb2d;
    public int _moveSpeed;
    public int _dz;

    public bool isFacingRight;
    public float timer;
    public float fallTimer;
    public bool isHitByShield;
    [SerializeField] private LayerMask groundLayer;
    private int playerLayer, shieldLayer, enemyLayer;
    RaycastHit2D bottom, fallingCheck;

    // Start is called before the first frame update
    void Start()
    {
        shieldLayer = LayerMask.NameToLayer("Shield");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        playerLayer = LayerMask.NameToLayer("Player");
        setMoveSpeed(_moveSpeed);
        setdz(_dz);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //bottom = Physics2D.Raycast(new Vector2(, transform.position.y - transform.localScale.y/2), transform.TransformDirection(Vector2.down), 0.1f, groundLayer);
        bottom = Physics2D.Raycast(new Vector2(isFacingRight ? (transform.position.x + transform.localScale.x/2) : transform.position.x - transform.localScale.x/2, transform.position.y - transform.localScale.y/2), transform.TransformDirection(Vector2.down), 0.1f, groundLayer);
        fallingCheck = Physics2D.Raycast(new Vector2(isFacingRight ? (transform.position.x + transform.localScale.x/2) : transform.position.x - transform.localScale.x/2, transform.position.y - transform.localScale.y/2), transform.TransformDirection(Vector2.down), 10f, groundLayer);
        
        if(fallingCheck){
            if (!isHitByShield){
                rb2d.velocity = new Vector2(1 * _moveSpeed, rb2d.velocity.y);
            }
            if((bottom) && timer <= 0f && isHitByShield) {
                isHitByShield = false;
                timer = 0.3f;
            }
        } else {
            fallTimer -= Time.deltaTime;
        }
        if((!bottom) && timer <= 0f && !isHitByShield){
            fallTimer = 0.05f;
            _moveSpeed *= -1;
            isFacingRight = !isFacingRight;
            timer = 0.4f;
        }
        if (fallTimer <= 0f){
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        timer -= Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.layer == playerLayer){
            coll.gameObject.SetActive(false);
        }
        if ((coll.gameObject.layer == LayerMask.NameToLayer("Ground") || coll.gameObject.layer == enemyLayer) && timer <= 0f){
            _moveSpeed *= -1;
            isFacingRight = !isFacingRight;
            timer = 0.1f;
        }
        if (coll.gameObject.layer == enemyLayer && isHitByShield){
            rb2d.AddForce(isFacingRight ? new Vector2(-5f, 5f) : new Vector2(5f, 5f), ForceMode2D.Impulse);
        }
    }
}
