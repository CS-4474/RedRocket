using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class ParallaxScroller : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        [Range(0.01f, 2)]
        private float horizontalShift;

        [SerializeField]
        [Range(0.01f, 2)]
        private float verticalShift;
        #endregion

        #region Fields
        private Transform cameraPosition;

        private Vector3 nextPosition;
        #endregion

        #region Initialisation Functions
        private void Start()
        {
            cameraPosition = Camera.main.transform;
        }
        #endregion

        #region Update Functions
        private void Update()
        {
            nextPosition = new Vector3(horizontalShift * cameraPosition.position.x, verticalShift * cameraPosition.position.y, 0);
            transform.position = nextPosition;
        }
        #endregion
    }
}