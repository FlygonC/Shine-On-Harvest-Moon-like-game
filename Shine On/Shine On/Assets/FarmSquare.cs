using UnityEngine;
using System.Collections;

public class FarmSquare : MonoBehaviour {

    public Material dryMat;
    public Material wetMat;

    private GameObject groundTile;

    public float hydration = 0;
    public bool hydrated
    {
        get
        {
            return hydration > 0;
        }
    }
    public bool planted = false;
    public Crop plantedCrop;
    

	// Use this for initialization
	void Start () {
	    //groundTile = gameObject.ch
	}
	
	// Update is called once per frame
	void Update () {
        // Hydration
        if (hydration > 0)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = wetMat;
            hydration -= Random.Range(1.0f, 2.0f) * Time.deltaTime;
            if (hydration < 0)
            {
                hydration = 0;
            }
        }
        else
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material = dryMat;
        }
        // Crop Handling
        if (planted)
        {
            if (hydrated)
            {
                plantedCrop.growth += (1 * Time.deltaTime) / 60;
            }
            else
            {
                plantedCrop.life -= (1 * Time.deltaTime) / 60;
            }
        }
	}

    public void Interact(PlayerControl.Tool _usedTool)
    {
        switch (_usedTool) {
            case PlayerControl.Tool.WaterCan:
                hydration += 60;
                break;
            default:
                break;
        }
    }
}

[System.Serializable]
public class Crop
{
    public float life = 100;
    public float growth = 0;
}

[System.Serializable]
public class Plant
{
    public float life = 100;
}