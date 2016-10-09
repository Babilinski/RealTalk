/*
 * Copyright (c) 2015-2016 Peter Koch <peterept@gmail.com>
 * 
 * Purpose: Fade in and out at any time during a level - with a callback when we've faded out
 */
using UnityEngine;
using System.Collections;
using System;

public class Fader : MonoBehaviour 
{
	#region Singleton Instance
	
	private static Fader _instance;
	static public Fader Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<Fader>();
			}
			return _instance;
		}
	}
	
	#endregion
	
	
	public Animation fadeAnimation;
	public string FadeInName = "FadeIn";
	public string FadeOutName = "FadeOut";
	public bool FadeInOnStart = true;

	void Start()
	{
		if (FadeInOnStart)
		{
			FadeIn();
		}
	}
	
	#region API
	
	public void FadeIn(Action fadedInCallback = null)
	{
		StartCoroutine(FadeInCoroutine(fadedInCallback));
	}
	
	public void FadeOut(Action fadedOutCallback)
	{
		StartCoroutine(FadeOutCoroutine(fadedOutCallback));
	}
	
	public void FadeOutAndIn(Action fadedOutCallback = null, Action fadedInCallback = null)
	{
		FadeOut (() => 
		         {
			if (fadedOutCallback != null) 
			{
				fadedOutCallback ();
			}
			FadeIn(fadedInCallback);
		});
	}
	
	#endregion
	
	IEnumerator FadeInCoroutine(Action fadedInCallback = null) 
	{
		fadeAnimation.Play (FadeInName);
		yield return new WaitForSeconds(fadeAnimation.GetClip (FadeInName).length); 
		if (fadedInCallback != null) 
		{
			fadedInCallback ();
		}
	}
	
	IEnumerator FadeOutCoroutine(Action fadedOutCallback = null) 
	{
		fadeAnimation.Play (FadeOutName);
		yield return new WaitForSeconds(fadeAnimation.GetClip (FadeOutName).length); 
		if (fadedOutCallback != null) 
		{
			fadedOutCallback ();
		}
	}
}
