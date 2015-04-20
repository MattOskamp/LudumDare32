using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float speed;
	private CharacterController cc;
	private Animator tor;
	private Vector3 mouseDirection;
	private float mouseAngle;
	public GameObject flashLight;
	public float batteryLife = 3;
	private float timer = 0;
	public float flickerTimer = 0;
	public Light flashSpotLight;
	public float keyCount = 0;
	private bool dead = false;
	private float deadTimer = 2.5f;

	public enum State {
		On,
		Off,
		Flickering
	}

	public State theState;

	Image youDied;

	void Start ()
	{
		cc = GetComponent<CharacterController>();
		tor = GetComponent<Animator>();
		theState = State.Off;
		flashLightOff ();
		keyCount = 0;
		youDied = GameObject.Find("YouDied").GetComponent<Image>();
	}

	void Update ()
	{
		if (dead)
		{
			deadTimer -= Time.deltaTime;
			if (deadTimer <= 0)
			{
				Application.LoadLevel("Title");
			}
		}

		switch (theState) {
		case State.On:
			if (Input.GetMouseButtonDown(0)) {
				theState = State.Off;
				flashLightOff();
				timer = 0;
			}

			// start the timer
			timer += Time.deltaTime;
			if (timer > batteryLife) {
				// flicker and turn off
				timer = 0;
				theState = State.Flickering;
			}

			break;
		case State.Off:
			if (Input.GetMouseButtonDown(0)) {
				theState = State.On;
				flashLightOn();
			}

			break;
		case State.Flickering:
			flickerTimer += Time.deltaTime;
			if (flickerTimer > 0 && flickerTimer < 0.1f)
				flashLightOff();
			else if (flickerTimer > 0.1f && flickerTimer < 0.5f)
				flashLightOn();
			else if (flickerTimer > 0.5f && flickerTimer < 0.8f)
				flashLightOff();
			else if (flickerTimer > 0.8f && flickerTimer < 1.0f)
				flashLightOn();
			else if (flickerTimer > 1.0f) {
				theState = State.Off;
				flashLightOff();
				flickerTimer = 0;
			}
			break;
		}
		
		//facing direction
		mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x)*Mathf.Rad2Deg-90;
		transform.rotation = Quaternion.AngleAxis(mouseAngle, Vector3.forward);
		
		/*if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			transform.rotation = Quaternion.LookRotation(Vector3.right*Input.GetAxis("Horizontal") + Vector3.up*Input.GetAxis("Vertical"), Vector3.back);
		}*/

		//movement direction
		if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
		{
			cc.Move(transform.right*Time.deltaTime*speed*Input.GetAxis("Horizontal")*Mathf.Sqrt(0.5f));
			cc.Move(transform.up*Time.deltaTime*speed*Input.GetAxis("Vertical")*Mathf.Sqrt(0.5f));
		}
		else if (Input.GetAxis("Horizontal") != 0)
		{
			cc.Move(transform.right*Time.deltaTime*speed*Input.GetAxis("Horizontal"));
		}
		else if (Input.GetAxis("Vertical") != 0)
		{
			cc.Move(transform.up*Time.deltaTime*speed*Input.GetAxis("Vertical"));
		}

		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			tor.SetBool("Walking", true);
		}
		else
		{
			tor.SetBool("Walking", false);
		}


	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Monster")
		{
			dead = true;
			youDied.enabled = true;
		}
	}

	public void flashLightOn() {
		flashLight.GetComponent<Renderer> ().enabled = true;
		flashSpotLight.enabled = true;
	}
	
	public void flashLightOff() {
		flashLight.GetComponent<Renderer> ().enabled = false;
		flashSpotLight.enabled = false;
	}


}
