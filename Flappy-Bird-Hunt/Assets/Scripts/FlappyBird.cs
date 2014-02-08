using UnityEngine;
using System;
using System.Collections;

public class FlappyBird : MonoBehaviour {

	public Vector3 direction;
	public float minTimeUntilJump = 1.0f;
	public float maxTimeUntilJump = 2.0f;

	public float currentTimeUntilJump = 0f;
	public Vector2 jumpForce = new Vector2(50.0f, 05.0f);



	// Use this for initialization
	void Start () {
	}



	// Update is called once per frame
	void Update () {
		if (currentTimeUntilJump <= 0f) {
			Vector3 force = new Vector3(direction.x*jumpForce.x, jumpForce.y)*Time.deltaTime;
			Debug.Log ("force: " + force);
			rigidbody.AddForce(force, ForceMode.Impulse);	
			currentTimeUntilJump = UnityEngine.Random.Range(minTimeUntilJump, maxTimeUntilJump);
		}
		currentTimeUntilJump -= Time.deltaTime;
	}



}
