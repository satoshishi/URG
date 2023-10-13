namespace URG.SCIP.Streaming
{
    using System.Net.Sockets;
    using System.Text;
    using Cysharp.Threading.Tasks;

    /// <summary>
    /// URG側にTCPクライアントを開始して命令する
    /// </summary>
    public class SCIPWriter
    {
        /// <summary>
        /// URGへの命令を書き込む
        /// </summary>
        /// <param name="client">TCPクライアント</param>
        /// <param name="scip">命令するコマンド</param>
        /// <returns>await</returns>
        public async UniTask Handle(TCPClientProvider client, string scip)
        {
            NetworkStream stream = await client.Stream();
            Write(stream, scip);
        }

        private static void Write(NetworkStream stream, string data)
        {
            if (stream != null && stream.CanWrite)
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
