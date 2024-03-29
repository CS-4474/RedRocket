﻿using Assets.Scripts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Spawning
{
    public class CheckpointManager : MonoBehaviour
    {
        #region Inspector Fields
        //[SerializeField]
        //private Checkpoint spawnPoint = null;

        [SerializeField]
        private List<Checkpoint> CheckpointList;

        [SerializeField]
        private PlayerManager playerManager = null;

        [SerializeField]
        private HintSystem hintSystemScript = null;

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

        public HintSystem HintSystem => hintSystemScript;

        public float RequiredStillTime => requiredStillTime;

        public float RequiredUpwardness => requiredUpwardness;

        public float RequiredVelocity => requiredVelocity;

        public int selectedLevel;

        public int maxLevel;
        #endregion

        #region Events
        public UnityEvent OnNewCheckpoint;
        #endregion

        #region Initialisation Functions
        private void Start()
        {
            selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 0);
            maxLevel = PlayerPrefs.GetInt("maxLevel", 0);

            SetCheckpoint(CheckpointList[selectedLevel]);

            if (maxLevel == 0)
            {
                hintSystemScript.ShowHint(maxLevel);
            }
        }
        #endregion

        #region Checkpoint Functions
        public void SetCheckpoint(Checkpoint checkpoint)
        {
            int index = 0;
            for (index = 0; index < CheckpointList.Count; index++)
            {
                if (CheckpointList[index] == checkpoint)
                {
                    if (index > maxLevel)
                    {
                        PlayerPrefs.SetInt("maxLevel", index);
                        maxLevel = index;
                        hintSystemScript.ShowHint(maxLevel);
                    }
                    break;
                }
            }

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RespawnPlayer();
            }
        }

        #endregion
    }
}