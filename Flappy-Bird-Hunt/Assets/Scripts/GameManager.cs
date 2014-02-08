using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static long numKilled;
	public static float timeRemaining = 60;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
	}
}
