using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private float timer;
    private GameObject contact;

    void OnTriggerEnter2D(Collider2D col){
        contact = col.gameObject;
        if (contact.layer == LayerMask.NameToLayer("Bullet")){
            contact.SetActive(false);
            Invoke("breakShield", 0.18f);
        } else if (contact.layer == LayerMask.NameToLayer("Enemy")){
            contact.GetComponent<Rigidbody2D>().AddForce(GetComponentInParent<Movement>().isFacingRight ? new Vector2(5f, 5f) : new Vector2(-5f, 5f), ForceMode2D.Impulse);
            contact.GetComponent<Walker_Movement>().isHitByShield = true;
            contact.GetComponent<Walker_Movement>().timer = 0.3f;
            contact.GetComponent<Walker_Movement>().fallTimer = 1f;
            Invoke("breakShield", 0.18f);
        }
    }

    void breakShield(){
        GetComponentInParent<Weapons>().hit = true;
        GetComponentInParent<Weapons>().currShield = false;
        GetComponentInParent<Weapons>().option = 3;
        gameObject.SetActive(false);
    }

}
