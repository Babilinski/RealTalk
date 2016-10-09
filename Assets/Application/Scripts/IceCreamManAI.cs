using UnityEngine;
using System.Collections;
using UnitySpeechToText.Widgets;
using UnityEngine.Events;

public class IceCreamManAI : MonoBehaviour {

	public int index = 0;
	bool manQuestStart = false;
	public SpeechToTextComparisonWidget speechToTextComparisonWidget;
	public UnityEvent OnFirstAction;

	[SerializeField]
	public AudioClip[] Quest;
	public AudioSource mySource;

	public void manGreeting() {
		if (!manQuestStart) {
			Debug.Log ("Testing Man Greeting");
			StartCoroutine (manGreetingUpdate ());
			manQuestStart = true;
		}
	}

	IEnumerator manGreetingUpdate (){
		mySource.clip = Quest[0];
		mySource.Play ();
		yield return new WaitUntil(() => mySource.isPlaying == false);
		OnFirstAction.Invoke ();
		speechToTextComparisonWidget.StartPhrase ();
	}



	public void respondFoodChoiceCorrect(){
		mySource.clip = Quest[1];
		mySource.Play ();
		print ("Correct answer");
	}

	public void respondFoodChoiceWrong(){
		mySource.clip = Quest[4];
		mySource.Play ();
		print ("WRONG!!!");
	}

}