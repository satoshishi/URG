namespace URG.Dist3D
{
    using UnityEngine;
    using URG.Sources;

    /// <summary>
    /// URGからの距離情報をローカル座標系に変換する
    /// </summary>
    public class LongToVector3 : MonoBehaviour
    {
        public static readonly float ANGLE_DELTA = 135f * 2f / 1080f;

        public static readonly float ANGLE_OFFSET = 135f;

        [SerializeField]
        private URGParamsProvider paramsProvider;

        public Vector3 Handle(long distance, int index)
        {
            Vector3 origin = this.transform.position;
            Quaternion offset = Quaternion.Euler(0f, this.paramsProvider.Parameter.OffsetAngle * -1f, 0f);
            Vector3 right = offset * this.transform.right;
            Vector3 forward = offset * this.transform.forward;

            float angle = (index * ANGLE_DELTA) - ANGLE_OFFSET + 90f;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);
            float d = distance * 0.001f; // mm -> m

            Vector3 pos = (((x * right) + (y * forward)) * d) + origin;

            return pos;
        }
    }
}
