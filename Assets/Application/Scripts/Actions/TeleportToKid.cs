using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TeleportToKid : MonoBehaviour, IPointerEnterHandler {

	public GameObject player;
	public Canvas canvas;
	public Image canvasImage;

	void Start() {
		teleportToPerson ();
	}
	public void OnPointerEnter(PointerEventData data){
		Debug.Log ("hey!");
		teleportToPerson ();
	}

	public void teleportToPerson() {
		Debug.Log ("Clicked");
		StartCoroutine (TeleportFade (1.0f));

	}

	IEnumerator TeleportFade(float aTime) {
		float alpha = canvasImage.color.a;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha,1.0f,t));
			canvasImage.color = newColor;
			yield return null;
		}
		yield return new WaitForSeconds (1.0f);

		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha,0.0f,t));
			canvasImage.color = newColor;
			yield return null;
		}

	}
}
