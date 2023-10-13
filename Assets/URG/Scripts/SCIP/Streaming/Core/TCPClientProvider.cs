namespace URG.SCIP.Streaming
{
    using System;
    using System.Net.Sockets;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    /// <summary>
    /// TCP通信をするためのクライアントを提供する
    /// </summary>
    [RequireComponent(typeof(SCIPStreamingParameter))]
    public class TCPClientProvider : MonoBehaviour
    {
        private TcpClient client;

        private SCIPStreamingParameter parameter;

        /// <summary>
        /// センサーとの接続が確立するまでawaitしてStreamを返す
        /// </summary>
        /// <returns>NetworkStream</returns>
        public async UniTask<NetworkStream> Stream()
        {
            await UniTask.WaitUntil(() => this.client.Connected);

            return this.client.GetStream();
        }

        private void Awake()
        {
            this.parameter = this.GetComponent<SCIPStreamingParameter>();
            this.Connection(this.parameter.IP, this.parameter.Port);
        }

        private void OnDestroy()
        {
            if (this.client != null)
            {
                if (this.client.Connected)
                {
                    NetworkStream stream = this.client.GetStream();
                    stream?.Close();
                }

                this.client.Close();
            }
        }

        /// <summary>
        /// 接続してクライアントを生成する
        /// </summary>
        /// <param name="ip">ipアドレス</param>
        /// <param name="port">ポート番号</param>
        private void Connection(string ip, int port)
        {
            try
            {
                this.client = new TcpClient();
                this.client.Connect(ip, port);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
