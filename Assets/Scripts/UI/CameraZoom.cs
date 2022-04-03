using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CameraZoom : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        [Range(1, 10)]
        private float minimumSize = 0;

        [SerializeField]
        [Range(1, 30)]
        private float maximumSize = 0;

        [SerializeField]
        [Range(1, 10)]
        private float sensitivity = 0;

        [SerializeField]
        private RectTransform timelineTransform;
        #endregion

        #region Fields
        private Camera camera = null;
        #endregion

        #region Initialisation Functions
        private void Start()
        {
            camera = GetComponent<Camera>();
        }
        #endregion

        #region Update Functions
        private void Update()
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(timelineTransform,Input.mousePosition) && Input.GetAxis("Mouse ScrollWheel") != 0)
                camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - sensitivity * Input.GetAxis("Mouse ScrollWheel"), minimumSize, maximumSize);
        }
        #endregion
    }
}