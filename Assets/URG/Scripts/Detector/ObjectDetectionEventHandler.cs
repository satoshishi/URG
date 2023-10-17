namespace URG.Detecting
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    [RequireComponent(typeof(IObjectDetector))]
    public class ObjectDetectionEventHandler : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<List<DetectedObject>> onDetected;

        [SerializeField]
        private UnityEvent onLosted;

        private IObjectDetector detector;

        private List<DetectedObject> detecteds = new List<DetectedObject>();

        private bool detection = false;

        public List<DetectedObject> Detecteds
        {
            get => this.detecteds;

            private set
            {
                if ((value == null || value.Count <= 0) && this.detection)
                {
                    this.onLosted?.Invoke();
                    this.detection = false;
                }
                else
                {
                    this.onDetected?.Invoke(this.detecteds);
                    this.detection = true;
                }

                this.detecteds.Clear();
                this.detecteds = value;
            }
        }

        private void Start()
        {
            this.detector = this.GetComponent<IObjectDetector>();
        }

        private void Update()
        {
            this.Detecteds = this.detector.Currently();
        }
    }
}
