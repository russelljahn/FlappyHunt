using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour {

        public static long NumKilled;
        public static float TimeRemaining;
        public static float MaxTimeRemaining = 60;
	

        public static GameObject TitleOverlay;
        public static GameObject GameOverOverlay;

        public static bool GameOver = false;
        public static bool MainMenu = true;
	

        // Use this for initialization
        void Start () {
            TimeRemaining = MaxTimeRemaining;

            TitleOverlay = GameObject.FindWithTag("Title");
            GameOverOverlay = GameObject.FindWithTag("GameOver");
            GameOverOverlay.SetActive(false);
		
            Time.timeScale = 0.0f;
        }

        // Update is called once per frame
        void Update () {
            TimeRemaining -= Time.deltaTime;

            if (TimeRemaining <= 0) {
//			Time.timeScale = 0.0f;
                GameOverOverlay.gameObject.SetActive(true);
                GameOver = true;
            }
        }



        public static void HandleTitleScreenTouch() {
            TitleOverlay.SetActive(false);
            Time.timeScale = 1.0f;
            MainMenu = false;
            GameOver = false;
        }



        public static void HandleGameOverScreenTouch() {
            NumKilled = 0;
            TimeRemaining = MaxTimeRemaining;
            GameOver = false;
            MainMenu = true;
		
            GameOverOverlay.SetActive(false);
            TitleOverlay.SetActive(true);
		
		
            Application.LoadLevel(Application.loadedLevel);
        }



    }
}
