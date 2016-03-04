using UnityEngine;
using System.Collections;

public class FarmSquare : MonoBehaviour {

    public GameObject GroundTilePrefab;
    public GameObject PlantPrefab;

    public Material dryMat;
    public Material wetMat;

    public Material healthyMat;
    public Material notDyingMat;
    public Material withermat;

    private GameObject groundTile;
    private GameObject plant;

    public FarmTileData data;
    

	// Use this for initialization
	void Start () {
        groundTile = (GameObject)Instantiate(GroundTilePrefab, new Vector3(this.transform.position.x + 0.5f, 0, this.transform.position.z + 0.5f), GroundTilePrefab.transform.rotation);
        groundTile.transform.parent = this.transform;
        plant = (GameObject)Instantiate(PlantPrefab, new Vector3(this.transform.position.x + 0.5f, 0, this.transform.position.z + 0.5f), PlantPrefab.transform.rotation);
        plant.transform.parent = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(data.tilePos.x, 0, data.tilePos.y);
        // Hydration
        if (data.hydration > 0)
        {
            groundTile.GetComponentInChildren<MeshRenderer>().material = wetMat;
        }
        else
        {
            groundTile.GetComponentInChildren<MeshRenderer>().material = dryMat;
        }


        /*if (data.crop.health < 50)
        {
            plant.GetComponentInChildren<MeshRenderer>().material = withermat;
        }
        else if (data.crop.health < 90)
        {
            plant.GetComponentInChildren<MeshRenderer>().material = notDyingMat;
        }
        else if (data.crop.health >= 90)
        {
            plant.GetComponentInChildren<MeshRenderer>().material = healthyMat;
        }*/


        plant.GetComponent<MeshRenderer>().enabled = data.planted;
        // Crop Handling
        if (data.planted)
        {
            int stageIndexer = Mathf.Clamp(data.crop.stage - 1, 0, data.crop.identity.stages - 1);
            plant.GetComponent<MeshFilter>().mesh = data.crop.identity.stagesMeshs[stageIndexer];
        }

        // Player interaction
        if (PlayerControl.ThePlayer.interact && PlayerControl.ThePlayer.targetTile.Equals(data.tilePos))
        {
            data.Interact();
        }
    }
}

