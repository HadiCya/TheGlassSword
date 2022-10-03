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
    public float attackRange;
    public GameObject txt;
    private int option;
    private float timer;
    private Tile oldTile;
    private Vector3Int currentCell;

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
        isFacingRight = GetComponent<Movement>().isFacingRight;
        direction = playerControls.Player.Choice.ReadValue<Vector2>();
        float dirX = direction.x;
        float dirY = direction.y;
        if ((dirX > -0.3f && dirX < 0.3f) && (dirY > 0.7f && dirY <= 1f) && option != 1){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "BULLET";
            action = "bullet";
        } else if ((dirX > 0.7f && dirX <= 1f) && (dirY > -0.3f && dirY < 0.3f) && option != 2){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "SWORD";
            action = "sword";
        } else if ((dirX > -0.3f && dirX < 0.3f) && (dirY >= -1f && dirY < -0.7f) && option != 3){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "SHIELD";
            action = "shield";
        } else if ((dirX >= -1f && dirX < -0.7f) && (dirY > -0.3f && dirY < 0.3f) && option != 4){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "BRIDGE";
            action = "bridge";
        }
        bool temp = playerControls.Player.Fire.IsPressed();
        if (temp && timer <= 0){
            switch (action){
                case "bullet":
                    shootBullet();
                    timer = 0.5f;
                    break;
                case "sword":
                    useSword();
                    timer = 0.5f;
                    break;
                case "shield":
                    useShield();
                    timer = 0.5f;
                    break;
                case "bridge":
                    placeBridge();
                    timer = 0.5f;
                    break;
            }
        }
        timer -= Time.deltaTime;
    }
    void shootBullet(){
        shield.SetActive(false);
        resetTiles();
        GameObject bulletInstance = Instantiate(bullet, weaponPosition.position, transform.rotation);
        if(isFacingRight){
            bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.right * 20f;
        } else {
            bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.right * -20f;
        }
        action = "";
        option = 1;
    }

    void useSword(){
        shield.SetActive(false);
        resetTiles();
        Collider2D coll = Physics2D.OverlapCircle(weaponPosition.position, attackRange, enemyLayer);
        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), attackRange, groundLayer);
        if(coll && !right){
            Destroy(coll.gameObject);
        }
        action = "";
        option = 2;
    }

    void useShield(){
        shield.SetActive(true);
        resetTiles();
        action = "";
        option = 3;
    }

    void placeBridge(){
        shield.SetActive(false);
        currentCell = floor.WorldToCell(transform.position);
        currentCell.y -= 2;
        for(int i = 0; i < 5; i++){
            currentCell.x += isFacingRight ? 1 : -1;
            wasFacingRight = isFacingRight;
            oldTile = (Tile)floor.GetTile(currentCell);
            if (oldTile == null && gameObject.GetComponent<Movement>().grounded){
            floor.SetTile(currentCell, bridge);
        } else {
            oldTile = null;
            maxBlocks = i;
            break;
        }
        }
        action = "";
        option = 4;
    }

    void resetTiles(){
        for(int i = 0; i < maxBlocks; i++){
            currentCell.x -= wasFacingRight ? 1 : -1;
            floor.SetTile(currentCell, oldTile);
        }
        
    }

    private void OnDrawGizmos() {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(weaponPosition.position, attackRange);
     Gizmos.color = Color.blue;
     Gizmos.DrawLine(transform.position, weaponPosition.position);
 }
}

