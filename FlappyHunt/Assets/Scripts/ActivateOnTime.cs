using UnityEngine;


namespace Assets.Scripts
{
    public class ActivateOnTime : MonoBehaviour
    {
        public FlappySpawner Spawner;
        public float TimeToActivate;

        // Use this for initialization
        private void Start() { }

        // Update is called once per frame
        private void Update()
        {
            if (GameManager.TimeRemaining <= TimeToActivate)
            {
                Spawner.enabled = true;
                Destroy(this);
            }
        }
    }
}
