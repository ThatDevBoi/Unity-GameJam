using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Platformer_Bullet : MonoBehaviour
{
    private Rigidbody2D RB;
    private CircleCollider2D CC;

    public float Speed = 0.5f;

    public Transform Player;
    private Vector2 moveDirection;
    
	// Use this for initialization
	void Start () {
        // Add Rigidbody2D
        RB = gameObject.AddComponent<Rigidbody2D>();
        // Remove Physcis
        RB.isKinematic = true;

        // Add CircleCollider2D
        CC = gameObject.AddComponent<CircleCollider2D>();
        CC.isTrigger = true;

        // Movement Logic
        // Finding the Player
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // Movement direction is towards the player gameObject and removing the gameObjects current transform and position while moving with speed value
        moveDirection = (Player.transform.position - transform.position).normalized * Speed;
        // Moves with physcis on the x axis
        RB.velocity = new Vector2(moveDirection.x, moveDirection.y);
        // Destroys bullet after 3 Seconds
        Destroy(gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
