using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class FreeMove : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        [Range(0.01f, 100)]
        private float moveSpeed;
        #endregion

        #region Update Functions
        private void LateUpdate()
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        #endregion

    }
}