using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnitySpeechToText.Widgets;

public class OnParkGaze : MonoBehaviour {

	public RawImage parkImage;
	public AudioSource letsGoAudio;
	public bool playingAudio = false;
	public AudioSource confirmationAudio;
	public AudioSource notHeardAudio;
	public SpeechToTextComparisonWidget speechToTextComparisonWidget;
	private bool isRecording = false;

	public GameObject hintCube;
	public GameObject hintText;
	public GameObject newHintCube;
	public GameObject newHintText;

	public void playInstructionsOnGaze() {
		if (playingAudio == false && isRecording == false) {
			StartCoroutine(PlayAudio());
		}
	}

	public void playWrongAnswerAudio() {
		notHeardAudio.Play ();
	}

	public void turnOnRecording() {
		isRecording = true;
	}

	public void turnOffRecording() {
		isRecording = false;
	}

	public void startParkScene() {
		StartCoroutine (LoadNewSceneWithSoundConfirmation ());
	}

	public void lightUpImage () {
		Color color = parkImage.color;
		color.a = 1.0f;
		parkImage.color = color;

		hintCube.SetActive (true);
		hintText.SetActive (true);

		//newHintCube = Instantiate (hintCube, new Vector3(0.03f, 0.39f, 1.685f), Quaternion.identity) as GameObject;
		//newHintText = Instantiate (hintText, new Vector3 (0.076f, 0.44f, 1.5f), Quaternion.identity) as GameObject;
		Debug.Log (newHintCube);

	}

	public void dimImage () {
		Color color = parkImage.color;
		color.a = 0.8f;
		parkImage.color = color;

		hintCube.SetActive (false);
		hintText.SetActive (false);
	}

	IEnumerator PlayAudio() {
		playingAudio = true;
		Debug.Log ("Say 'Let's go' to begin!");
		letsGoAudio.Play();
		yield return new WaitForSeconds(letsGoAudio.clip.length);
		playingAudio = false;

		speechToTextComparisonWidget.StartPhrase ();
	}

	IEnumerator LoadNewSceneWithSoundConfirmation() {
		yield return new WaitForSeconds (0.2f);
		SceneManager.LoadScene ("Main");
	}
}
