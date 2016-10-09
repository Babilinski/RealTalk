using UnityEngine;
using System.Collections;

public class FaderDemo : MonoBehaviour 
{

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("space")) 
		{
			Fader.Instance.FadeOutAndIn(() => 
            {
				Debug.Log ("I'm faded out. I can do something now before fading back in");
			});	
		}
	
	}
}
