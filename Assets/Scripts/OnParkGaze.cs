using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnParkGaze : MonoBehaviour {

	public RawImage parkImage;

	public void lightUpImage () {
		Color color = parkImage.color;
		color.a = 1.0f;
		parkImage.color = color;
	}

	public void dimImage () {
		Color color = parkImage.color;
		color.a = 0.8f;
		parkImage.color = color;
	}
}
