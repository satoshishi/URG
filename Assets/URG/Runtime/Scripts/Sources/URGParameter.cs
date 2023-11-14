namespace URG.Sources
{
    using UnityEngine;

    /// <summary>
    /// URGによるセンシングのためのパラメーター群
    /// </summary>
    [CreateAssetMenu(menuName = "URG/Parameter", fileName = "New Parameter")]
    public class URGParameter : ScriptableObject
    {
        public static readonly float UST_10_MAX_ANGLE = 270f;

        [Header("IPアドレス")]
        [SerializeField]
        private string ipAddress;

        [Header("ポート番号")]
        [SerializeField]
        private int port;

        [Header("角度")]
        [SerializeField]
        private float angle = 270f;

        /// <summary>
        /// Gets ipアドレス
        /// </summary>
        public string IP => this.ipAddress;

        /// <summary>
        /// Gets ポート番号
        /// </summary>
        public int Port => this.port;

        /// <summary>
        /// Gets 取得する最初のステップ
        /// </summary>
        public int MeasurementStartStep => Mathf.FloorToInt(this.OffsetAngle * 4f);

        /// <summary>
        /// Gets 取得する最後のステップ
        /// </summary>
        public int MeasurementEndStep => Mathf.FloorToInt((UST_10_MAX_ANGLE - this.OffsetAngle) * 4f);

        /// <summary>
        /// Gets 角度のオフセット
        /// </summary>
        public float OffsetAngle => (UST_10_MAX_ANGLE - this.angle) * 0.5f;
    }
}
