using UnityEngine;

namespace Assets.Scripts
{
    public class FlappySpawner : MonoBehaviour {

        public string [] FlappyFlappyPaths = {"FlappyFlappyRed", "FlappyFlappyYellow", "FlappyFlappyBlue"};
	
        public Vector3 FlappyDirection = Vector3.right;

        public float SpawnSpeed = 1.0f;

        public float TimeUntilNextSpawn;

        // Use this for initialization
        void Start () {

        }
	
        // Update is called once per frame
        void Update () {

            SpawnSpeed = (1.0f-GameManager.TimeRemaining/60f);

            if (TimeUntilNextSpawn <= 0.0f) {
                var spawnLocation = collider.bounds.center;

                var flappyFlappyPath = FlappyFlappyPaths[UnityEngine.Random.Range(0, 3)];

                var flappyBirdGameObject = Instantiate(Resources.Load(flappyFlappyPath), spawnLocation, Quaternion.identity) as GameObject;
                var flappyBird = flappyBirdGameObject.GetComponent<FlappyBird>();
                flappyBird.Direction = FlappyDirection;

                if (FlappyDirection.x < 0) {
                    flappyBird.transform.Rotate(0.0f, 180f, 0.0f);
                }

                TimeUntilNextSpawn = SpawnSpeed + UnityEngine.Random.Range(0.0f, 1.0f);
		
            }

            TimeUntilNextSpawn -= Time.deltaTime;

        }
    }
}
