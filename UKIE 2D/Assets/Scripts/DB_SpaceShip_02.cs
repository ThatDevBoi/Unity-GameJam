// David Peter Brooks 
// Space Ship Controller_02

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_SpaceShip_02 : MonoBehaviour
{
    public float Speed = 5;

    public float rotationSpeed = 360;

    private Rigidbody2D RB_PC;

    private BoxCollider2D BC;

    private Vector3 Vect = Vector3.zero;

    public GameObject Bullet;

    public GameObject Firepos;

	// Use this for initialization
	void Start () {
        RB_PC = gameObject.AddComponent<Rigidbody2D>();
        RB_PC.isKinematic = true;

        BC = gameObject.AddComponent<BoxCollider2D>();
        BC.size = new Vector2(0.85f, 1);
	}
	
	// Update is called once per frame
	void Update () {
        // Functions
        Move();
        Fire();
	}

    public void Move()
    {
        // Get Inputs From Movex
        float MoveY = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        // Get inputs from MoveY
        float RotateZ = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        // Rotate the Player gameObject along the Z Axis
        transform.Rotate(0, 0, RotateZ * rotationSpeed * -1);


        Vect += Quaternion.Euler(0, 0, transform.rotation.z) * transform.up * MoveY * Speed;

        // Allow to Computer to figure out where the player will be next
        transform.position += Vect * Time.deltaTime;
    }

    public void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(Bullet, Firepos.transform.position, transform.rotation);
        }
    }
}
