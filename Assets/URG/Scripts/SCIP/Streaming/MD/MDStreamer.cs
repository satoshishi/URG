namespace URG.SCIP.Streaming.MD
{
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using URG.SCIP.Core;
    using URG.SCIP.Streaming;

    /// <summary>
    /// MDモードの開始やデータの提供をする
    /// </summary>
    [RequireComponent(typeof(ISCIPListener))]
    [RequireComponent(typeof(ISCIPDataStreamer<MD>))]
    public class MDStreamer : MonoBehaviour
    {
        [SerializeField]
        private SCIPStreamingParameter parameter;

        [SerializeField]
        private TCPClientProvider client;

        private SCIPWriter writer;

        private ISCIPListener listener;

        private ISCIPDataStreamer<MD> data;

        public async UniTask StartOperationAsync()
        {
            string command = SCIP_Writer.MD(this.parameter.MeasurementStartStep, this.parameter.MeasurementEndStep, 1, 0, 0);

            // MDモードをURGにリクエスト
            await this.writer.Handle(this.client, command);
            await this.listener.Listen(this.client);
        }

        public MD GetCurrently()
        {
            return this.data.Currently();
        }

        private void Start()
        {
            this.writer = new SCIPWriter();
            this.listener = this.GetComponent<ISCIPListener>();
            this.data = this.GetComponent<ISCIPDataStreamer<MD>>();
            this.StartOperationAsync().Forget();
        }
    }
}
