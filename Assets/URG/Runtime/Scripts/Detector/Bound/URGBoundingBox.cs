namespace URG.Detecting.Bound
{
    using UnityEngine;

    [RequireComponent(typeof(MeshRenderer))]
    [ExecuteAlways]
    public class URGBoundingBox : MonoBehaviour
    {
        public static readonly float PLANE_SCALE_FACTOR = 10f / 2f;

        [SerializeField]
        private bool isVisualizing = false;

        private MeshRenderer meshRenderer;

        public void Update()
        {
            if (this.meshRenderer == null)
            {
                this.meshRenderer = this.GetComponent<MeshRenderer>();
            }

            this.meshRenderer.enabled = this.isVisualizing;
        }

        /// <summary>
        /// 指定された座標がboudingbox内に存在するかどうかを返す
        /// </summary>
        /// <param name="to">目標座標</param>
        /// <returns>真偽</returns>
        public bool Contains(Vector3 to)
        {
            Plane left = this.ToLeftPlane();
            Plane right = this.ToRightPlane();
            Plane top = this.ToTopPlane();
            Plane bottom = this.ToBottomPlane();

            bool insideLeft = left.GetDistanceToPoint(to) >= 0;
            bool insideRight = right.GetDistanceToPoint(to) >= 0;
            bool insideTop = top.GetDistanceToPoint(to) >= 0;
            bool insideBottom = bottom.GetDistanceToPoint(to) >= 0;

            return insideLeft && insideRight && insideTop && insideBottom;
        }

        private Plane ToLeftPlane()
        {
            Vector3 point = this.transform.position + (-this.transform.right * (this.transform.localScale.x * PLANE_SCALE_FACTOR));
            Vector3 normal = (this.transform.position - point).normalized;

            Plane plane = new Plane(normal, point);

            // Debug.DrawRay(point, normal * 0.1f, Color.red);
            return plane;
        }

        private Plane ToRightPlane()
        {
            Vector3 point = this.transform.position + (this.transform.right * (this.transform.localScale.x * PLANE_SCALE_FACTOR));
            Vector3 normal = (this.transform.position - point).normalized;

            Plane plane = new Plane(normal, point);

            // Debug.DrawRay(point, normal * 0.1f, Color.blue);
            return plane;
        }

        private Plane ToTopPlane()
        {
            Vector3 point = this.transform.position + (this.transform.forward * (this.transform.localScale.z * PLANE_SCALE_FACTOR));
            Vector3 normal = (this.transform.position - point).normalized;

            Plane plane = new Plane(normal, point);

            // Debug.DrawRay(point, normal * 0.1f, Color.yellow);
            return plane;
        }

        private Plane ToBottomPlane()
        {
            Vector3 point = this.transform.position + (-this.transform.forward * (this.transform.localScale.z * PLANE_SCALE_FACTOR));
            Vector3 normal = (this.transform.position - point).normalized;

            Plane plane = new Plane(normal, point);

            // Debug.DrawRay(point, normal * 0.1f, Color.green);
            return plane;
        }
    }
}
