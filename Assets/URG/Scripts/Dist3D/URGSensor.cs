namespace URG.Dist3D
{
    using System.Collections.Generic;
    using UnityEngine;
    using URG.SCIP.Streaming.MD;

    [RequireComponent(typeof(LongToVector3))]
    public class URGSensor : MonoBehaviour, I3DDistanceStreamer
    {
        [SerializeField]
        private MDStreamer streamer;

        private LongToVector3 translator;

        private List<long> rawDistances = new List<long>();

        private Vector3[] distances = null;

        public Vector3[] Currently()
        {
            if (this.distances == null)
            {
                return new Vector3[] { };
            }

            return this.distances;
        }

        private void Start()
        {
            this.translator = this.GetComponent<LongToVector3>();
        }

        private void Update()
        {
            this.rawDistances.Clear();
            this.rawDistances.AddRange(this.streamer.GetCurrently().Distance);

            this.distances = null;
            this.distances = new Vector3[this.rawDistances.Count];

            for (int i = 0; i <= this.distances.Length; i++)
            {
                this.distances[i] = this.translator.Handle(this.rawDistances[i], i);
            }
        }
    }
}
