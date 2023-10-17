namespace URG.Detecting.Standard
{
    using UnityEngine;

    /// <summary>
    /// 検出物の連続的な部分を探す処理群
    /// </summary>
    public partial class StandardObjectDetector
    {
        private bool SeekContinuity()
        {
            Vector3 dist = this.GetCurrentStep();

            if (this.boundingBox.Contains(dist))
            {
                this.distanceCenter = this.UpdateCenterDistance(dist);
                return true;
            }

            return false;
        }

        private Vector3 UpdateCenterDistance(Vector3 dist)
        {
            // 一度も中央の座標候補が設定されていない場合は、第一候補に
            if (this.distanceCenter == Vector3.zero)
            {
                return dist;
            }

            float challenger = (dist - this.transform.position).sqrMagnitude;
            float candidate = (this.distanceCenter - this.transform.position).sqrMagnitude;

            return challenger <= candidate ? dist : this.distanceCenter;
        }
    }
}
