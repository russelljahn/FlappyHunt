using UnityEngine;
using System.Collections;

public class HoverCosine : MonoBehaviour {

	public float waveStart = 0.0f;
	public float waveSpeed = 1.0f;
	public float waveHeight = 1.0f;

	private Vector3 originalPosition;
	
	// Use this for initialization
	void Start () {
		originalPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(0.0f, waveHeight*Mathf.Cos(Mathf.PI*waveStart + waveSpeed*Time.time*Mathf.PI)) + originalPosition;
	}
}