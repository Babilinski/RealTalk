using UnityEngine;
using System.Collections;
using UnitySpeechToText.Widgets;

public class IceCreamScript : MonoBehaviour {

	bool iceCreamQuestStart = false;
	public GameObject audioGameObject;
	public AudioSource iceCreamAudio;

	public void wantIceCream() {
		if (!iceCreamQuestStart) {
			StartCoroutine (wantIceCreamUpdate ());
			iceCreamQuestStart = true;
		}
	}

	IEnumerator wantIceCreamUpdate (){
		//iceCreamAudio.clip = audioGameObject.transform.GetComponent<IceCreamManAI> ().Quest [15];
		iceCreamAudio.Play ();
		yield return new WaitUntil(() => iceCreamAudio.isPlaying == false);
	}
}
