using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float timeUntilDeath = 1.0f;

	public SphereCollider sphereCollider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		timeUntilDeath -= Time.deltaTime;
		sphereCollider.radius += Time.deltaTime;

		if (timeUntilDeath <= 0f) {
			GameObject.Destroy(this.gameObject);
		}

	}
}
