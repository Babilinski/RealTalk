using UnityEngine;
using System.Collections;

public class recenterOnStart : MonoBehaviour {

	public GvrViewer viewer;

	// Use this for initialization
	void Start () {
		viewer.Recenter ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
