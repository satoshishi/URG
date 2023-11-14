namespace URG.SCIP.Streaming.MD
{
    using System.Collections.Generic;
    using URG.SCIP.Core;

    /// <summary>
    /// センシング情報
    /// </summary>
    public class MD
    {
        private List<long> distance = new List<long>();

        public List<long> Distance => this.distance;

        /// <summary>
        /// 文字列情報からセンシング情報を抽出する
        /// </summary>
        /// <param name="receiveData">送られてきた文字列</param>
        internal void Set(string receiveData)
        {
            this.Clear();
            long timeStamp = 0;

            SCIP_Reader.MD(receiveData, ref timeStamp, ref this.distance);
        }

        internal void Clear()
        {
            this.distance.Clear();
        }
    }
}
