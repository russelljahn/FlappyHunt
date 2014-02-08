using UnityEngine;
using System.Collections;

public class ActivateOnTime : MonoBehaviour {

	public float timeToActivate;
	public FlappySpawner spawner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.timeRemaining <= timeToActivate) {
			spawner.enabled = true;
//			spawner.gameObject.SetActive(true);
			Destroy(this);
		}

	}
}
