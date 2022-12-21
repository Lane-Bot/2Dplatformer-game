using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFileter;
    CapsuleCollider2D touchingCol;
    public float groundDistance = 0.5f;
    public float wallkDistance = 0.02f;
    public float ceilingDistance = 0.05f;
    Animator animator;


    
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGround;
    public bool isGround { get { return _isGround; }
        private set {
        _isGround= value;
            animator.SetBool(AnimationString.isGround, value);
        } 
    }
    [SerializeField]
    private bool _isOnWall;
    public bool isOnWall
    {
        get { return _isOnWall; }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.isOnWall, value);
        }
    }
    [SerializeField]
    private bool _isOnCeiling;
    private Vector2 wallcheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    public bool isOnCeiling
    {
        get { return _isOnCeiling; }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationString.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        isGround = touchingCol.Cast(Vector2.down, castFileter, groundHits, groundDistance)>0;
        isOnWall = touchingCol.Cast(wallcheckDirection, castFileter, wallHits, wallkDistance) > 0;
        isOnCeiling = touchingCol.Cast(Vector2.up, castFileter, ceilingHits, ceilingDistance) > 0;
  
    }
}
