using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

	public Transform target;
	public bool on;
	public Component vlsRadial;

	// Use this for initialization
	void Start () {
		on = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		this.transform.position = target.transform.position;
		//facing direction
		Vector2 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x)*Mathf.Rad2Deg + 90;
		transform.rotation = Quaternion.AngleAxis(mouseAngle, Vector3.forward);
	}
}
