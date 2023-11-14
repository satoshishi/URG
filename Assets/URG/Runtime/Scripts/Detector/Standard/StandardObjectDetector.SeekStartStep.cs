namespace URG.Detecting.Standard
{
    using UnityEngine;

    /// <summary>
    /// 検出物の最初のステップを探す処理群
    /// </summary>
    public partial class StandardObjectDetector
    {
        private bool SeekStartDetectionStep()
        {
            Vector3 dist = this.GetCurrentStep();

            if (this.boundingBox.Contains(dist))
            {
                this.startStepDistance = dist;
                return true;
            }

            return false;
        }
    }
}
