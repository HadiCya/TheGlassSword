using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col){
        GameObject contact = col.gameObject;
        if (contact.layer == LayerMask.NameToLayer("Bullet")){
            contact.SetActive(false);
            Invoke("breakShield", 0.18f);
        } else if (contact.layer == LayerMask.NameToLayer("Enemy")){
            Walker_Movement enemyObj = contact.GetComponent<Walker_Movement>();
            enemyObj.rb2d.AddForce(GetComponentInParent<Movement>().isFacingRight ? new Vector2(5f, 5f) : new Vector2(-5f, 5f), ForceMode2D.Impulse);
            enemyObj.isHitByShield = true;
            enemyObj.timer = 0.3f;
            enemyObj.fallTimer = 1f;
            Invoke("breakShield", 0.18f);
        }
    }

    void breakShield(){
        Weapons parentWeapons = GetComponentInParent<Weapons>();
        parentWeapons.hit = true;
        parentWeapons.option = 3;
        gameObject.SetActive(false);
    }

}
