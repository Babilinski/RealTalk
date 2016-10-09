using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DogAI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	private Animator thisAnimation;
	private GameObject LastPlayerLocation;

	private Transform player;
	private bool isGazing;

	private NavMeshAgent agent;

	Vector3 target;
	bool isSimple;


	public enum State {Talking,Simple	};
	public State currentState;

	[SerializeField]
	AudioClip[] Quest;
	AudioSource mySource;

	bool firstQuestStart;

	public UnityEvent firstQuestComplete;

	public UnityEvent secondQuestComplete;

	// Use this for initialization
	void Start () {
		thisAnimation = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
		mySource = GetComponent<AudioSource> ();
		StartCoroutine (AIUpdate ());
	}

	IEnumerator AIUpdate(){
		while (true) {
			switch (currentState) {
			case State.Simple:
				isSimple = true;
				if (agent.remainingDistance < .01f) {
					thisAnimation.SetBool ("Run", false);
					StartCoroutine (LookAtPlayer ());


				} else {
					thisAnimation.SetBool ("Run", true);
				}
				break;

			case State.Talking:
				isSimple = false;
				ComplexAI ();
				break;


			}

			yield return 0;
		}
		}
		
	
	// Update is called once per frame
	void ComplexAI() {


	
	}

	public void Instructions(){
		if (!firstQuestStart) {
			mySource.clip = Quest [0];
			mySource.Play ();
			StartCoroutine (firstQuest ());
			firstQuestStart = true;
		}

	}

	IEnumerator firstQuest(){

		yield return new WaitUntil (() => mySource.isPlaying == false);

		mySource.clip = Quest [1];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);

		firstQuestComplete.Invoke ();
	}
	


	public void Instructions2(Transform Forrest){
		mySource.clip = Quest [2];
		mySource.Play ();
		StartCoroutine (secondQuest (Forrest));

	}

	IEnumerator secondQuest(Transform forrest){
		yield return new WaitUntil (() => mySource.isPlaying == false);
		currentState = State.Simple;
		GoToTarget (forrest);
		secondQuestComplete.Invoke ();

	}

	public void HowAreYou(){


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
	if(isSimple)
		agent.SetDestination (target.position);

	}

	public void OnPointerExit(PointerEventData data){

		isGazing = false;
		ExitGaze ();
	}

}
