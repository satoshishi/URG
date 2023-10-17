namespace URG.Visualizing
{
    using System.Collections.Generic;
    using UnityEngine;
    using URG.Detecting;
    using URG.Dist3D;

    [RequireComponent(typeof(I3DDistanceStreamer))]
    [RequireComponent(typeof(ObjectDetectionEventHandler))]
    public class URGVisualizer : MonoBehaviour
    {
        [SerializeField]
        private bool isVisualizing = false;

        [SerializeField]
        private Color distanceLineColor;

        [SerializeField]
        private Color detectedLineColor;

        [SerializeField]
        private Color detectedCenterLineColor;

        private I3DDistanceStreamer distanceStreamer;

        private ObjectDetectionEventHandler objectDetection;

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

            this.VisualizeDetecteds();

            GL.PopMatrix();
        }

        private void Start()
        {
            this.distanceStreamer = this.GetComponent<I3DDistanceStreamer>();
            this.objectDetection = this.GetComponent<ObjectDetectionEventHandler>();
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

        private void VisualizeDetecteds()
        {
            List<DetectedObject> detecteds = this.objectDetection.Detecteds;

            if (detecteds == null || detecteds.Count <= 0)
            {
                return;
            }

            foreach (DetectedObject detected in detecteds)
            {
                GL.Begin(GL.LINES);
                GL.Color(this.detectedLineColor);

                Vector3 from = this.transform.position;
                Vector3 p0 = detected.StartStep;
                Vector3 p1 = detected.EndStep;

                GL.Vertex3(from.x, from.y, from.z);
                GL.Vertex3(p0.x, p0.y, p0.z);

                GL.Vertex3(from.x, from.y, from.z);
                GL.Vertex3(p1.x, p1.y, p1.z);

                GL.End();

                GL.Begin(GL.LINES);
                GL.Color(this.detectedCenterLineColor);

                Vector3 center = detected.Center;

                GL.Vertex3(from.x, from.y, from.z);
                GL.Vertex3(center.x, center.y, center.z);
            }

            GL.End();
        }

        private void OnDestroy()
        {
            Destroy(this.lineMaterial);
        }
    }
}
