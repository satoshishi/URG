namespace URG.Detecting.Standard
{
    using UnityEngine;
    using URG.Detecting;

    /// <summary>
    /// 検出物の最初のステップを探す処理群
    /// </summary>
    public partial class StandardObjectDetector
    {
        [Header("検出物の最小サイズ")]
        [SerializeField]
        private float minDetectedObjectSize;

        [Header("検出物の最大サイズ")]
        [SerializeField]
        private float maxDetectedObjectSize;

        private void RegistrationNewObject()
        {
            bool inside = this.currentStep - 1 >= 0;

            // 一つ前の距離データが存在しないケースは無効
            if (!inside)
            {
                return;
            }

            Vector3 previous = this.distances[this.currentStep - 1];

            if (this.boundingBox.Contains(this.distanceCenter) && this.IsValidDetectedObjectSize(this.startStepDistance, previous))
            {
                this.detecteds.Add(new DetectedObject(
                    this.startStepDistance,
                    previous,
                    this.distanceCenter));
            }
        }

        private bool IsValidDetectedObjectSize(Vector3 start, Vector3 end)
        {
            bool greaterThan = this.minDetectedObjectSize * this.minDetectedObjectSize < (start - end).sqrMagnitude;
            bool lessThan = this.maxDetectedObjectSize * this.maxDetectedObjectSize > (start - end).sqrMagnitude;

            return greaterThan && lessThan;
        }
    }
}
