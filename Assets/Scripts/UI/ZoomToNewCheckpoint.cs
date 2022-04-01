using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Spawning;

namespace Assets.Scripts.UI
{
    public class ZoomToNewCheckpoint : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private CheckpointManager checkpointManager;
        #endregion

        #region Fields
        private Camera camera = null;
        private bool doneZoom = true;
        public float oneWayZoomDuration = 3.0f;
        public float zoomOutCameraSize = 14.0f;
        #endregion

        #region Initialisation Functions
        private void Start()
        {
            camera = GetComponent<Camera>();
        }
        #endregion

        private void Update()
        {
            if (!doneZoom)
            {
                StartCoroutine(ZoomToNextCheckpoint());
                doneZoom = true;
            }
        }

        #region Camera Functions
        public void ShowNewCheckpoint()
        {
            doneZoom = false;
        }


        private IEnumerator ZoomToNextCheckpoint(){
            //set up some variables for lerping
            float timeElapsed = 0.0f;
            float zoomDuration = oneWayZoomDuration;

            Checkpoint nextCheckpointObj = checkpointManager.CurrentCheckpoint.Next;
            if (nextCheckpointObj == null) yield break;

            //x positions
            float currentCheckpointX = checkpointManager.CurrentCheckpoint.transform.position.x;

            float nextCheckpointX = checkpointManager.CurrentCheckpoint.Next.transform.position.x;

            //y positions
            float currentCheckpointY = checkpointManager.CurrentCheckpoint.transform.position.y;

            float nextCheckpointY = checkpointManager.CurrentCheckpoint.Next.transform.position.y;

            //camera zoom levels
            float zoomedIn = camera.orthographicSize;
            float zoomedOut = zoomOutCameraSize;

            //zoom out
            while (timeElapsed <= zoomDuration)
            {
                transform.position = new Vector3(Mathf.Lerp(currentCheckpointX, nextCheckpointX, timeElapsed / zoomDuration), Mathf.Lerp(currentCheckpointY, nextCheckpointY, timeElapsed / zoomDuration), transform.position.z);
                camera.orthographicSize = Mathf.Lerp(zoomedIn, zoomedOut, timeElapsed / zoomDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = new Vector3(nextCheckpointX, nextCheckpointY, transform.position.z);
            camera.orthographicSize = zoomedOut;

            //hold
            timeElapsed = 0.0f;
            while (timeElapsed <= zoomDuration / 3)
            {
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            //zoom back in
            timeElapsed = 0.0f;
            while (timeElapsed <= zoomDuration)
            {
                transform.position= new Vector3(Mathf.Lerp(nextCheckpointX, currentCheckpointX, timeElapsed / zoomDuration), Mathf.Lerp(nextCheckpointY, currentCheckpointY, timeElapsed / zoomDuration), transform.position.z);
                camera.orthographicSize = Mathf.Lerp(zoomedOut, zoomedIn, timeElapsed / zoomDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = new Vector3(currentCheckpointX, currentCheckpointY, transform.position.z);
            camera.orthographicSize = zoomedIn;

        }
        #endregion


    }
}
