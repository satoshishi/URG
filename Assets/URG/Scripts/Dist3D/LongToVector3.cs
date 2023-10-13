namespace URG.Dist3D
{
    using UnityEngine;

    /// <summary>
    /// URGからの距離情報をローカル座標系に変換する
    /// </summary>
    public class LongToVector3 : MonoBehaviour
    {
        public static readonly float ANGLE_DELTA = 135f * 2f / 1080f;

        public static readonly float ANGLE_OFFSET = 135f;

        public Vector3 Handle(long distance, int index)
        {
            Vector3 origin = this.transform.position;
            Vector3 right = this.transform.right;
            Vector3 forward = this.transform.forward;

            float angle = (index * ANGLE_DELTA) - ANGLE_OFFSET + 90f;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);
            float d = distance * 0.001f; // mm -> m

            Vector3 pos = (((x * right) + (y * forward)) * d) + origin;

            return pos;
        }
    }
}
