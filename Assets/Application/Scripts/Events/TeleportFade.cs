using UnityEngine;
using System.Collections;

public class TeleportFade : MonoBehaviour {

	public void teleportToTransform(Transform playerPosition, Transform newPosition) {
		playerPosition.position = newPosition.position;
	}
}
