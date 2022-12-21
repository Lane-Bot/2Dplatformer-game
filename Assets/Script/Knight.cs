using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections), typeof(DamageAble))]
public class Knight : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public DetectionZone attackZone;
    Animator animator;
    TouchingDirections touchingDirectons;
    Rigidbody2D rb;
    public float walkStopRate = 0.05f;
    DamageAble damageAble;
    public DetectionZone cliffDetectionZone;

    public enum WalkableDirection { Right,Left }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set { 
            if(_walkDirection !=value)
            {
                //flip direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1,gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            
            
            _walkDirection = value; }
    }
    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set 
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
     }
    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }

    public float attackCooldown
    { get 
            {
            return animator.GetFloat(AnimationString.attackCooldown);
            } private set { 
            animator.SetFloat(AnimationString.attackCooldown, Mathf.Max(value,0));
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
        touchingDirectons = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageAble = GetComponent<DamageAble>();
    }
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        
    }
    private void FixedUpdate()
    {
        if (touchingDirectons.isGround &&touchingDirectons.isOnWall)
        {
            FilpDirection();
        }
        if (canMove && touchingDirectons.isGround)
            //Accelerate towards max Speed
            rb.velocity = new Vector2(
                Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.deltaTime), -maxSpeed, maxSpeed),rb.velocity.y);
        else
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
    }
    private void FilpDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not right or left");
        }
    }
    public void OnHit(int Damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    //No ground filp
    public void OnCliffDetected()
    {
        if (touchingDirectons.isGround)
        {
            FilpDirection();
        }
    }
}
