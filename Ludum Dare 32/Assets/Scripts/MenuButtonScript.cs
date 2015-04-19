using UnityEngine;
using System.Collections;

public class MenuButtonScript : MonoBehaviour {

	Color normalColor = new Color(0.83f, 0.83f, 0.647f);
	Color highlightColor = new Color(0.8f,0.8f,0.2f);

	void Awake ()
	{
		GetComponent<Clickable>().DownAction += OnDownAction;
		GetComponent<Clickable>().UpAction += OnUpAction;
	}
	

	
	void OnDownAction (Vector3 position)
	{		
		this.GetComponent<SpriteRenderer>().color = highlightColor;
	}
	
	void OnUpAction (Vector3 position)
	{
		this.GetComponent<SpriteRenderer>().color = normalColor;

		if ( this.name == "PlayButton")
		{
			this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			this.transform.FindChild("Loading").gameObject.SetActive(true);
			Application.LoadLevel("Shadows");

		}
	}
	
	void OnMouseEnter()
	{
		if (!Input.touchSupported || Input.touches.Length > 0)
		{
			this.GetComponent<SpriteRenderer>().color = highlightColor;
		}
	}
	
	void OnMouseExit()
	{
		this.GetComponent<SpriteRenderer>().color = normalColor;
	}
	
}
