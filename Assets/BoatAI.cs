using UnityEngine;
using System.Collections;

public class BoatAI : MonoBehaviour {

	Animator myAnimator;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Sink(){

		myAnimator.SetTrigger ("Sink");
	}
}
