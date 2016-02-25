using UnityEngine;
using System.Collections;

public class PlayerControl : Entity {

    public enum Heading { North = 0, East, South, West };
    public Heading facing;
    public enum Tool { WaterCan, Hoe };
    public Tool equipedTool = Tool.WaterCan;
    public float walkSpeed = 0.1f;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(0.0f, (float)facing * 90.0f, 0.0f);
        // Face Direction of Axis
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.5f)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                facing = Heading.North;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                facing = Heading.South;
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                facing = Heading.East;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                facing = Heading.West;
            }
        }
        // Move
        Vector3 rawMovement = new Vector3(Input.GetAxis("Horizontal") * walkSpeed, 0, Input.GetAxis("Vertical") * walkSpeed);
        if (rawMovement.magnitude > 1)
        {
            transform.position += rawMovement.normalized;
        } else
        {
            transform.position += rawMovement;
        }
	}
}
