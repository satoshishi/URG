namespace URG.Detecting.Standard
{
    using System.Collections.Generic;
    using UnityEngine;
    using URG.Detecting;
    using URG.Detecting.Bound;
    using URG.Dist3D;

    /// <summary>
    /// 標準的な検出ロジッククラス
    /// </summary>
    [RequireComponent(typeof(I3DDistanceStreamer))]
    public partial class StandardObjectDetector : MonoBehaviour, IObjectDetector
    {
        [SerializeField]
        private URGBoundingBox boundingBox;

        private I3DDistanceStreamer distanceStreamer;

        private Vector3[] distances;

        private int currentStep;

        private bool detectedAny;

        private Vector3 startStepDistance;

        private Vector3 distanceCenter;

        private List<DetectedObject> detecteds = new List<DetectedObject>();

        public List<DetectedObject> Currently()
        {
            // return new List<DetectedObject>();
            this.Init();
            this.detecteds.Clear();
            this.detecteds = new List<DetectedObject>();

            this.distances = null;
            this.distances = this.distanceStreamer.Currently();

            for (this.currentStep = 0; this.currentStep < this.distances.Length; this.currentStep++)
            {
                // 検出物となるステップが検出済みかどうか
                if (this.detectedAny)
                {
                    bool breaked = !this.SeekContinuity();

                    // 検出物の切れ目に到達した
                    if (breaked)
                    {
                        this.RegistrationNewObject();
                        this.Init();
                    }
                }

                // まだ検出されていない。始点となるステップを探す
                else
                {
                    this.detectedAny = this.SeekStartDetectionStep();
                }
            }

            return this.detecteds;
        }

        private void Init()
        {
            this.detectedAny = false;
            this.startStepDistance = Vector3.zero;
            this.distanceCenter = Vector3.zero;
        }

        private void Start()
        {
            this.distanceStreamer = this.GetComponent<I3DDistanceStreamer>();
        }

        private Vector3 GetCurrentStep()
        {
            return this.distances[this.currentStep];
        }
    }
}
