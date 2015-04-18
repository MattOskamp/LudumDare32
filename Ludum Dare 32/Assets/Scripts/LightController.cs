using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	public Transform target;
	public float speed = 5.0f;
	public float radius = 1;
	public float xOffset = 0;
	public float yOffset = 0;
	public float timer = 2;
	private float counter = 0;

	private Vector3 destination;

	// Use this for initialization
	void Start () {
		// find a destination around the target
		newOffsets ();
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter > timer) {
			newOffsets ();
			counter = 0;
		}
	}

	void FixedUpdate() {

		Vector3 newPosition = Vector3.zero;
		newPosition.x = Mathf.Lerp( this.transform.position.x, target.position.x + xOffset, Time.deltaTime * speed);
		newPosition.y = Mathf.Lerp( this.transform.position.y, target.position.y + yOffset, Time.deltaTime * speed);

		this.transform.position = newPosition;
	}

	void newOffsets() {
		yOffset = Random.Range (-radius / 2, radius / 2);
		xOffset = Random.Range (-radius / 2, radius / 2);
	}

}
