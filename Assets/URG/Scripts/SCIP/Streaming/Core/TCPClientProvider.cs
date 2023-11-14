namespace URG.SCIP.Streaming
{
    using System;
    using System.Net.Sockets;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using URG.Sources;

    /// <summary>
    /// TCP通信をするためのクライアントを提供する
    /// </summary>
    public class TCPClientProvider : MonoBehaviour
    {
        [SerializeField]
        private URGParamsProvider paramsProvider;

        private TcpClient client;

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
            this.Connection(this.paramsProvider.Parameter.IP, this.paramsProvider.Parameter.Port);
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

                Debug.Log($"Connected TcpClient IP -> {ip} PORT -> {port}");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
