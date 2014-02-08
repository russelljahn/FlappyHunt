using UnityEngine;
using System.Collections;

public class HandleTouch : MonoBehaviour {


	public string explosionPath = "Exploda";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnPress (bool isDown) {
		Debug.Log ("Press!");
		if (!isDown) {
			GameObject.Instantiate(Resources.Load(explosionPath), UICamera.lastTouchPosition, Quaternion.identity);
		}
	}

	void OnMouseUp() {
		Debug.Log ("Mouse!");
		Vector3 clickInWorldCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		clickInWorldCoord.z = 0.0f;
		GameObject.Instantiate(Resources.Load(explosionPath), clickInWorldCoord, Quaternion.identity);
	}
}
