using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class BoatAI : MonoBehaviour {

	Animator myAnimator;

	[SerializeField]
	AudioClip clip;
	AudioSource source;

	public UnityEvent Onstart;
	bool playing;
	bool sinking;



	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator> ();
		source = GetComponent<AudioSource> ();

	
	}

	public void LookAt(){
		if (playing == false && sinking != true) {
			StartCoroutine (TalkAboutIcecream ());
			playing = true;
		}
	}

	IEnumerator TalkAboutIcecream(){
		source.Play ();
		yield return new WaitUntil (() => source.isPlaying == false);
		Onstart.Invoke ();
		yield return new WaitForSeconds (4);
		playing = false;
	}

	public void Sink(){
		sinking = true;
		myAnimator.SetTrigger ("Sink");
	}
}
