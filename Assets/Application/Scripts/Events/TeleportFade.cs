using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeleportFade : MonoBehaviour {

	public Canvas fadeCanvas; 

	void Start() {
		fadeOnTeleport ();
	}

	public void teleportToTransform(Transform playerPosition, Transform newPosition) {
		Vector3 TempPos = newPosition.position;
		TempPos.y = playerPosition.position.y;
		playerPosition.position = TempPos;
	}

	public void fadeOnTeleport() {
		
	}
	/*
	IEnumerator FadeToBlack() {
		
	}*/
}
