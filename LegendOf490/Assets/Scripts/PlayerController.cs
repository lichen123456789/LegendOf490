﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 12.0f;
    public float turnSpeed = 150.0f;
    public float strafeSpeed = 12.0f;
	private float jumpPressure;
	private float minJump;
	private float maxJumpPressure;
	private Rigidbody rbody;
	private bool onGround;

    public float dashAmount = 120.0f;
    public float dashDamping = 2.0f;
    private int dashCount = 0;
    private float dashCooldown = 0f;
    
 
    //this runs once 
    void Start()
    {
		onGround = true;
		jumpPressure = 0f;
		minJump = 8f;
		maxJumpPressure = 10f;
		rbody = GetComponent<Rigidbody> ();

    }

    //this runs every frame
    void Update()
    {
        bool forwardPressed = Input.GetKeyDown(KeyCode.W);
        bool attackPressed = Input.GetMouseButtonDown(0);

        float currentDash = transform.position.x;
        float wantedDash = transform.position.x + dashAmount;

        if (onGround) {
			// holding space bar
			if (Input.GetButton ("Jump"))
			{
				if (jumpPressure < maxJumpPressure)
				{
					jumpPressure += Time.deltaTime * 10f;
				}
				else {
					jumpPressure = maxJumpPressure;
				}
				//print (jumpPressure);
			}
			else 	// not holding space bar
			{
				if (jumpPressure > 0f)
				{
					jumpPressure = jumpPressure + minJump;
					rbody.velocity = new Vector3 (0f, jumpPressure, 0f);
					jumpPressure = 0;
					onGround = false;
				}
				
			}
		}

        if (forwardPressed)
        {
            GameObject.Find("Player").GetComponent<Animation>().Play("Walk");
        } else if (attackPressed)
        {
            GameObject.Find("Player").GetComponent<Animation>().Play("Attack");
        }
        else {
            //play wait animation

        }

        //Double tap e to dodge right, and double tap q to dodge left
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(dashCooldown > 0 && dashCount >= 1)
            {
                transform.Translate(dashAmount,0 ,0);
                //currentDash = Mathf.Lerp(currentDash, wantedDash, dashDamping * Time.deltaTime);
                //transform.position = new Vector3(currentDash, transform.position.y, transform.position.z);
            } else {
                dashCooldown = 0.5f;
                dashCount += 1;
            }
        } else if(Input.GetKeyDown(KeyCode.Q))
        {
            if(dashCooldown > 0 && dashCount >= 1)
            {
                transform.Translate(-dashAmount, 0, 0);
                //currentDash = Mathf.Lerp(currentDash, -1* wantedDash, dashDamping * Time.deltaTime);
                //transform.position = new Vector3(currentDash, transform.position.y, transform.position.z);
            } else
            {
                dashCooldown = 0.5f;
                dashCount += 1;
            }
        }

        //Code to handle the reseting of the cooldown on the dash
        if(dashCooldown > 0)
        {
            dashCooldown -= 1 * Time.deltaTime;
        } else
        {
            dashCooldown = 0;
        }

        //Code to move the player forward, back,left,right,and strafe
        var rotationMovement = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;
        var forwardMovement = Input.GetAxis("Vertical") * Time.deltaTime * forwardSpeed;
        var horizontalMovement = Input.GetAxis("Strafe") * Time.deltaTime * forwardSpeed;

        transform.Rotate(0, rotationMovement, 0);
        transform.Translate(horizontalMovement, 0, forwardMovement);
    }

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag ("ground"))
		{
			onGround = true;
		}
	}

}

