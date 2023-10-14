namespace URG.Visualizer
{
    using UnityEngine;
    using URG.Dist3D;

    [RequireComponent(typeof(I3DDistanceStreamer))]
    public class URGVisualizer : MonoBehaviour
    {
        [SerializeField]
        private bool isVisualizing = false;

        [SerializeField]
        private Color distanceLineColor;

        private I3DDistanceStreamer distanceStreamer;

        private Material lineMaterial;

        private Vector3[] distances = null;

        public void OnRenderObject()
        {
            if (!this.isVisualizing)
            {
                return;
            }

            this.CreateLineMaterial();

            this.lineMaterial.SetPass(0);
            GL.PushMatrix();

            this.VisualizeDistance();

            GL.PopMatrix();
        }

        private void Start()
        {
            this.distanceStreamer = this.GetComponent<I3DDistanceStreamer>();
        }

        private void CreateLineMaterial()
        {
            if (!this.lineMaterial)
            {
                Shader shader = Shader.Find("Hidden/Internal-Colored");
                this.lineMaterial = new Material(shader)
                {
                    hideFlags = HideFlags.HideAndDontSave,
                };

                this.lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                this.lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                this.lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                this.lineMaterial.SetInt("_ZWrite", 0);
            }
        }

        /// <summary>
        /// URGの距離データを可視化する
        /// </summary>
        private void VisualizeDistance()
        {
            // GL.MultMatrix(this.transform.localToWorldMatrix);
            GL.Begin(GL.LINES);
            GL.Color(this.distanceLineColor);

            this.distances = null;
            this.distances = this.distanceStreamer.Currently();

            foreach (Vector3 dist in this.distances)
            {
                Vector3 from = this.transform.position;
                Vector3 to = dist;

                GL.Vertex3(from.x, from.y, from.z);
                GL.Vertex3(to.x, to.y, to.z);
            }

            GL.End();
        }

        private void OnDestroy()
        {
            Destroy(this.lineMaterial);
        }
    }
}
