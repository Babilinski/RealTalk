using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SmallGIrlAI : MonoBehaviour {

	AudioSource mySource;
	public AudioClip[] myAudios;
	public UnityEvent OnSaidName;
	public UnityEvent OnSaidTime;

	bool TryTime;




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
		TryTime = true;
		yield return new WaitUntil (() => mySource.isPlaying == false);
		OnSaidName.Invoke ();
	}

	public void AskForTime(){
		StartCoroutine (StartSayingTime ());
	}

	public void AskForTimeAgain(){
		if (TryTime) {
			StartCoroutine (WaitForResponce ());
		}
	}


	IEnumerator WaitForResponce(){
		while (TryTime = true) {

			yield return new WaitForSeconds (10);
			if (TryTime != true)
				yield break;
			OnSaidName.Invoke ();
			yield return 0;
		}

	}

	public void ClickOnher(){

		OnSaidName.Invoke ();
	}

	IEnumerator StartSayingTime(){
		TryTime = false;
		mySource.clip = myAudios [2];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);
		OnSaidTime.Invoke ();
	}


}
