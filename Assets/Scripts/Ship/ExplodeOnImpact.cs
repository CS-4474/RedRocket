using System;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ExplodeOnImpact : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        [Range(0, 1000)]
        private float requiredForce = 100;

        [SerializeField]
        private GameObject explosionPrefab;
        #endregion

        #region Fields
        private Collider2D collider;
        #endregion

        #region Initialisation Functions
        private void Start()
        {
            collider = GetComponent<Collider2D>();

            if (collider is null)
            {
                Debug.Log("Collider was missing from object that was meant to explode, disabling explosion script.");
                enabled = false;
            }
        }
        #endregion

        #region Collision Functions
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.sqrMagnitude >= Math.Pow(requiredForce, 2)) Explode();
        }
        #endregion

        #region Explosion Functions
        public void Explode()
        {
            Instantiate(explosionPrefab);
            Destroy(gameObject);
        }
        #endregion
    }
}