using UnityEngine;
using System.Collections;

public class MiraMira : MonoBehaviour {


	// Use this for initialization
	IEnumerator Start () {
		while (true) {

			yield return new WaitForSeconds (5);
			GetComponent<AudioSource> ().Play ();
			yield return 0;
		}
	}
	

}
