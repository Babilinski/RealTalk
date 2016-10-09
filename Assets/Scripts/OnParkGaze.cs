using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnParkGaze : MonoBehaviour {

	public RawImage parkImage;
	public AudioSource letsGoAudio;
	public bool playingAudio = false;
	public AudioSource confirmationAudio;

	public void playInstructionsOnGaze() {
		if (playingAudio == false) {
			StartCoroutine(PlayAudio());
		}
	}

	public void startParkScene() {
		StartCoroutine (LoadNewSceneWithSoundConfirmation ());
	}

	public void lightUpImage () {
		Color color = parkImage.color;
		color.a = 1.0f;
		parkImage.color = color;
	}

	public void dimImage () {
		Color color = parkImage.color;
		color.a = 0.8f;
		parkImage.color = color;
	}

	IEnumerator PlayAudio() {
		playingAudio = true;
		Debug.Log ("Say 'Let's go' to begin!");
		letsGoAudio.Play();
		yield return new WaitForSeconds(letsGoAudio.clip.length);
		playingAudio = false;

	}

	IEnumerator LoadNewSceneWithSoundConfirmation() {
		confirmationAudio.Play ();
		yield return new WaitForSeconds (confirmationAudio.clip.length);
		SceneManager.LoadScene ("Main");
	}
}
