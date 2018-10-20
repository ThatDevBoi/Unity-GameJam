using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Platformer_Patrol : MonoBehaviour
{
    // Floats & int
    // Speed the Object Moves At
    // Patrol n Chase
    public float speed;

    public float MinDist;
    public float MaxDist;

    public float BulletCount;

    public float FireRate;

    public float NextTimeToFire;

    // Health Stuff

    // The current way point the Object is at
    public int currentWayPoint;

    // References
    // Rigidbody2D Reference
    private Rigidbody2D NPC_RB;
    // BoxCollider Reference
    private BoxCollider2D NPC_BC;
    // List of wayPoints in an array
    public Transform[] WayPoints;
    // Player Transform
    public Transform Player;
    // Bullet Prefab
    public GameObject Bullet;
    // Fire Position
    public GameObject Firepos;
    // Target of checkpoints
    public Vector3 Target;
    // Wherte the gameObject moves on the vector
    public Vector2 MoveDirection;
    // Velocity Vector2 Variable
    public Vector2 Velocity;

    // Booleans
    public bool DoPatrol, Chasing, StopChasing;

    // Use this for initialization
    void Start () {

        // Finding The PLayer
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // Finding FirePos
        Firepos = GameObject.FindGameObjectWithTag("Fire_Position");

        // Add Rigidbody2D
        NPC_RB = gameObject.AddComponent<Rigidbody2D>();
        // Freeze z Rotation
        NPC_RB.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Add BoxCollider2D
        NPC_BC = gameObject.AddComponent<BoxCollider2D>();
        // On Start FireRate = 1
        FireRate = 1f;
        // Next Time to fire controled and calculated by time
        NextTimeToFire = Time.time;


        // Boolean is true
        DoPatrol = true;

        Chasing = false;

        StopChasing = true;
	}
	
	// Update is called once per frame
	void Update () {
        Patrol_AI();
        Chase();

        if (Chasing)
        {

            StopChasing = false;
        }
        else
        {
            Chasing = false;

            StopChasing = true;
        }
	}

    public void Patrol_AI()
    {
        // if the current Point is less than the wayPoint Array Length
        if(currentWayPoint < WayPoints.Length)
        {
            // The Target is the current waypoint in the array list
            Target = WayPoints[currentWayPoint].position;
            // gameObjet Moves towards the target point and take away the current position
            MoveDirection = Target - transform.position;
            //Velocity Variable becomes the Rigidbody2D Velocity 
            Velocity = NPC_RB.velocity;

            // If the gameObjects Move direction returns back more than 1
            if(MoveDirection.magnitude < 1)
            {
                // Change Current Way Point in the Array List
                currentWayPoint++;
            }
            else
            {
                // Movement with physics in the MoveDirection Vector2 is normalized and moved with speed
                Velocity = MoveDirection.normalized * speed;
            }
        }
        else
        {
            // if boolean = true
            if(DoPatrol)
            {
                // Start patroling at the current check point 0 also a reset
                currentWayPoint = 0;


            }
            else
            {
                // continue until it resets
                Velocity = Vector2.zero;
            }
        }
        // Rigidbody Velocity becomes Velocity Vector Variable
        NPC_RB.velocity = Velocity;
        
    }
    public void Chase()
    {
            if (Vector2.Distance(transform.position, Player.position) <= MinDist)
            {
                Chasing = true;
                transform.position = Vector2.MoveTowards(transform.position, Player.position, speed);
            }

            if (Vector2.Distance(transform.position, Player.position) <= MaxDist)
            {
                Chasing = true;
                if (BulletCount >= 0 && Time.time > NextTimeToFire)
                {
                    // Fire Bullet
                    Instantiate(Bullet, Firepos.transform.position, transform.rotation);
                    // Calculating when we fire
                    NextTimeToFire = Time.time + FireRate;
                    // Number Of Bullets goes down
                    BulletCount--;
                }
            }
            else
            {
                Chasing = false;
            }
    }
}
