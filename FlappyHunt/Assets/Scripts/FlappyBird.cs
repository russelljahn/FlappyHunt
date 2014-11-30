using UnityEngine;
using System;
using System.Collections;

public class FlappyBird : MonoBehaviour {

	public Vector3 direction;
	public float minTimeUntilJump = 1.0f;
	public float maxTimeUntilJump = 2.0f;

	public float currentTimeUntilJump = 0f;
	public Vector2 jumpForce = new Vector2(50.0f, 05.0f);

	public Vector3 upRotation = new Vector3(0.0f, 0.0f, 45.0f);
	public Vector3 downRotation = new Vector3(0.0f, 0.0f, -90.0f);

	public float upRotationTime;
	public float upRotationMaxTime = 1.0f;

	public GameObject deathParticles;

	public float diveBombSpeed = 3.0f;
	public float timeUntilGuillotine = 8.0f;

	public AudioClip [] clips;


	// Use this for initialization
	void Start () {
		if (direction.x < 0) {
			upRotation.y = 180f;
			downRotation.y = 180f;
		}
		upRotationTime = upRotationMaxTime;
	}



	// Update is called once per frame
	void Update () {

		timeUntilGuillotine -= Time.deltaTime;

		if (timeUntilGuillotine <= 0.0f) {
			GameObject.Destroy(this.gameObject);
		}


		if (currentTimeUntilJump <= 0f) {
			Vector3 force = new Vector3(direction.x*jumpForce.x, jumpForce.y)*Time.deltaTime;
			rigidbody.AddForce(force, ForceMode.Impulse);	
			currentTimeUntilJump = UnityEngine.Random.Range(minTimeUntilJump, maxTimeUntilJump);
			this.transform.rotation = Quaternion.Euler(upRotation);
			upRotationTime = upRotationMaxTime;
		}

		if (upRotationTime > 0) {
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(upRotation), Time.deltaTime);
			upRotationTime -= Time.deltaTime;
		}
		else {
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(downRotation), diveBombSpeed*Time.deltaTime);
		}

		currentTimeUntilJump -= Time.deltaTime;
	}


	void OnCollisionEnter(Collision collision) {
//		Debug.Log (collision.gameObject.name + " collided with " + this.gameObject.name);

		if (collision.gameObject.CompareTag("Explosion")) {
			deathParticles.gameObject.SetActive(true);
			deathParticles.transform.parent = null;

			deathParticles.AddComponent<AudioSource>();

			int soundToPlayIndex = UnityEngine.Random.Range(0, clips.Length);
			deathParticles.GetComponent<AudioSource>().PlayOneShot(clips[soundToPlayIndex]);


			GameObject.Destroy(this.gameObject);
			++GameManager.numKilled;
		}

	}

}
