using UnityEngine;

namespace Assets.Scripts
{
    public class HoverCosine : MonoBehaviour {

        public float WaveStart = 0.0f;
        public float WaveSpeed = 1.0f;
        public float WaveHeight = 1.0f;

        private Vector3 _originalPosition;
	
        // Use this for initialization
        void Start () {
            _originalPosition = transform.position;
        }
	
        // Update is called once per frame
        void Update () {
            transform.position = new Vector3(0.0f, WaveHeight*Mathf.Cos(Mathf.PI*WaveStart + WaveSpeed*Time.time*Mathf.PI)) + _originalPosition;
        }
    }
}
