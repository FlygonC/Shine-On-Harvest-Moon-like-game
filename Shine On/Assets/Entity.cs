using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    
    public enum Heading { North = 0, East, South, West };
    [Header("Entity:")]
    public Heading facing;

    public float walkSpeed = 0.1f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
