using UnityEngine;
using System.Collections;

public class KillAfter : MonoBehaviour {

	public float timeUntilDeath = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeUntilDeath -= Time.deltaTime;

		if (timeUntilDeath <= 0.0f) {
			GameObject.Destroy(this.gameObject);
		}
	}
}
