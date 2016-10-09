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
	bool askName;

	bool playingSound;

	public UnityEvent firstQuestComplete;

	public UnityEvent secondQuestComplete;

	public UnityEvent askHerNameComplete;
	public UnityEvent askNameWasWrong;
	public UnityEvent askNameWasCorrect;

	public UnityEvent nowAskForTime;


	bool NameWasCorrect;


	public TeleportFade fade;

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
		currentState = State.Talking;
		playingSound = true;
		yield return new WaitUntil (() => mySource.isPlaying == false);

		mySource.clip = Quest [1];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);

		firstQuestComplete.Invoke ();
		playingSound = false;
	}
	


	public void Instructions2(Transform Forrest){
		currentState = State.Talking;
		mySource.clip = Quest [2];
		mySource.Play ();
		StartCoroutine (secondQuest (Forrest));

	}

	IEnumerator secondQuest(Transform forrest){
		agent.Stop ();
		playingSound = true;
		NavigateTo (forrest);
		agent.Resume ();
		bool teleported = false;
		float time = 0;

	
		while(time <= 3.5f) {
			thisAnimation.SetBool ("Run", true);
			if (time > 2.4f && teleported == false) {
				fade.teleportToTransform (player, forrest.GetChild(0));
				teleported = true;
			}
			time = time + Time.deltaTime;
			yield return 0;
		}
	
		thisAnimation.SetBool ("Run", false);
		StartCoroutine (LookAtPlayer ());
			
		yield return new WaitUntil (() => mySource.isPlaying == false);

	
		secondQuestComplete.Invoke ();
		playingSound = false;

	}

	public void AskHerForName(){
		if (!askName) {
			askName = true;
			agent.Stop ();
			playingSound = true;
			StartCoroutine (WaitForGirlsName ());
		}

	}

	IEnumerator WaitForGirlsName(){
		mySource.clip = Quest [3];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);
		askHerNameComplete.Invoke ();

		while (NameWasCorrect != true) {

			yield return new WaitForSeconds (10f);
			if (NameWasCorrect)
				yield break;
				askHerNameComplete.Invoke ();
			yield return 0;
		}
			
	

	}

	public void NowAskForTime(){
		NameWasCorrect = true;
		StartCoroutine (AskingForTheTime());
	}


	IEnumerator AskingForTheTime(){
		

		mySource.clip = Quest [4];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);
		yield return new WaitForSeconds (1);
		nowAskForTime.Invoke ();


	}


	public void LetsGetIcecream(){

		StartCoroutine (TalkAboutIcecream ());
	}

	IEnumerator TalkAboutIcecream(){

		mySource.clip = Quest [5];
		mySource.Play ();
		yield return new WaitUntil (() => mySource.isPlaying == false);
		agent.Stop ();
		playingSound = true;
		GameObject forrest = GameObject.FindGameObjectWithTag ("IceCream");
		NavigateTo (forrest.transform);
		agent.Resume ();
		bool teleported = false;
		float time = 0;


		while(time <= 3.5f) {
			thisAnimation.SetBool ("Run", true);
			if (time > 2.4f && teleported == false) {
				fade.teleportToTransform (player, forrest.transform.GetChild(0));
				teleported = true;
			}
			time = time + Time.deltaTime;
			yield return 0;
		}

	}

	public void AnswerNameWasCorrect(){
		NameWasCorrect = true;
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
		if (!playingSound) {
			currentState = State.Simple;
			agent.SetDestination (target.position);

		}

	}

	void NavigateTo(Transform target){
		agent.SetDestination (target.position);

	}

	public void OnPointerExit(PointerEventData data){

		isGazing = false;
		ExitGaze ();
	}

}
