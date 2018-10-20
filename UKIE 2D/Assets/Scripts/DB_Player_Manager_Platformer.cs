// David Peter Brooks 
// Player Manager Platformer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Player_Manager_Platformer : MonoBehaviour
{

    // Floats 
    // x movement speed
    public float speed = 5;
    // y Jump Speed
    public float Jumpforce = 5;
    // Knife Damage Value
    public float MeleeDamage = 5;
    // Booleans
    public bool Grounded, CanDoubleJump;
    // References
    [SerializeField]
    // Animator Reference on child object knife
    private Animator Anim;
    // Rigibody2D reference
    private Rigidbody2D PC_RB;
    // Box Collider2d reference
    private BoxCollider2D PC_BC;
    // Knife Child Object Reference
    [SerializeField]
    private GameObject PC_melee;
    
	// Use this for initialization
	void Start () {
        // Finding the Knife gameObject
        PC_melee = GameObject.FindGameObjectWithTag("Player_Knife");


        // Grounded always = true on start
        Grounded = true;

        // Always false on start
        CanDoubleJump = false;


        // Add Rigidbody2D
        PC_RB = gameObject.AddComponent<Rigidbody2D>();
        // Freeze z rotation constraint
        PC_RB.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Add 2D Box Collider
        PC_BC = gameObject.AddComponent<BoxCollider2D>();
        // ReSizzing the boxcollider
        PC_BC.size = new Vector2(0.63f, 1);

        // Finding the BoxCollider On te child object knife
        BoxCollider2D BC = PC_melee.AddComponent<BoxCollider2D>();
        // Making the Knife Child object BoxCollider2D a trigger
        BC.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
        // Functions 
        Movement();
        Jumping();

        if(Input.GetKeyDown(KeyCode.F))
        {
            Anim.SetBool("Attacking", true);
        } else if (Input.GetKeyUp(KeyCode.F))
        {
            Anim.SetBool("Attacking", false);
        }

        
	}

    public void Movement() {
        // Making the float Move carry the inputs and values to move the PC
        float Move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // Allowing the player to move when pressing a buytton along the x axis 
        transform.position = new Vector2(transform.position.x + Move, transform.position.y);
    }

    public void Jumping() {
        // Simple Jump 1 Jump Only
        /*
        if(Grounded)
        { 
            if (Input.GetButtonDown("Jump"))
            {
                PC_RB.AddForce(new Vector2(0f, Jumpforce));
            }
        } */

        // Simple Double Jump
        if(Input.GetButtonDown("Jump"))
        {
            if(Grounded)
            {
                // Using physics we use the Rigidbody2d to add a jump value on the Y axis
                PC_RB.AddForce(new Vector2(0f, Jumpforce));

                // We can now double Jump
                CanDoubleJump = true;
            }
            else
            {
                if(CanDoubleJump)
                {
                    // We can no longer double jump again until we hit the ground
                    CanDoubleJump = false;
                    // Revert the y velocity 
                    PC_RB.velocity = new Vector2(PC_RB.velocity.x, 0);
                    // Make the player object jump again using physics
                    PC_RB.AddForce(new Vector2(0f, Jumpforce));
                }
            }
        }
        
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Grounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Grounded = false;
        }
    }
}
