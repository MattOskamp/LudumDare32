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

	public enum State {
		Follow,
		Chase
	}
	public State theState;

	// Use this for initialization
	void Start () {
		// find a destination around the target
		newOffsets ();
		theState = State.Follow;
	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		if (counter > timer) {
			newOffsets ();
			counter = 0;
		}

		if (Input.GetMouseButtonDown (1)) {
			// fly to that spot and come back
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				destination = hit.point;
				destination.z = 0;
			}
			theState = State.Chase;
		}
	}

	void FixedUpdate() {
		Vector3 newPosition = Vector3.zero;

		switch (theState) {
		case State.Follow:
			newPosition.x = Mathf.Lerp (this.transform.position.x, target.position.x + xOffset, Time.deltaTime * speed);
			newPosition.y = Mathf.Lerp (this.transform.position.y, target.position.y + yOffset, Time.deltaTime * speed);
			
			this.transform.position = newPosition;
			break;
		case State.Chase:
			newPosition.x = Mathf.Lerp (this.transform.position.x, destination.x, Time.deltaTime * speed);
			newPosition.y = Mathf.Lerp (this.transform.position.y, destination.y, Time.deltaTime * speed);
			
			this.transform.position = newPosition;
			if (Vector3.Distance(destination, this.transform.position) < 0.001f) {
				theState = State.Follow;

			}
			break;
		}
	}

	void newOffsets() {
		yOffset = Random.Range (-radius / 2, radius / 2);
		xOffset = Random.Range (-radius / 2, radius / 2);
	}

}
