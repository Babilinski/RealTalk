using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TeleportFade : MonoBehaviour {

	public Canvas fadeCanvas; 

	public void teleportToTransform(Transform playerPosition, Transform newPosition) {
		Vector3 TempPos = newPosition.position;
		TempPos.y = playerPosition.position.y;
		playerPosition.position = TempPos;
	}

	public void fadeOnTeleport() {
		StartCoroutine (FadeToBlack(1.0f, 1.0f));
	}

	IEnumerator FadeToBlack(float aValue, float aTime) {
		Color canvasImageColor = fadeCanvas.GetComponent<Image>().color;
		Debug.Log (canvasImageColor);
		float alpha = fadeCanvas.GetComponent<Image> ().color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha,aValue,t));
			fadeCanvas.GetComponent<Image>().color = newColor;
			yield return null;
		}
		yield return new WaitForSeconds (aTime);
		Color newCanvasColor = fadeCanvas.GetComponent<Image>().color;
		newCanvasColor.a = 0.0f;
		fadeCanvas.GetComponent<Image>().color = newCanvasColor;
	}
}
