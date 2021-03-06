﻿using UnityEngine;
using System.Collections;

public class MonsterBrain : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private float speed;
	private CharacterController cc;
	private Vector3 dir, leftR, rightR;
	private float angle;
	private RaycastHit hit;

	//state 0 is idle/frozen
	//state 1 is chasing
	//state 2 is dying
	private int state = 1;

	void Start ()
	{
		cc = GetComponent<CharacterController>();
	}

	void Update ()
	{
		//constant artificial gravity
		cc.Move(Vector3.forward*Time.deltaTime);

		//if we're in chase mode, hunt the player
		if (state == 1)
		{
			/*dir = (target.position - transform.position);
			if(Physics.Raycast(transform.position, transform.up, out hit, speed))
			{
				if(hit.transform != transform && hit.transform != target)
				{
					dir += hit.normal*50;
				}
			}

			leftR = transform.position + (transform.right * -0.5f);
			rightR = transform.position + (transform.right * 0.5f);
			
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
			//transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.AngleAxis(angle, Vector3.forward),Time.deltaTime);
			//transform.position += transform.up * speed * Time.deltaTime;*/

			//rotation toward player
			dir = target.position - transform.position;
			angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg-90;
			transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.AngleAxis(angle, Vector3.forward),Time.deltaTime);

			//movement forward
			cc.Move(transform.up * speed * Time.deltaTime);
		}
	}
}
