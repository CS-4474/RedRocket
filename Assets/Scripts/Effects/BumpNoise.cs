using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpNoise : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    [Range(1, 100)]
    private float minimumForce = 0;

    [SerializeField]
    private AudioSource audioSource;
    #endregion

    #region Fields
    #endregion

    #region Initialisation Functions
    private void Start()
    {

    }
    #endregion

    #region Collision Functions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.sqrMagnitude >= Math.Pow(minimumForce, 2)) audioSource.Play();
    }
    #endregion
}
