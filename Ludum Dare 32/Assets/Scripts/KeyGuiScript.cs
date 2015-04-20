using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyGuiScript : MonoBehaviour {

	private PlayerController playerController;
	Image gameOver;

	// Use this for initialization
	void Start () {
		playerController = GameObject.Find("MainCharacter").GetComponent<PlayerController> ();
		gameOver = GameObject.Find("GameOver").GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text>().text = "" + playerController.keyCount + "/6";
		if (playerController.keyCount == 6)
		{
			gameOver.enabled = true;
		}
	
	}
}
