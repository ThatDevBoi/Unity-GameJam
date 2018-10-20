// David Peter Brooks 
// Camera Follow Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Follow_Player : MonoBehaviour
{
    // Target Transform
    public Transform Player;

    // zeros out the velocity
    Vector3 velocity = Vector3.zero;

    // Time to folow Player
    public float smoothTime = .15f;

    //Enable and set the max Y value
    public bool yMaxEnable = false;
    public float yMaxValue = 0;

    //Enable and set the min Y value
    public bool yMinEnable = false;
    public float yMinValue = 0;

    //Enable and set the max X value
    public bool xMaxEnable = false;
    public float xMaxValue = 0;

    // Enable and set the min X value
    public bool xMinEnable = false;
    public float xMinValue = 0;

    public void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // Player Position
        Vector3 Playerpos = Player.position;

        // Vertical
        if (yMinEnable && yMaxEnable)
            Playerpos.y = Mathf.Clamp(Player.position.y, yMinValue, yMaxValue);

        else if (yMinEnable)
            Playerpos.y = Mathf.Clamp(Player.position.y, yMinValue, Player.position.y);

        else if (yMaxEnable)
            Playerpos.y = Mathf.Clamp(Player.position.y, Player.position.y, yMaxValue);

        // Horizontal
        if (xMinEnable && xMaxEnable)
            Playerpos.x = Mathf.Clamp(Player.position.x, xMinValue, xMaxValue);

        else if (xMinEnable)
            Playerpos.x = Mathf.Clamp(Player.position.x, xMinValue, Player.position.x);

        else if (xMaxEnable)
            Playerpos.x = Mathf.Clamp(Player.position.x, Player.position.x, xMaxValue);







        // Allign the camera and the Players z position
        Playerpos.z = transform.position.z;
        // Using Smooth damp we will gradually change the camera transform position to the Players position based on the cameras transform velocity and out smooth time
        transform.position = Vector3.SmoothDamp(transform.position, Playerpos, ref velocity, smoothTime);
    }
}
