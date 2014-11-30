using UnityEngine;

namespace Assets.Scripts
{
    public class HandleTouch : MonoBehaviour {


        public string ExplosionPath = "Exploda";


        void OnPress (bool isDown) {

            if (!isDown) {
		
                if (GameManager.MainMenu) {
                    GameManager.HandleTitleScreenTouch();
                }
                else if (GameManager.GameOver) {
                    GameManager.HandleGameOverScreenTouch();
                }
                else {
                    var clickInWorldCoord = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    clickInWorldCoord.z = 0.0f;
                    Instantiate(Resources.Load(ExplosionPath), clickInWorldCoord, Quaternion.identity);
                }

            }
		
        }
    }
}
