using Assets.Scripts.Ship;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Spawning
{
    public class CheckpointManager : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private Checkpoint spawnPoint = null;

        [SerializeField]
        private PlayerManager playerManager = null;

        [SerializeField]
        [Range(1, 10)]
        private float requiredStillTime = 1.0f;

        [SerializeField]
        [Range(0, 90)]
        private float requiredUpwardness = 45;

        [SerializeField]
        [Range(0, 10)]
        private float requiredVelocity = 1.0f;
        #endregion

        #region Properties
        public Checkpoint CurrentCheckpoint { get; set; }

        public PlayerManager PlayerManager => playerManager;

        public float RequiredStillTime => requiredStillTime;

        public float RequiredUpwardness => requiredUpwardness;

        public float RequiredVelocity => requiredVelocity;
        #endregion

        #region Events
        public UnityEvent OnNewCheckpoint;
        #endregion

        #region Initialisation Functions
        private void Start()
        {
            SetCheckpoint(spawnPoint);
        }
        #endregion

        #region Checkpoint Functions
        public void SetCheckpoint(Checkpoint checkpoint)
        {
            // Lock the previous checkpoint and unlock the checkpoint after it.
            if (CurrentCheckpoint != null) CurrentCheckpoint.Lock();
            //if (checkpoint.Next != null) checkpoint.Next.SpawnLocked = false;

            // Set the new checkpoint.
            CurrentCheckpoint = checkpoint;
            CurrentCheckpoint.SetAsCheckpoint();

            // Respawn the player.
            SpawnPlayerAndZoom();

            // Fire the new checkpoint event.
            OnNewCheckpoint.Invoke();
        }
        #endregion

        #region Spawn Functions
        public void RespawnPlayer()
        {
            playerManager.SpawnPlayer(CurrentCheckpoint, false);
        }

        public void SpawnPlayerAndZoom()
        {
            playerManager.SpawnPlayer(CurrentCheckpoint, true);
        }

        #endregion
    }
}