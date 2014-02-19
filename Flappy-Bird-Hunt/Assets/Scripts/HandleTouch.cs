using UnityEngine;
using System.Collections;

public class HandleTouch : MonoBehaviour {


	public string explosionPath = "Exploda";


	void OnPress (bool isDown) {

		if (!isDown) {
		
			if (GameManager.mainMenu) {
				GameManager.HandleTitleScreenTouch();
			}
			else if (GameManager.gameOver) {
				GameManager.HandleGameOverScreenTouch();
			}
			else {
				Vector3 clickInWorldCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				clickInWorldCoord.z = 0.0f;
				GameObject.Instantiate(Resources.Load(explosionPath), clickInWorldCoord, Quaternion.identity);
			}

		}
		
	}
}
