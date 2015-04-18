using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

	public Transform player;
	public float speed = 1.8f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// do the ray cast for the player
		RaycastHit hit;
		Vector3 toPlayer = this.player.position - this.transform.position;
		Debug.DrawLine(this.transform.position, this.player.position, Color.blue);
		if (Physics.Raycast (this.transform.position, toPlayer, out hit)) {
			if (hit.collider.gameObject.tag == "Player")
				Debug.DrawLine(this.transform.position, this.player.position, Color.green);
			else
				Debug.DrawLine(this.transform.position, this.player.position, Color.red);
		}

		// raycast to see if the enemy will run into a wall

	}

	void FixedUpdate() {
		Vector3 heading = player.position - this.transform.position;
		heading.Normalize ();
		//this.transform.position += heading * speed * Time.deltaTime;
	}
}
