namespace URG.Dist3D
{
    using UnityEngine;

    /// <summary>
    /// URGからの距離情報をもとにローカル3D座標に変換した値をストリーミングする
    /// </summary>
    public interface I3DDistanceStreamer
    {
        public Vector3[] Currently();
    }
}
