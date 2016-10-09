using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnitySpeechToText.Widgets;

public class Recording : MonoBehaviour {
	[SerializeField]
	Transform recordingIcon;
	Animator myAnimation;
	AudioSource myAudio;
	[SerializeField]
	AudioClip end;
	[SerializeField]
	AudioClip start;

	// Use this for initialization
	void Start () {
		myAudio = GetComponent<AudioSource> ();
		myAnimation = recordingIcon.GetComponent<Animator> ();
		SpeechToTextServiceWidget.onRecord.AddListener (() => StartRecording ());
		SpeechToTextServiceWidget.onEndRecording.AddListener (() => EndRecording ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartRecording(){
		myAnimation.Play ("Start");
		myAudio.clip = start;
		myAudio.Play ();

	}

	public void EndRecording(){

		myAnimation.SetTrigger ("End");
		myAudio.clip = end;
		myAudio.Play ();
	}
}
