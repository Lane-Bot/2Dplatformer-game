using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections),typeof(DamageAble))] // Make sure it have Rigibody2D
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed =8f;
    TouchingDirections touchingDirections;
    Vector2 moveInput;
    DamageAble damageAble;
   public float currentMoveSpeed { get
        {
            if (canMove) // check that when attack can't move
            {
                if (isMoving && !touchingDirections.isOnWall)
                {
                    if (isRunning)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }
                else
                {
                    // idle speed is 0;
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    
    [SerializeField]
    private bool _isMoving = false;
    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);

        }
    }
    [SerializeField]
    private bool _isRunning = false;
    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);

        }
    }
    public bool canMove { get
        {
            return animator.GetBool(AnimationString.canMove);
        } 
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationString.IsAlive);
        }
    }



    Rigidbody2D rb;
    Animator animator;
   
    public bool _isFacingright= true;
    private float jumpImpulse = 10f;

    public bool isFacingright
    { get { return _isFacingright; }private set{
            // flip only if value is new
            if (_isFacingright != value)
            {
                //flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
        _isFacingright = value;
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections= GetComponent<TouchingDirections>();
        damageAble = GetComponent<DamageAble>();


    }

    private void FixedUpdate()
    {
        if(!damageAble.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);

    }
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive) //check is Alive before do faceDirection
        {
            isMoving = moveInput != Vector2.zero;


            setFacingDirection(moveInput);
        }
        else
        {
            isMoving = false;
        }
    }
    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingright)
        {
            //face the right
            isFacingright = true;
        }
        else if (moveInput.x < 0 && isFacingright)
        {
            //face the left
            isFacingright = false;
        }
    }
    public void Onjump(InputAction.CallbackContext context)
    {
        // Todo Check if alive as well
        if(context.started && touchingDirections.isGround && canMove)
        {
            animator.SetTrigger(AnimationString.jumpTriger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void Onattack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.attackTriger);
        }
    }
    public void OnRangeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.rangeAttackTriger);
        }
    }
    public void Onrun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }
    public void OnHit(int Damage,Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
}
