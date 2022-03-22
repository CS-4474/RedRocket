using Assets.Scripts.Spawning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCentre : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField]
    private CheckpointManager checkpointManager;
    #endregion

    #region Centre Functions
    public void CentreOnPlayer()
    {
        transform.position = checkpointManager.CurrentCheckpoint.transform.position - new Vector3(0, 0, 10);
    }
    #endregion
}
