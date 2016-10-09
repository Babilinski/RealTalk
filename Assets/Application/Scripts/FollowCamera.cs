using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	[SerializeField]
	Transform cardboard;

	// Use this for initialization
	void Start () {
		//player = Camera.main.transform;
		StartCoroutine (LookAtPlayer ());
	}
	
	IEnumerator LookAtPlayer(){

		while (true) {


			Quaternion	neededRotation = cardboard.rotation;
		

				transform.rotation = Quaternion.Slerp (transform.rotation, neededRotation, Time.deltaTime );

			yield return 0;
		}


	}
}
