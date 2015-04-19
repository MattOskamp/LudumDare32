using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public string name = "Key";
	public GameObject Player;
	public float rotateSpeed = 4.0f;
	private PlayerController playerController;

	// Use this for initialization
	void Start () {
		playerController = Player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.name);
		if (other.tag.Equals ("Navi")) {
			playerController.keyCount++;
			Destroy(gameObject);
			Debug.Log("destroy");
		}
	}
}
