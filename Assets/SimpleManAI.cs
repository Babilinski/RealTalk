using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SimpleManAI : MonoBehaviour {

	private AudioSource mySource;

	public AudioClip[] soundClips;

	public UnityEvent OnDoneIntro;


	void Start(){
		mySource = GetComponent<AudioSource> ();

	}
	public void StartBuying(){
		StartCoroutine (SayItems ());

	}


	IEnumerator SayItems(){
		mySource.clip = soundClips [0];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);
		OnDoneIntro.Invoke ();
	}



}
