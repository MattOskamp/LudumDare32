using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyGuiScript : MonoBehaviour {

	private PlayerController playerController;


	// Use this for initialization
	void Start () {
		playerController = GameObject.Find("MainCharacter").GetComponent<PlayerController> ();

	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text>().text = "" + playerController.keyCount + "/6";
	
	}
}
