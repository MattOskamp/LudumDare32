using UnityEngine;
using System.Collections;

public class CharaterMover : MonoBehaviour {

	CharacterController controller;
	public float speed = 2.0f;

	// Use this for initialization
	void Start () {
		controller = new CharacterController ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;
		transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
	}
}
