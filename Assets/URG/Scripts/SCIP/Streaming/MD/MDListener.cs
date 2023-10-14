namespace URG.SCIP.Streaming.MD
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine;
    using URG.SCIP.Core;
    using URG.SCIP.Streaming;

    /// <summary>
    /// MDモード中のデータを読み込む
    /// </summary>
    public class MDListener : MonoBehaviour, ISCIPListener, ISCIPDataStreamer<MD>
    {
        public static readonly MODE TARGET_MODE = MODE.MD;

        private Thread clientThread;

        private MD md = new MD();

        /// <summary>
        /// clientの読み込み開始
        /// </summary>
        /// <param name="client">tcpクライアント</param>
        /// <returns>await</returns>
        public async UniTask Listen(TCPClientProvider client)
        {
            NetworkStream stream = await client.Stream();

            Debug.Log($"Start MD Streaming");

            this.clientThread = new Thread(new ParameterizedThreadStart(this.Handle));
            this.clientThread.Start(stream);
        }

        public MD Currently()
        {
            return this.md;
        }

        /// <summary>
        /// Read to "\n\n" from NetworkStream
        /// </summary>
        /// <returns>receive data</returns>
        private static string ReadLine(NetworkStream stream)
        {
            if (!stream.CanRead)
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();
            bool is_NL2 = false;
            bool is_NL = false;
            do
            {
                char buf = (char)stream.ReadByte();
                if (buf == '\n')
                {
                    if (is_NL)
                    {
                        is_NL2 = true;
                    }
                    else
                    {
                        is_NL = true;
                    }
                }
                else
                {
                    is_NL = false;
                }

                sb.Append(buf);
            }
            while (!is_NL2);

            return sb.ToString();
        }

        private void OnDestroy()
        {
            this.clientThread?.Abort();
        }

        /// <summary>
        /// tcpクライアントからデータを読み込んでセンシング情報を取得する
        /// </summary>
        /// <param name="source">NetworkStream</param>
        private void Handle(object source)
        {
            try
            {
                using NetworkStream stream = (NetworkStream)source;

                while (true)
                {
                    string receive_data = ReadLine(stream);
                    string cmd = this.GetCommand(receive_data);
                    if (cmd == TARGET_MODE.ToCMD())
                    {
                        this.md.Set(receive_data);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  streamから取得した文字列からコマンドを抽出する
        /// </summary>
        /// <param name="get_command">文字列</param>
        /// <returns>抽出結果</returns>
        private string GetCommand(string get_command)
        {
            string[] split_command = get_command.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return split_command[0][..2];
        }
    }
}
