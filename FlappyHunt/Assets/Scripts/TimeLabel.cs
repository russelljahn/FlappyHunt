using UnityEngine;

namespace Assets.Scripts
{
    public class TimeLabel : MonoBehaviour {

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
            GetComponent<UILabel>().text = Mathf.FloorToInt(Mathf.Max(0.0f, GameManager.TimeRemaining)).ToString();
        }
    }
}
