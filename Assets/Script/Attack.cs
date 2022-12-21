using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knocback = Vector2.zero; 

    private void Awake()
    {
        attackCollider= GetComponent<Collider2D>(); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hit
        DamageAble damageAble = collision.GetComponent<DamageAble>();

        if (damageAble != null) 
        {
            // if parent is facing the left localscale , out knock back x flip its value to face the left as well
            Vector2 deiliverKnockback = transform.parent.localScale.x > 0 ? knocback : new Vector2(-knocback.x, knocback.y);//make sure when knockback get to rigth direction
            //hit target
            bool gotHit = damageAble.Hit(attackDamage, deiliverKnockback);

            if (gotHit)
            
                Debug.Log(collision.name + "Hit for" + attackDamage);
            
          
        }
    }
}
