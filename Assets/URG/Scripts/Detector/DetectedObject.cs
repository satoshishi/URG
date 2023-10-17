namespace URG.Detecting
{
    using UnityEngine;

    /// <summary>
    /// 検出物の情報
    /// </summary>
    public class DetectedObject
    {
        private Vector3 startStep;

        private Vector3 endStep;

        private Vector3 center;

        public DetectedObject(Vector3 startStep, Vector3 endStep, Vector3 center)
        {
            this.startStep = startStep;
            this.endStep = endStep;
            this.center = center;
        }

        /// <summary>
        /// Gets 検出物の始点となるステップ
        /// </summary>
        public Vector3 StartStep => this.startStep;

        /// <summary>
        /// Gets 検出物の終点となるステップ
        /// </summary>
        public Vector3 EndStep => this.endStep;

        /// <summary>
        /// Gets 検出物の中心
        /// </summary>
        public Vector3 Center => this.center;
    }
}
