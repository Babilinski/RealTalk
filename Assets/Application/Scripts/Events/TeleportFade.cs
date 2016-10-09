using UnityEngine;
using System.Collections;

public class TeleportFade : MonoBehaviour {

	public void teleportToTransform(Transform playerPosition, Transform newPosition) {
		Vector3 TempPos = newPosition.position;
		TempPos.y = playerPosition.position.y;
		playerPosition.position = TempPos;
	}
}
