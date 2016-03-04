using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    
    public enum Heading { North = 0, East, South, West };
    [Header("Entity:")]
    public Heading facing;

    public float walkSpeed = 0.1f;
    public float collisionRadius = 0.4f;

    public Vector2 groundPosition
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
        set
        {
            transform.position = new Vector3(value.x, 0, value.y);
        }
    }
    public TilePosition tilePos
    {
        get
        {
            TilePosition ret = new TilePosition();
            ret.x = (int)groundPosition.x;
            ret.y = (int)groundPosition.y;
            return ret;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        if (transform.position.y > 0)
        {
            transform.position -= new Vector3(0, 0.1f, 0);
            if (transform.position.y < 0)
            {
                transform.position -= new Vector3(0, transform.position.y, 0);
            }
        }
	}

    public bool hitTest(Entity _other)
    {
        float dist = Mathf.Abs((groundPosition - _other.groundPosition).magnitude);
        return dist < collisionRadius + _other.collisionRadius;
    }
}

