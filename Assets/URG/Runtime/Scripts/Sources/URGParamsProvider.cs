namespace URG.Sources
{
    using UnityEngine;

    public class URGParamsProvider : MonoBehaviour
    {
        [SerializeField]
        private URGParameter sources;

        public URGParameter Parameter => this.sources;
    }
}
