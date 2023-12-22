using UnityEngine;
using System.Collections;

public class CameraDemo : MonoBehaviour {

	public Transform target;

	public float speed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {

			speed = 50f;

		} else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			
			speed = -50f;

		} else {

			speed = 0;
		}

		transform.LookAt (target.position);

		transform.RotateAround (target.position, Vector3.up, speed * Time.deltaTime);
	
	}
}
