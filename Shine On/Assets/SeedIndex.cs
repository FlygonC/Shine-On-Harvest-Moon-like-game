using UnityEngine;
using System.Collections;

public class SeedIndex : MonoBehaviour {

    [SerializeField]
    public Plant[] seedsIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Plant GetSeed(int _ind)
    {
        if (_ind >= 0 /*&& _ind < seedsIndex.Length*/)
        {
            return seedsIndex[_ind];
        }
        return seedsIndex[0];
    }
}
