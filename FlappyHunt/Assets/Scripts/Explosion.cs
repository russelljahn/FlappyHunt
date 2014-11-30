using UnityEngine;

namespace Assets.Scripts
{
    public class Explosion : MonoBehaviour {

        public float TimeUntilDeath = 1.0f;

        public SphereCollider SphereCollider;

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {

            TimeUntilDeath -= Time.deltaTime;
            SphereCollider.radius += Time.deltaTime;

            if (TimeUntilDeath <= 0f) {
                Destroy(gameObject);
            }

        }
    }
}
