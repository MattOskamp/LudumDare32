using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float speed;
	private CharacterController cc;

	void Start ()
	{
		cc = GetComponent<CharacterController>();
	}

	void Update ()
	{
		if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
		{
			float angle = Mathf.Atan2(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal")));

			float xSpeed = speed*Mathf.Cos(angle);
			if (Input.GetAxis("Horizontal") < 0)
				xSpeed *= -1;
			cc.Move(Vector3.right*Time.deltaTime*xSpeed);

			float ySpeed = speed*Mathf.Sin(angle);
			if (Input.GetAxis("Vertical") < 0)
		        ySpeed *= -1;
			cc.Move(Vector3.up*Time.deltaTime*ySpeed);
		}
		else if (Input.GetAxis("Horizontal") != 0)
		{
			cc.Move(Vector3.right*Time.deltaTime*speed*Input.GetAxis("Horizontal"));
		}
		else if (Input.GetAxis("Vertical") != 0)
		{
			cc.Move(Vector3.up*Time.deltaTime*speed*Input.GetAxis("Vertical"));
		}
	}

}
