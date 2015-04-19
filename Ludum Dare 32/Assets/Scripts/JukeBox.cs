using UnityEngine;
using System.Collections;

public class JukeBox : MonoBehaviour {

	private AudioSource sound;
	[SerializeField] private AudioClip[] musics;
	private int currentlyPlaying;

	void Start ()
	{
		sound = GetComponent<AudioSource>();
		for (int i = musics.Length-1; i>0; i--)
		{
			int r = Random.Range(0,i);
			AudioClip temp = musics[i];
			musics[i] = musics[r];
			musics[r] = temp;
			Debug.Log(i);
		}
		currentlyPlaying = 0;
	}
	
	void Update ()
	{
		if (!sound.isPlaying)
		{
			currentlyPlaying++;
			if (currentlyPlaying >= musics.Length)
			{
				currentlyPlaying = 0;
			}
			sound.clip = musics[currentlyPlaying];
			sound.Play();
		}
	}
}
