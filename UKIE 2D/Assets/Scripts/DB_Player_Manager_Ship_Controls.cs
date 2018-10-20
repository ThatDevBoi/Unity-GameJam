// David Peter Brooks 
// Space Ship Controller

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Player_Manager_Ship_Controls : MonoBehaviour
{
    // Floats 
    public float speed = 5;
    // Booleans
    public bool Moving;
    // References
    private Rigidbody2D PC_Rig;
    private BoxCollider2D PC_BC2D;
	// Use this for initialization
	void Start () {
        // Adding a Rigidbody2D
        PC_Rig = gameObject.AddComponent<Rigidbody2D>();
        //Turning off Rigidbody2D physics
        PC_Rig.isKinematic = true;

        // Adding A boxcollider2D
        PC_BC2D = gameObject.AddComponent<BoxCollider2D>();
        // Sizing the boxCollider 
        PC_BC2D.size = new Vector2(0.85f, 1);
	}
	
	// Update is called once per frame
	void Update () {
        // Functions
        PlayerMovement();
	}

    public void PlayerMovement()
    {
        float Movex = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        float MoveY = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + Movex, transform.position.y + MoveY);
    }
}
