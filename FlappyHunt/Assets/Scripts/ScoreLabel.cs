using UnityEngine;

namespace Assets.Scripts
{
    public class ScoreLabel : MonoBehaviour {

        // Use this for initialization
        void Start () {
            GetComponent<UILabel>().text = GameManager.NumKilled.ToString();
		
        }
	
        // Update is called once per frame
        void Update () {
            GetComponent<UILabel>().text = GameManager.NumKilled.ToString();
        }
    }
}
