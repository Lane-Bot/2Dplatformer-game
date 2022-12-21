using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2 (3f,0);
    public Vector2 knockback = new Vector2(0,0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();     
    }
    void Start()
    {
        // if you want the projectile to be effected by gravity by default make it dynamic mode rigibody
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageAble damageAble = collision.GetComponent<DamageAble>();       
        if(damageAble != null)
        {
            Vector2 deiliverKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);//make sure when knockback get to rigth direction
            //hit target
            bool gotHit = damageAble.Hit(damage, deiliverKnockback);

            if (gotHit)
               
                Debug.Log(collision.name + "Hit for" + damage);
                Destroy(gameObject);
        }
    }
}
