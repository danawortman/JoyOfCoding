using UnityEngine;
using System.Collections;

public class AnimationSelectionGUI : MonoBehaviour {

	private Animator animBlackScorpion;
	private Animator animScorpion;

	private CameraDemo mainCamera;

	private GameObject scorpion;
	private GameObject blackscorpion;

	// Use this for initialization
	void Start () {

		scorpion = GameObject.FindObjectOfType<Scorpion> ().gameObject;
		blackscorpion = GameObject.FindObjectOfType<BlackScorpion> ().gameObject;

		mainCamera = GameObject.FindObjectOfType<CameraDemo> ().GetComponent<CameraDemo> ();

		if (GameObject.FindObjectOfType<Scorpion> ()) {

			animScorpion = GameObject.FindObjectOfType<Scorpion> ().GetComponent<Animator> ();
			mainCamera.target = GameObject.FindObjectOfType<Scorpion> ().GetComponent<Transform>();


		}

		if (GameObject.FindObjectOfType<BlackScorpion> ()) {

			animBlackScorpion = GameObject.FindObjectOfType<BlackScorpion> ().GetComponent<Animator> ();
			blackscorpion.SetActive (false);

		}


	
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.FindObjectOfType<Scorpion> () && !animScorpion) {

			animScorpion = GameObject.FindObjectOfType<Scorpion> ().GetComponent<Animator> ();

		}

		if (GameObject.FindObjectOfType<BlackScorpion> () && !animBlackScorpion) {

			animBlackScorpion = GameObject.FindObjectOfType<BlackScorpion> ().GetComponent<Animator> ();

		}
	
	}

	public void Attack(){

		if (scorpion.activeSelf) {

			animScorpion.SetTrigger ("AttackTrigger");
			animScorpion.SetBool ("WalkingBool", false);

		}

		if (blackscorpion.activeSelf) {

			animBlackScorpion.SetTrigger ("AttackTrigger");
			animBlackScorpion.SetBool ("WalkingBool", false);

		}
	}

	public void Attack2(){

		if (scorpion.activeSelf) {

			animScorpion.SetTrigger ("Attack2Trigger");
			animScorpion.SetBool ("WalkingBool", false);

		}

		if (blackscorpion.activeSelf) {

			animBlackScorpion.SetTrigger ("Attack2Trigger");
			animBlackScorpion.SetBool ("WalkingBool", false);

		}
	}

	public void Attack3(){

		if (scorpion.activeSelf) {

			animScorpion.SetTrigger ("Attack3Trigger");
			animScorpion.SetBool ("WalkingBool", false);

		}

		if (blackscorpion.activeSelf) {

			animBlackScorpion.SetTrigger ("Attack3Trigger");
			animBlackScorpion.SetBool ("WalkingBool", false);

		}
	}
		
	public void Dead(){

		if (scorpion.activeSelf) {

			animScorpion.SetTrigger ("DeadTrigger");
			animScorpion.SetBool ("WalkingBool", false);

		}

		if (blackscorpion.activeSelf) {

			animBlackScorpion.SetTrigger ("DeadTrigger");
			animBlackScorpion.SetBool ("WalkingBool", false);

		}

	}

	public void Hit(){	
	
		if (scorpion.activeSelf) {

			animScorpion.SetTrigger ("HitTrigger");
			animScorpion.SetBool ("WalkingBool", false);

		}

		if (blackscorpion.activeSelf) {

			animBlackScorpion.SetTrigger ("HitTrigger");
			animBlackScorpion.SetBool ("WalkingBool", false);

		}

	}

	public void Walking(){

		if (scorpion.activeSelf) {

			animScorpion.SetBool ("WalkingBool", true);

		}

		if (blackscorpion.activeSelf) {

			animBlackScorpion.SetBool ("WalkingBool", true);

		}

	}

	public void Scorpion(){

		scorpion.gameObject.SetActive (true);
		blackscorpion.gameObject.SetActive (false);

		mainCamera.target = GameObject.FindObjectOfType<Scorpion> ().GetComponent<Transform>();
	}

	public void BlackScorpion(){

		scorpion.gameObject.SetActive (false);
		blackscorpion.gameObject.SetActive (true);

		mainCamera.target = GameObject.FindObjectOfType<BlackScorpion> ().GetComponent<Transform>();
	}

}
