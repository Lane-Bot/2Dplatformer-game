using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEyes : MonoBehaviour
{
    public float flightSpeed = 3f;
    public DetectionZone biteDetectionzone;
    public List<Transform> waypoints;
    Animator animator;
    Rigidbody2D rb;
    DamageAble damageAble;
    Transform nextWaypoint;
    public Collider2D deathCollider;
   

    int waypointNum = 0;

    public bool _hasTarget = false;
    public float waypointToreachDistance = 0.1f;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageAble = GetComponent<DamageAble>();
    }
    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    void Update()
    {
        HasTarget = biteDetectionzone.detectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        if (damageAble.IsAlive)
        {
            if (canMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        
    }
    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }
    private void Flight()
    {
        // Flight to the next waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
        //check distance to waypoints
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        // see if wee need to switch waypoint
        if (distance < waypointToreachDistance)
        {
            // switch to next way point
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                //loop back to original way points
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];
        }
    }
    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            // facing the right
            if (rb.velocity.x<0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            //facing the left
            if (rb.velocity.x > 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }

    }
    public void Ondeath()    
        {
            //dead flyer falls to the ground
            rb.gravityScale = 2f;
            rb.velocity = new Vector2(0, rb.velocity.y);
            deathCollider.enabled = true;
        }
}
