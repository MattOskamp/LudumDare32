﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float speed;
	private CharacterController cc;
	private Animator tor;
	private Vector3 mouseDirection;
	private float mouseAngle;

	void Start ()
	{
		cc = GetComponent<CharacterController>();
		tor = GetComponent<Animator>();
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

		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			tor.SetBool("Walking", true);
		}
		else
		{
			tor.SetBool("Walking", false);
		}

		//facing direction
		mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		mouseAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x)*Mathf.Rad2Deg-90;
		transform.rotation = Quaternion.AngleAxis(mouseAngle, Vector3.forward);

		/*if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			transform.rotation = Quaternion.LookRotation(Vector3.right*Input.GetAxis("Horizontal") + Vector3.up*Input.GetAxis("Vertical"), Vector3.back);
		}*/
	}

}
