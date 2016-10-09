using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SmallGIrlAI : MonoBehaviour {

	AudioSource mySource;
	public AudioClip[] myAudios;
	public UnityEvent OnSaidName;
	public UnityEvent OnSaidTime;


	void Start(){

		mySource = GetComponent<AudioSource> ();
	}

	public void GotNameWrong(){
		mySource.clip = myAudios [0];
		mySource.Play ();

	}

	public void GotNameRight(){
		StartCoroutine (StartSayingName ());

	}

	IEnumerator StartSayingName(){
		mySource.clip = myAudios [1];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);
		OnSaidName.Invoke ();
	}

	public void AskForTime(){
		StartCoroutine (StartSayingTime ());
	}

	IEnumerator StartSayingTime(){
		mySource.clip = myAudios [2];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);
		OnSaidTime.Invoke ();
	}


}
