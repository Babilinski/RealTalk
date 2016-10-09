using UnityEngine;
using System.Collections;
using UnitySpeechToText.Widgets;

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

	public SpeechToTextComparisonWidget speechToTextComparisonWidget;

	public void teleportKid() {
		if (index == 0) {
			teleportFadeScript.teleportToTransform (playerLocation, newPositionGameObject);
			/*
			newPosition = TeleportKid1Location.position;
			playerLocation.position = newPosition;
			Debug.Log (newPosition);
			*/
		}
		if (index == 0) {
			StartCoroutine (delayIndexUp (0.4f));
			// StartCoroutine (playDialog (kid1Prompt, 1.0f));
		}

	}

	public void kidPromptForTime() {
		if (index == 1) {
			StartCoroutine(delayStartRecording(dogPrompt, 0.5f));
		}
		Debug.Log (index);
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
