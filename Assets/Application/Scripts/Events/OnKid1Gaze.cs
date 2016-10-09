using UnityEngine;
using System.Collections;
using UnitySpeechToText.Widgets;
using UnityEngine.Events;

public class OnKid1Gaze : MonoBehaviour {

	public Transform TeleportKid1Location;
	public Transform newPositionGameObject;
	private Vector3 newPosition;
	public Transform playerLocation;
	public int index = 0;
	public AudioSource kid1Prompt;
	public Transform dog;
	public AudioSource dogPrompt;
	public TeleportFade teleportFadeScript;

	public AudioClip wrongName;
	public AudioClip myName;
	public UnityEvent OnCompleteName;



	public SpeechToTextComparisonWidget speechToTextComparisonWidget;


	public void kidPromptForTime() {
		if (index == 0) {
			StartCoroutine(delayStartRecording(dogPrompt, 0.5f));
			index = 1;
		}
		Debug.Log (index);
	}

	public void GotWrongName(){
		kid1Prompt.clip = wrongName;
		kid1Prompt.Play ();

	}

	public void GotNameRight(){
		print ("correct");
		kid1Prompt.clip = myName;
		kid1Prompt.Play ();

	}


	IEnumerator NameWasRight(){

		yield return new WaitUntil (() => kid1Prompt.isPlaying == false);
		OnCompleteName.Invoke ();
	}




	public void kidCorrectPrompt() {
		Debug.Log ("You are correct!");
		StartCoroutine (playDialog(kid1Prompt, 0.0f));
	}
	// delays recording until the audio finishes playing
	IEnumerator delayStartRecording (AudioSource audioSource, float timeToDelay) {
		audioSource.Play ();
		yield return new WaitForSeconds (audioSource.clip.length);
		speechToTextComparisonWidget.StartPhrase ();
	} 

	// Plays dialog with a delay of user set seconds if needed
	IEnumerator playDialog(AudioSource audioSource, float timeToDelayFirstPlay) {
		yield return new WaitForSeconds (timeToDelayFirstPlay);
		audioSource.Play ();
		yield return new WaitForSeconds (audioSource.clip.length);

	}

	IEnumerator delayIndexUp(float time) {
		yield return new WaitForSeconds (time);
		index++;
	}
	
	
}
