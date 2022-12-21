using Assets.Script.Event;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DamageAble : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    public int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            // if health  drops below 0, character is no longer alive 
            if(_health <=0)
            {
                IsAlive = false;             
            }
        }
    }
    [SerializeField]
    private bool _IsAlive = true;
    private bool isInvicible = false;
    /* public bool isHit
     {
         get
         {
             return animator.GetBool(AnimationString.isHit);
         }
         set
         {
             animator.SetBool(AnimationString.isHit, value); 
         }
     }
    */
    // The velocity Should not be change while this is true but need to be respected by other components like
    //the player Controller
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationString.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationString.lockVelocity, value);
        }
    }

    public float timeSinceHit=0;
    public float invicibilityTimer =0.25f;

    public bool IsAlive
    {
        get
        {
            return _IsAlive;
        }
        set
        {
            _IsAlive = value;
            animator.SetBool(AnimationString.IsAlive, value);
           // Debug.Log("IsAlive set" + value);

            if (value == false)
            {
                damageableDeath.Invoke();   
            }
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        if (isInvicible)
        {
            if(timeSinceHit > invicibilityTimer)
            {
                //Remove Invicibility
                isInvicible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }
    //Return whether the damgealbe took damage or not
    public bool Hit(int Damage ,Vector2 knockback) //vector 2 knockback for knock back
    {
        if(IsAlive && !isInvicible)
        {
            Health -= Damage;
            isInvicible = true;
            LockVelocity = true;
            animator.SetTrigger(AnimationString.hitTrigger);
            //Notify other subscribed components that the damageable was hit to handle the knockback and such
            damageableHit?.Invoke(Damage, knockback); //Invoke method using these as the parameters when character get hit component resopond to it and apply knock back to rigibody
            //? check null or not null
            CharacterEvent.characterDamaged.Invoke(gameObject, Damage);


            return true;
        }
        //unable to be hit
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            //Health cap
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            //Lower Value
            int actualHeal = Mathf.Min(maxHeal, healthRestore);

            Health += actualHeal;

            CharacterEvent.characterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }   
}
