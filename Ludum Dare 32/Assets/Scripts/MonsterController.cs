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
	private CharacterController cc;
	private Animator tor;
	[SerializeField] private GameObject child;
	[SerializeField] private float playerDist;
	[SerializeField] private float detectionDistance;

	public enum State {
		Chasing,
		Idle,
		Dying
	}

	public State theState;

	// Use this for initialization
	void Start () {
		playerController = target.GetComponent<PlayerController> ();
		cc = GetComponent<CharacterController>();
		tor = child.GetComponent<Animator>();
		setIdle();
	}

	public void setDying() {
		theState = State.Dying;
		tor.speed = 0;
	}
	
	public void setIdle() {
		theState = State.Idle;
		tor.speed = 0;
	}
	
	public void setChasing() {
		theState = State.Chasing;
		tor.speed = 1;
	}
	
	// Update is called once per frame
	void Update () {

		playerDist = (target.position - transform.position).sqrMagnitude;

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
				setIdle();
			} else if (playerDist <= detectionDistance*detectionDistance) {
				//Debug.DrawLine (this.transform.position, lightPosition, Color.red);
				setChasing();
			}
		} else if (playerDist <= detectionDistance*detectionDistance) {
			// out of range can start chasing
			setChasing();
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
						setDying();
					}
					
				}
			}
		}
	}

	void FixedUpdate() {

		//constant artificial gravity, in case of collision weirdness
		cc.Move(Vector3.forward*Time.deltaTime);

		switch (theState) {
		case State.Chasing:

			//rotation toward player
			dir = target.position - transform.position;
			angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg-90;
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime);
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			
			//movement forward
			cc.Move(transform.up * speed * Time.deltaTime);

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
