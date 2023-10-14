namespace URG.SCIP.Streaming
{
    using UnityEngine;

    /// <summary>
    /// SCIPを介してURPと通信する際のパラメーター情報
    /// https://img.atwiki.jp/kanazawa2robocar/attach/20/2/URG-Series_SCIP2_Compatible_Communication_Specification_JPN.pdf
    /// </summary>
    public class SCIPStreamingParameter : MonoBehaviour
    {
        [Header("IPアドレス")]
        [SerializeField]
        private string ipAddress;

        [Header("ポート番号")]
        [SerializeField]
        private int port;

        [Header("取得する最初のステップ")]
        [SerializeField]
        private int measurementStartStep;

        [Header("取得する最後のステップ")]
        [SerializeField]
        private int measurementEndStep;

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
        public int MeasurementStartStep => this.measurementStartStep;

        /// <summary>
        /// Gets 取得する最後のステップ
        /// </summary>
        public int MeasurementEndStep => this.measurementEndStep;
    }
}
