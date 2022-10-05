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
    RaycastHit2D bottom;

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

        bottom = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.6f, groundLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 0.6f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 0.6f, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 0.6f, Color.red);
        if(!bottom && timer <= 0f){
            _moveSpeed *= -1;
            timer = 0.15f;
        }
        timer -= Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D coll){
        if (coll.gameObject.layer == LayerMask.NameToLayer("Player")){
            Destroy(coll.gameObject);
        }
        // if (coll.gameObject.layer == LayerMask.NameToLayer("Shield")){
        //     GameObject player = GameObject.FindGameObjectWithTag("Player");
        //     player.GetComponent<Weapons>().hit = true;
        //     player.GetComponent<Weapons>().currShield = false;
        //     player.GetComponent<Weapons>().option = 3;
        //     coll.gameObject.SetActive(false);
        //     Destroy(gameObject);
        // }
        if (coll.gameObject.layer == LayerMask.NameToLayer("Ground") || coll.gameObject.layer == LayerMask.NameToLayer("Enemy") && timer <= 0f){
            _moveSpeed *= -1;
            timer = 0.15f;
        }
    }
}
