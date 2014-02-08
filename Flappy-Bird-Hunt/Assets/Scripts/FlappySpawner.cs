using UnityEngine;
using System.Collections;

public class FlappySpawner : MonoBehaviour {

	public string flappyFlappyPath = "FlappyFlappy";
	public Vector3 flappyDirection = Vector3.right;

	public float spawnSpeed = 1.0f;

	public float timeUntilNextSpawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (timeUntilNextSpawn <= 0.0f) {
			Vector3 spawnLocation = collider.bounds.center;

			GameObject flappyBirdGameObject = GameObject.Instantiate(Resources.Load(flappyFlappyPath), spawnLocation, Quaternion.identity) as GameObject;
			FlappyBird flappyBird = flappyBirdGameObject.GetComponent<FlappyBird>();
			flappyBird.direction = flappyDirection;

			if (flappyDirection.x < 0) {
				flappyBird.transform.Rotate(0.0f, 180f, 0.0f);
			}

			timeUntilNextSpawn = spawnSpeed + UnityEngine.Random.Range(0.0f, 1.0f);
		
		}

		timeUntilNextSpawn -= Time.deltaTime;

	}
}
