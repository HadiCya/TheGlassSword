using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Weapons : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 direction;
    public GameObject bullet;
    public Transform weaponPosition;
    public GameObject shield;
    public GameObject sword;
    private bool isFacingRight;
    private bool wasFacingRight;
    private int maxBlocks;
    private string action = "";
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    public Tile bridge;
    public Tilemap floor;
    public Tilemap bridgeFloor;
    public float attackRange;
    public GameObject txt;
    public int option;
    private float timer;
    public bool didBridge;
    public bool hit = true;
    public bool currBullet;
    public bool currShield;
    private Vector3Int currentCell;
    private GameObject bulletInstance;

    //Sets up player controls with input system
    private void Awake(){
        playerControls = new PlayerControls();
    }
    private void OnEnable(){
        playerControls.Enable();
    }
    private void OnDisable(){
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see if player is facing right
        isFacingRight = GetComponent<Movement>().isFacingRight;
        //Get direction of right joystick/keyboard input to choose what is selected
        direction = playerControls.Player.Choice.ReadValue<Vector2>();
        float dirX = direction.x;
        float dirY = direction.y;
        //checkHit(option);
        Debug.Log(direction);
        if (!currBullet && (dirX > -0.75f && dirX < 0f) && (dirY > -0.56f && dirY < 1f) && option != 1){
            action = "SHARD";
        } else if ((dirX > 0f && dirX < 0.75f) && (dirY > -0.56f && dirY < 1f) && option != 2){
            action = "SWORD";
        } else if (!currShield && (dirX > -0.75f && dirX < 0.75f) && (dirY > -1f && dirY < -0.56f) && option != 3){
            action = "SHIELD";
        }
        txt.GetComponent<TMPro.TextMeshProUGUI>().text = action;
        //If the fire button is pressed, do one of the mechanics selected.
        bool temp = playerControls.Player.Fire.IsPressed();
        if (temp && timer <= 0){
            hit = false;
            switch (action){
                case "SHARD":
                    shootBullet();
                    timer = 0.2f;
                    break;
                case "SWORD":
                    useSword();
                    timer = 0.2f;
                    break;
                case "SHIELD":
                    useShield();
                    timer = 0.2f;
                    break;
            }
        }
        if (playerControls.Player.Break.IsPressed() && !didBridge && timer <= -0.2f){
            placeBridge();
            timer = 0.2f;
        } else if (playerControls.Player.Break.IsPressed() && timer <= -0.2f){
            clearTools();
            timer = 0.2f;
        }
        timer -= Time.deltaTime;
    }
    void shootBullet(){
        DestroyImmediate(bulletInstance);
        currBullet = true;
        bulletInstance = null;
        resetShield();
        resetTiles();
        bulletInstance = Instantiate(bullet, weaponPosition.position, transform.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = isFacingRight ? Vector3.right * 20f : Vector3.right * -20f;
        action = "";
    }

    void useSword(){
        resetShield();
        resetTiles();
        Collider2D[] enemyCheck = Physics2D.OverlapCircleAll(weaponPosition.position, attackRange, enemyLayer);
        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), attackRange, groundLayer);
        foreach(Collider2D enemy in enemyCheck){
            if (!groundCheck){
                Destroy(enemy.gameObject);
                hit = true;
                didBridge = false;
                option = 2;
                action = "";
            }
        }
    }

    void useShield(){
        resetTiles();
        currShield = true;
        shield.SetActive(true);
        action = "";
    }

    void placeBridge(){
        resetShield();
        currentCell = floor.WorldToCell(transform.position);
        currentCell.y -= 2;
        currentCell.x -= 1;
        if (floor.GetTile(currentCell) == null && gameObject.GetComponent<Movement>().grounded){
            bridgeFloor.SetTile(currentCell, bridge);
        }
        currentCell.x += 1;
        if (floor.GetTile(currentCell) == null && gameObject.GetComponent<Movement>().grounded){
            bridgeFloor.SetTile(currentCell, bridge);
        }
        for(int i = 0; i < 5; i++){
            currentCell.x += isFacingRight ? 1 : -1;
            wasFacingRight = isFacingRight;
        if (floor.GetTile(currentCell) == null && gameObject.GetComponent<Movement>().grounded){
            bridgeFloor.SetTile(currentCell, bridge);
            didBridge = true;
        } else if (i == 0){
        }
        else {
            maxBlocks = i;
            break;
        }
        }
    }

    void clearTools(){
        resetTiles();
        didBridge = false;
    }
    void resetTiles(){
        bridgeFloor.ClearAllTiles();
    }

    void resetShield(){
        currShield = false;
        shield.SetActive(false);
    }

    private void OnDrawGizmos() {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(weaponPosition.position, attackRange);
     Gizmos.color = Color.blue;
     Gizmos.DrawLine(transform.position, weaponPosition.position);
 }
}