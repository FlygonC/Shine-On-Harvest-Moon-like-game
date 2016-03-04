using UnityEngine;
using System.Collections;

public class Chicken : Entity {
    [Header("Chicken:")]
    public int age = 0;
    public bool beingCarried = false;

    public float walkTime = 0;
    public float idleTime = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        transform.rotation = Quaternion.Euler(0.0f, ((float)facing * 90.0f) - 90.0f, 0.0f);

        if (walkTime > 0)
        {
            switch (facing)
            {
                case Heading.North:
                    transform.position += new Vector3(0,0,walkSpeed) * Time.deltaTime;
                    break;
                case Heading.East:
                    transform.position += new Vector3(walkSpeed, 0, 0) * Time.deltaTime;
                    break;
                case Heading.South:
                    transform.position += new Vector3(0, 0, -walkSpeed) * Time.deltaTime;
                    break;
                case Heading.West:
                    transform.position += new Vector3(-walkSpeed, 0, 0) * Time.deltaTime;
                    break;
            }
        }
        idleTime -= Time.deltaTime;
        walkTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            idleTime = Random.Range(3, 8);
            walkTime = Random.Range(0.2f, 0.4f);
            int dir = Random.Range(0, 3);
            switch (dir)
            {
                case 0:
                    facing = Heading.North;
                    break;
                case 1:
                    facing = Heading.East;
                    break;
                case 2:
                    facing = Heading.South;
                    break;
                case 3:
                    facing = Heading.West;
                    break;
            }
        }

        if (beingCarried)
        {
            transform.position = PlayerControl.ThePlayer.transform.position + new Vector3(0, 2, 0);
        }
    }
}
