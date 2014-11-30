using UnityEngine;

namespace Assets.Scripts
{
    public class FlappyBird : MonoBehaviour {

        public Vector3 Direction;
        public float MinTimeUntilJump = 1.0f;
        public float MaxTimeUntilJump = 2.0f;

        public float CurrentTimeUntilJump = 0f;
        public Vector2 JumpForce = new Vector2(50.0f, 05.0f);

        public Vector3 UpRotation = new Vector3(0.0f, 0.0f, 45.0f);
        public Vector3 DownRotation = new Vector3(0.0f, 0.0f, -90.0f);

        public float UpRotationTime;
        public float UpRotationMaxTime = 1.0f;

        public GameObject DeathParticles;

        public float DiveBombSpeed = 3.0f;
        public float TimeUntilGuillotine = 8.0f;

        public AudioClip [] Clips;


        // Use this for initialization
        void Start () {
            if (Direction.x < 0) {
                UpRotation.y = 180f;
                DownRotation.y = 180f;
            }
            UpRotationTime = UpRotationMaxTime;
        }



        // Update is called once per frame
        void Update () {

            TimeUntilGuillotine -= Time.deltaTime;

            if (TimeUntilGuillotine <= 0.0f) {
                Destroy(gameObject);
            }


            if (CurrentTimeUntilJump <= 0f) {
                var force = new Vector3(Direction.x*JumpForce.x, JumpForce.y)*Time.deltaTime;
                rigidbody.AddForce(force, ForceMode.Impulse);	
                CurrentTimeUntilJump = Random.Range(MinTimeUntilJump, MaxTimeUntilJump);
                transform.rotation = Quaternion.Euler(UpRotation);
                UpRotationTime = UpRotationMaxTime;
            }

            if (UpRotationTime > 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(UpRotation), Time.deltaTime);
                UpRotationTime -= Time.deltaTime;
            }
            else {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(DownRotation), DiveBombSpeed*Time.deltaTime);
            }

            CurrentTimeUntilJump -= Time.deltaTime;
        }


        void OnCollisionEnter(Collision collision) {
//		Debug.Log (collision.gameObject.name + " collided with " + this.gameObject.name);

            if (collision.gameObject.CompareTag("Explosion")) {
                DeathParticles.gameObject.SetActive(true);
                DeathParticles.transform.parent = null;

                DeathParticles.AddComponent<AudioSource>();

                int soundToPlayIndex = Random.Range(0, Clips.Length);
                DeathParticles.GetComponent<AudioSource>().PlayOneShot(Clips[soundToPlayIndex]);


                Destroy(gameObject);
                ++GameManager.NumKilled;
            }

        }

    }
}
