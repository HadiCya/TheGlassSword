using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 direction;
    public GameObject bullet;
    public Transform weaponPosition;
    private bool isFacingRight;
    private string action = "";
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    public float attackRange;
    public GameObject txt;
    private int option;

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
        if ((dirX > -0.3f && dirX < 0.3f) && (dirY > 0.7f && dirY < 1f) && option != 1){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "BULLET";
            action = "bullet";
        } else if ((dirX > 0.7f && dirX < 1f) && (dirY > -0.3f && dirY < 0.3f) && option != 2){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "SWORD";
            action = "sword";
        } else if ((dirX > -0.3f && dirX < 0.3f) && (dirY > -1f && dirY < -0.7f) && option != 3){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "SHIELD";
            action = "shield";
        } else if ((dirX > -1f && dirX < -0.7f) && (dirY > -0.3f && dirY < 0.3f) && option != 4){
            txt.GetComponent<TMPro.TextMeshProUGUI>().text = "BRIDGE";
            action = "bridge";
        }
        bool temp = playerControls.Player.Fire.IsPressed();
        if (temp){
            switch (action){
                case "bullet":
                    shootBullet();
                    break;
                case "sword":
                    useSword();
                    break;
                case "shield":
                    useShield();
                    break;
                case "bridge":
                    placeBridge();
                    break;
            }
        }
    }
    void shootBullet(){
        GameObject bulletInstance = Instantiate(bullet, weaponPosition.position, transform.rotation);
        if(isFacingRight){
            bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.right * 5f;
        } else {
            bulletInstance.GetComponent<Rigidbody2D>().velocity = transform.right * -5f;
        }
        action = "";
        option = 1;
    }

    void useSword(){
        Collider2D coll = Physics2D.OverlapCircle(weaponPosition.position, attackRange, enemyLayer);
        RaycastHit2D right = Physics2D.Raycast(transform.position, /*weaponPosition.position*/transform.TransformDirection(Vector2.right), attackRange, groundLayer);
        if(coll && !right){
            Destroy(coll.gameObject);
        }
        action = "";
        option = 2;
    }

    void useShield(){
        action = "";
        option = 3;
    }

    void placeBridge(){
        action = "";
        option = 4;
    }

    private void OnDrawGizmos() {
     Gizmos.color = Color.red;
     Gizmos.DrawWireSphere(weaponPosition.position, attackRange);
     Gizmos.color = Color.blue;
     Gizmos.DrawLine(transform.position, weaponPosition.position);
 }
}

