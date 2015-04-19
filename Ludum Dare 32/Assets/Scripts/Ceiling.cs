using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Renderer))]
public class Ceiling : MonoBehaviour {

	public float fadeSpeed = 1.0f;
	private Renderer renderer;
	public bool turnDown = false;
	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (turnDown)
			turnOpacityDown ();
		else
			turnOpacityUp ();
	}

	void OnTriggerEnter(Collider other) {
		turnDown = true;
	}

	void OnTriggerExit(Collider other) {
		turnDown = false;
	}

	private void turnOpacityDown() {
		float alpha = renderer.material.color.a;
		if (alpha > 0)
			alpha -= Time.deltaTime * fadeSpeed;
		Color col = renderer.material.color;
		col.a = alpha;
		renderer.material.color = col;
	}

	private void turnOpacityUp() {
		float alpha = renderer.material.color.a;
		if (alpha < 1)
			alpha += Time.deltaTime * fadeSpeed;
		Color col = renderer.material.color;
		col.a = alpha;
		renderer.material.color = col;
	}
}
