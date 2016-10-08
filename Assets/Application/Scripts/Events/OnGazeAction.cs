using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class OnGazeAction : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {

	public UnityEvent OnClick ();
	public UnityEvent OnEnter ();
	public UnityEvent OnExit ();


	public void OnPointerClick(PointerEventData data){

		OnClick.Invoke();
	}

	public void OnPointerEnter(PointerEventData data){

		OnEnter.Invoke();

	}

	public void OnPointerExit(PointerEventData data){

		OnExit.Invoke();
	}

}
