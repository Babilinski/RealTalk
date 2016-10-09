using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeleportFade : MonoBehaviour  {


	public bool teleported;

	public void teleportToTransform(Transform playerPosition, Transform newPosition) {

		if (!teleported) {
			teleported = true;
			Fader.Instance.FadeOutAndIn (() => {
				Vector3 TempPos = newPosition.position;
				TempPos.y = playerPosition.position.y;
				playerPosition.position = TempPos;
				teleported = false;

			});	

		}

	}




}
