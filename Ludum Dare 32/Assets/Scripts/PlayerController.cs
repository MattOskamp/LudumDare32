using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float speed;
	private CharacterController cc;
	private Vector3 screenPoint;
	private float distMX, distMY;

	void Start ()
	{
		cc = GetComponent<CharacterController>();
	}

	void Update ()
	{
		//movement direction
		if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
		{
			cc.Move(Vector3.right*Time.deltaTime*speed*Input.GetAxis("Horizontal")*Mathf.Sqrt(0.5f));
			cc.Move(Vector3.up*Time.deltaTime*speed*Input.GetAxis("Vertical")*Mathf.Sqrt(0.5f));
		}
		else if (Input.GetAxis("Horizontal") != 0)
		{
			cc.Move(Vector3.right*Time.deltaTime*speed*Input.GetAxis("Horizontal"));
		}
		else if (Input.GetAxis("Vertical") != 0)
		{
			cc.Move(Vector3.up*Time.deltaTime*speed*Input.GetAxis("Vertical"));
		}

		//facing direction
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		transform.rotation = Quaternion.LookRotation(Input.mousePosition - screenPoint, Vector3.back);

		/*if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			transform.rotation = Quaternion.LookRotation(Vector3.right*Input.GetAxis("Horizontal") + Vector3.up*Input.GetAxis("Vertical"), Vector3.back);
		}*/
	}

}
