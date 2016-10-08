using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DogAI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	private Animator thisAnimation;
	private GameObject LastPlayerLocation;

	private Transform player;
	private bool isGazing;

	private NavMeshAgent agent;

	Vector3 target;

	// Use this for initialization
	void Start () {
		thisAnimation = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {


			if (agent.remainingDistance < .01f) {
				thisAnimation.SetBool ("Run", false);
				StartCoroutine (LookAtPlayer ());


			} else {
				thisAnimation.SetBool ("Run", true);
			}

	
	}

	public void GazedAt(){

		thisAnimation.SetBool ("Talking", true);
	}

	public void ExitGaze(){

		thisAnimation.SetBool ("Talking", false);
	}




	public void OnPointerEnter(PointerEventData data){
		isGazing = true;
		GazedAt ();
		StartCoroutine (LookAtPlayer ());

	}

	IEnumerator LookAtPlayer(){


		Vector3 targetPosition = player.position;
		targetPosition.y = transform.position.y;

		Quaternion	neededRotation = Quaternion.LookRotation(targetPosition - transform.position);
		while (transform.rotation != neededRotation) {
			transform.rotation = Quaternion.Slerp (transform.rotation, neededRotation, Time.deltaTime / 2);
			yield return 0;
		}


	}
		

	public void GoToTarget(Transform target){

		agent.SetDestination (target.position);

	}

	public void OnPointerExit(PointerEventData data){

		isGazing = false;
		ExitGaze ();
	}

}
