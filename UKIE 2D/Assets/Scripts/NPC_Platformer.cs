using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Platformer : MonoBehaviour
{
    // Floats
    public float Speed = 3f;        // Movement speed that will move the NPC
    public float MinDist = 2;       // The Minimum distance the NPC can detect
    public float MaxDist = 4;       // The Maximum distance the NPC can detect
    public float BulletCount;       // A bullet count to give the NPC so it can shoot
    public float FireRate;          // How fast the NPC can shoot 
    public float NextToFire;        // The delay on the Fire
    public float Health = 5;        // Health Value
    public float Damage = 1;        // Damage per hit

    // References
    private BoxCollider2D NPC_Col;      // BoxCollider2D component
    private Rigidbody2D NPC_RB;     // Rigidbody2D component Reference
    [SerializeField]
    private Transform Target;       // Target is the player
    public GameObject Bullet_Prefab;       // Bullet Prefab Reference
    public Transform FirePos;       // Where the bullet prefab GameObject will be shot from
    

	// Use this for initialization
	void Start () {
        //Finding the FirePosition GameObject
        FirePos = GameObject.FindGameObjectWithTag("Fire_Position").GetComponent<Transform>();
        // Finding the Target Transform
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // Adding a BoxCollider Component to the gameObject
        NPC_Col = gameObject.AddComponent<BoxCollider2D>();
        // Sizzing the BoxCollider
        NPC_Col.size = new Vector2(0.55f, 1f);
        // Add Rigidbody2D component to gameObject
        NPC_RB = gameObject.AddComponent<Rigidbody2D>();
        // Freeze the Z Rotation
        NPC_RB.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Firerate = 1 on start
        FireRate = 1f;
        // Next Time to Fire is calculated with time
        NextToFire = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        Chase();
        // If health value is more or equal to 0 Then call Die() Function
        if (Health <= 0)
        {
            Dead();
        }
	}


    public void Chase()
    {
        // If the NPC isnt close enough to the player it continue to move
        if(Vector2.Distance(transform.position,Target.position) >= MinDist)
        {
            // Moves the NPC poisition towards the Targeted position using speed and using Time so it moved smooth
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        
        if(Vector2.Distance(transform.position, Target.position) <= MaxDist)
        {
            if(BulletCount >= 0 && Time.time > NextToFire)
            {
                // Fire Bullet
                Instantiate(Bullet_Prefab, FirePos.transform.position, transform.rotation);
                // Calculating when we fire
                NextToFire = Time.time + FireRate;
                // Number Of Bullets goes down
                BulletCount--;
            }
            
        } 
    }

    public void Dead()
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player_Knife")
        {
            Health -= Damage;
        }
    }
}
