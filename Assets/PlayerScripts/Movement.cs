using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 moveDirection;
    public float moveSpeed = 8f;
    public float jump = 6f;
    private float timer;
    public bool grounded;
    public bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    

    private void Awake(){
        playerControls = new PlayerControls();
    }

    private void OnEnable(){
        playerControls.Enable();
    }

    private void OnDisable(){
        playerControls.Disable();
        playerControls.Player.Jump.performed -= DoJump;
    }

    void Start(){
        playerControls.Player.Jump.performed += DoJump;
    }

    private void DoJump(InputAction.CallbackContext context){
        if(grounded || timer < 0.1f){
            timer = 1f;
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }

    private void Update(){
        moveDirection = playerControls.Player.Move.ReadValue<Vector2>();
        if(isGrounded()){
            grounded = true;
            timer = 0f;
        } else {
            grounded = false;
        }
        timer += Time.deltaTime;
        Flip();
    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    private bool isGrounded(){
        return (Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer) | Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer));
    }

    private void Flip(){
        if (isFacingRight && moveDirection.x < 0f || !isFacingRight && moveDirection.x > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}