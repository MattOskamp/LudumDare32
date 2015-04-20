using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

	public Transform light;
	public float speed = 3.5f;
	[SerializeField] private Transform target;
	private Vector3 dir, leftR, rightR;
	private float angle;
	public float range = 8.0f;
	public float health = 1f;
	public float flashLightRange = 5.0f;
	public GameObject smoke;
	private PlayerController playerController;

	public enum State {
		Chasing,
		Idle,
		Dying
	}

	public State theState;

	// Use this for initialization
	void Start () {
		theState = State.Chasing;
		playerController = target.GetComponent<PlayerController> ();
	}

	public void setDying() {
		theState = State.Dying;
	}

	public void setIdle() {
		theState = State.Idle;
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;

		switch (theState) {
		case State.Chasing:
			break;
		case State.Dying:
			health -= Time.deltaTime;
			if (health < 0)
				die();
			break;
		case State.Idle:
			break;
		}

		// do the ray cast for the light
		Vector3 lightPosition = this.light.position;
		Vector3 toLight = lightPosition - this.transform.position;
		//Debug.DrawLine(this.transform.position, lightPosition, Color.blue);
		if (Physics.Raycast (this.transform.position, toLight, out hit, range)) {
			if (hit.collider.gameObject.tag == "Navi") {
				//Debug.DrawLine (this.transform.position, lightPosition, Color.green);
				// is in the light so change state to idle
				theState = State.Idle;
			} else {
				//Debug.DrawLine (this.transform.position, lightPosition, Color.red);
				theState = State.Chasing;
			}
		} else {
			// out of range can start chasing
			theState = State.Chasing;
		}

		Vector3 toPlayer = this.target.position - transform.position;
		// do a raycast for the player
		if (Physics.Raycast (this.transform.position, toPlayer, out hit, flashLightRange)) {
			if (hit.collider.gameObject.tag.Equals ("Player")) {
				if (playerController.theState == PlayerController.State.On) {
					// flash light is on, is it in view of me?
					Vector3 toEnemy = this.transform.position - this.target.position;
					toEnemy.Normalize ();
					float dotProd = Vector3.Dot (this.target.up, toEnemy);
					float angle = Mathf.Rad2Deg * dotProd;
					if (angle > 50.0f && angle < 60.0f) {
						// start dying
						theState = State.Dying;
					}
					
				}
			}
		}
	}

	void FixedUpdate() {
		switch (theState) {
		case State.Chasing:
			dir = (target.position - transform.position);
			
			leftR = transform.position + (transform.right * -0.5f);
			rightR = transform.position + (transform.right * 0.5f);
			RaycastHit hit;
			if(Physics.Raycast(leftR, transform.up, out hit, speed))
			{
				if(hit.transform != transform && hit.transform != target)
				{
					dir += hit.normal*20;
				}
			}
			
			if(Physics.Raycast(rightR, transform.up, out hit, speed))
			{
				if(hit.transform != transform && hit.transform != target)
				{
					dir += hit.normal*20;
				}
			}
			
			angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg-90;
			transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.AngleAxis(angle, Vector3.forward),Time.deltaTime);
			transform.position += transform.up * speed * Time.deltaTime;
			break;
		case State.Dying:
			break;
		case State.Idle:
			break;
		}
	}

	public void die() {
		// smoke effect here
		// instantiate smoke prefab here and destroy
		GameObject.Instantiate (smoke, this.transform.position, this.transform.rotation);
		Destroy (gameObject);
	}
}
