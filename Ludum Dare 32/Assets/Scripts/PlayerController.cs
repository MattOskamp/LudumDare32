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
	}

}
