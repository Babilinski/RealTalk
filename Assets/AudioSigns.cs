using UnityEngine;
using System.Collections;

public class AudioSigns : MonoBehaviour {

	public AudioSource myAudio;
	public GameObject soundIcons;


	bool wasPlaying;
	// Use this for initialization
	void Start () {
		if (myAudio)
		myAudio = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
			if (myAudio.isPlaying) {
			wasPlaying = true;
				soundIcons.SetActive (true);
			} else {
			if (wasPlaying) {
				soundIcons.SetActive (false);
				wasPlaying = false;
			}

			}


		}
	
	}
	

