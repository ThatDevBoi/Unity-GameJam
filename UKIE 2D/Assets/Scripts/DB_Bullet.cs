using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Bullet : MonoBehaviour
{
    // References
    private Rigidbody2D Bullet_RB;
    private BoxCollider2D Bullet_BC;

    public float speed = 5;
	// Use this for initialization
	void Start () {
        Bullet_RB = gameObject.AddComponent<Rigidbody2D>();
        Bullet_RB.isKinematic = true;

        Bullet_BC = gameObject.AddComponent<BoxCollider2D>();
        Bullet_BC.size = new Vector2(0.13f, 0.85f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(0, speed, 0);
	}
}
