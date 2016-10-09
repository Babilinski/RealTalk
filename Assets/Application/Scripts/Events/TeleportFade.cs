using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeleportFade : MonoBehaviour {

	public Canvas fadeCanvas; 

	void Start() {
		fadeOnTeleport ();
	}

	public void teleportToTransform(Transform playerPosition, Transform newPosition) {
		playerPosition.position = newPosition.position;
	}

	public void fadeOnTeleport() {
		
	}
	/*
	IEnumerator FadeToBlack() {
		
	}*/
}
