using UnityEngine;
using System.Collections;

public class CharaterMover : MonoBehaviour {
	public int numberOfKeys = 0;

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

		Vector3 objectPos = Camera.main.WorldToScreenPoint(this.transform.position);
		Vector3 dir = Input.mousePosition - objectPos; 
		//transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg));
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Key") {
			numberOfKeys++;
			Destroy(other.gameObject);
		}
	}
}
