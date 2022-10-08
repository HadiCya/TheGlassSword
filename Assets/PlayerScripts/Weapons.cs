using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

enum WeaponSelection{ SHARD, SWORD, SHIELD, NONE }

public class Weapons : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 direction;
    private bool isFacingRight, wasFacingRight;
    private int maxBlocks;
    private WeaponSelection action = WeaponSelection.NONE;
    private float timer;
    private Vector3Int currentCell;
    private GameObject bulletInstance;
    [SerializeField] private LayerMask enemyLayer, groundLayer;

    public GameObject bullet, shield;
    public Transform weaponPosition;
    public float attackRange;
    public bool currBullet, currShield;
    public bool hit = true;
    public Tile bridge;
    public Tilemap floor, bridgeFloor;
    public GameObject txt;
    public int option;

    
    #region InputSystem //Sets up player controls with input system
    private void Awake(){
        playerControls = new PlayerControls();
    }
    private void OnEnable(){
        playerControls.Enable();
    }
    private void OnDisable(){
        playerControls.Disable();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        isFacingRight = GetComponent<Movement>().isFacingRight; //Checks to see if player is facing right
        direction = playerControls.Player.Choice.ReadValue<Vector2>(); //Get direction of right joystick/keyboard input to choose what is selected

        float dirX = direction.x;
        float dirY = direction.y;
        if (!currBullet && (((dirX > -0.75f && dirX < 0f) && (dirY > -0.56f && dirY < 1f)) || playerControls.Player.Shard.IsPressed()) && option != 1){
            action = WeaponSelection.SHARD;
        } else if ((((dirX > 0f && dirX < 0.75f) && (dirY > -0.56f && dirY < 1f)) || playerControls.Player.Sword.IsPressed()) && option != 2){
            action = WeaponSelection.SWORD;
        } else if (!currShield && (((dirX > -0.75f && dirX < 0.75f) && (dirY > -1f && dirY < -0.56f)) || playerControls.Player.Shield.IsPressed()) && option != 3){
            action = WeaponSelection.SHIELD;
        }
        txt.GetComponent<TMPro.TextMeshProUGUI>().text = action.ToString(); //Display weapon selection on screen.
        bool temp = playerControls.Player.Fire.IsPressed(); //If the fire button is pressed, do one of the mechanics selected.
        if (temp && timer <= 0){
            hit = false;
            switch (action){
                case WeaponSelection.SHARD:
                    shootBullet();
                    timer = 0.2f;
                    break;
                case WeaponSelection.SWORD:
                    useSword();
                    timer = 0.2f;
                    break;
                case WeaponSelection.SHIELD:
                    useShield();
                    timer = 0.2f;
                    break;
            }
        } 
        if (playerControls.Player.Bridge.IsPressed() && gameObject.GetComponent<Movement>().grounded && timer <= 0) {
            placeBridge();
            timer = 0.2f;
        }
        timer -= Time.deltaTime;
    }
    void shootBullet(){
        currBullet = true;
        resetShield();
        clearBridge();
        bulletInstance = Instantiate(bullet, weaponPosition.position, transform.rotation);
        action = WeaponSelection.NONE;
    }

    void useSword(){
        resetShield();
        clearBridge();
        Collider2D[] enemyCheck = Physics2D.OverlapCircleAll(weaponPosition.position, attackRange, enemyLayer);
        RaycastHit2D groundCheck = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), attackRange, groundLayer);
        foreach(Collider2D enemy in enemyCheck){
            if (!groundCheck){
                enemy.gameObject.SetActive(false);
                hit = true;
                option = 2;
                action = WeaponSelection.NONE;
            }
        }
    }

    void useShield(){
        clearBridge();
        currShield = true;
        shield.SetActive(true);
        action = WeaponSelection.NONE;
    }

    void placeBridge(){
        clearBridge();
        resetShield();
        currentCell = floor.WorldToCell(transform.position);
        int factor = isFacingRight ? 1 : -1;
        currentCell.y -= 2;
        for(int i = 0; i < 6; i++){
            if (floor.GetTile(currentCell) == null){
                bridgeFloor.SetTile(currentCell, bridge);
            } else if (i == 0){
            } else {
                break;
            }
            currentCell.x += factor;
        }
    }

    void clearBridge(){
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