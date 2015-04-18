using UnityEngine;
using System.Collections;

public class MonsterBrain : MonoBehaviour {

	[SerializeField] private float speed;
	private CharacterController cc;
	private Transform target = null;
	
	void Start ()
	{
		cc = GetComponent<CharacterController>();
	}

	void Update ()
	{

	}
}
