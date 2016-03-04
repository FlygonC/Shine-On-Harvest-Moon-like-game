using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

    private Text display;

	// Use this for initialization
	void Start () {
        display = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        //PlayerControl.Tool equiped = GameObject.FindObjectOfType<PlayerControl>().equipedTool;
        float munny = FindObjectOfType<PlayerControl>().tempMoney;
        float timee = FindObjectOfType<FarmManager>().tempClock;
        float dayy = FindObjectOfType<FarmManager>().day;

        display.text = "Time: " + timee + " Day: " + dayy + "\nMoney: " + munny;
	}
}
