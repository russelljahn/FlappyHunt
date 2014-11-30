using UnityEngine;

namespace Assets.Scripts
{
    public class KillAfter : MonoBehaviour {

        public float TimeUntilDeath = 5.0f;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
            TimeUntilDeath -= Time.deltaTime;

            if (TimeUntilDeath <= 0.0f) {
                Destroy(gameObject);
            }
        }
    }
}
