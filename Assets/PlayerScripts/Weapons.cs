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
    public bool hit = true;
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
        if ((dirX > -0.3f && dirX < 0.3f) && (dirY > 0.7f && dirY <= 1f) && option != 1){
            action = "SHARD";
        } else if ((dirX > 0.7f && dirX <= 1f) && (dirY > -0.3f && dirY < 0.3f) && option != 2){
            action = "SWORD";
        } else if ((dirX > -0.3f && dirX < 0.3f) && (dirY >= -1f && dirY < -0.7f) && option != 3){
            action = "SHIELD";
        } else if ((dirX >= -1f && dirX < -0.7f) && (dirY > -0.3f && dirY < 0.3f) && option != 4){
            action = "SCAFFOLD";
        }
        txt.GetComponent<TMPro.TextMeshProUGUI>().text = action;
        //If the fire button is pressed, do one of the mechanics selected.
        bool temp = playerControls.Player.Fire.IsPressed();
        if (temp && timer <= 0){
            hit = false;
            switch (action){
                case "SHARD":
                    shootBullet();
                    timer = 0.5f;
                    break;
                case "SWORD":
                    useSword();
                    timer = 0.5f;
                    break;
                case "SHIELD":
                    useShield();
                    timer = 0.5f;
                    break;
                case "SCAFFOLD":
                    placeBridge();
                    timer = 0.5f;
                    break;
            }
        }
        timer -= Time.deltaTime;
    }
    void shootBullet(){
        Destroy(bulletInstance);
        resetShield();
        resetTiles();
        bulletInstance = Instantiate(bullet, weaponPosition.position, transform.rotation);
        bulletInstance.GetComponent<Rigidbody2D>().velocity = isFacingRight ? Vector3.right * 20f : Vector3.right * -20f;
        action = "";
    }

    void useSword(){
        resetShield();
        resetTiles();
        Collider2D coll = Physics2D.OverlapCircle(weaponPosition.position, attackRange, enemyLayer);
        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), attackRange, groundLayer);
        if(coll && !right){
            Destroy(coll.gameObject);
            hit = true;
            option = 2;
        }
        action = "";
    }

    void useShield(){
        resetTiles();
        shield.SetActive(true);
        action = "";
    }

    void placeBridge(){
        resetShield();
        currentCell = floor.WorldToCell(transform.position);
        currentCell.y -= 2;
        for(int i = 0; i < 5; i++){
            currentCell.x += isFacingRight ? 1 : -1;
            wasFacingRight = isFacingRight;
        if (floor.GetTile(currentCell) == null && gameObject.GetComponent<Movement>().grounded){
            bridgeFloor.SetTile(currentCell, bridge);
            hit = true;
            option = 4;
        } else if (i == 0){
        }
        else {
            maxBlocks = i;
            break;
        }
        }
        action = "";
    }

    void resetTiles(){
        bridgeFloor.ClearAllTiles();
    }

    void resetShield(){
        shield.SetActive(false);
    }

    // void checkHit(int op){
    //     if(!hit){
    //         option = 0;
    //     } else {
    //         option = op;
    //     }
    // }

    private void OnDrawGizmos() {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(weaponPosition.position, attackRange);
     Gizmos.color = Color.blue;
     Gizmos.DrawLine(transform.position, weaponPosition.position);
 }
}