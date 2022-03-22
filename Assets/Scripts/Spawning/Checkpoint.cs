using Assets.Scripts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawning
{
    public class Checkpoint : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private bool spawnLocked = false;

        [SerializeField]
        private Transform spawnPoint = null;

        [SerializeField]
        private GameObject shipPrefab = null;

        [SerializeField]
        private SpriteRenderer forceField = null;

        [SerializeField]
        private GameObject arrow = null;

        [SerializeField]
        private Checkpoint next = null;
        #endregion

        #region Fields
        private CheckpointManager checkpointManager;

        private AudioSource audioSource;

        private float currentStillTime = 0.0f;
        #endregion

        #region Properties
        public GameObject ShipPrefab => shipPrefab;

        public Checkpoint Next => next;

        private ShipManager player => checkpointManager.PlayerManager.Player;
        #endregion

        #region Initialisation Functions
        private void Awake()
        {
            checkpointManager = GetComponentInParent<CheckpointManager>();
            audioSource = GetComponent<AudioSource>();

            // Make the arrow point to the next checkpoint.
            if (next != null) arrow.transform.right = next.transform.position - arrow.transform.position;
            
        }
        #endregion

        #region Collision Functions
        private void OnTriggerStay2D(Collider2D collider)
        {
            // If this checkpoint is locked, do nothing.
            if (spawnLocked) return;

            // Ensure that it's the player's main part.
            if (collider.gameObject != player.MainPart) return;

            // Ensure that the player is on the platform pointing roughly up and not moving too much.
            float upwardness = Math.Abs(player.MainBody.rotation);
            if (player.MainBody.velocity.sqrMagnitude <= Math.Pow(checkpointManager.RequiredVelocity, 2) && upwardness <= checkpointManager.RequiredUpwardness)
            {
                currentStillTime += Time.fixedDeltaTime;

                Color newColour = forceField.color;
                newColour.a = 0.5f + ((currentStillTime / checkpointManager.RequiredStillTime) / 2);
                forceField.color = newColour;
            }

            if (currentStillTime >= checkpointManager.RequiredStillTime) checkpointManager.SetCheckpoint(this);
        }
        #endregion

        #region Spawn Functions
        public void PositionPlayer(GameObject player)
        {
            player.transform.position = spawnPoint.position;
        }

        public void SetAsCheckpoint()
        {
            // Unlock the next checkpoint.
            if (next != null) next.Unlock();

            // Lock this checkpoint.
            Lock();

            // Make the arrow visible.
            arrow.SetActive(true);

            forceField.color = new Color(0, 0, 1, 0.5f);
        }

        public void Lock()
        {
            // Make the arrow invisible.
            arrow.SetActive(false);

            spawnLocked = true;
            forceField.color = new Color(1, 0, 0, 0.5f);
        }

        public void Unlock()
        {
            spawnLocked = false;
            audioSource.Play();
            forceField.color = new Color(0, 1, 0, 0.5f);
        }
        #endregion
    }
}