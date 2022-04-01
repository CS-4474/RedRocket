using Assets.Scripts.DataStructures;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Ship
{
    public class CommandSequencer : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private PlayerManager playerManager;
        #endregion

        #region Fields
        private TimelineData timelineData;

        private float timeOfLastTick;
        #endregion

        #region Properties
        public int CurrentTick { get; private set; }

        public bool HasStarted { get; private set; }
        #endregion

        #region Events
        public UnityEvent OnBegin;

        public UnityEvent OnStop;

        public UnityAction<int> OnTick;
        #endregion

        #region Initialisation Functions
        private void Awake()
        {
            timelineData = GetComponent<TimelineData>();
        }
        #endregion

        #region Update Functions
        private void FixedUpdate()
        {
            // If the sequencer is running, calculate the next step.
            if (!HasStarted) return;

            if (Time.time - timeOfLastTick >= timelineData.TimeStep)
            {
                doTick();

                timeOfLastTick = Time.time;

                if (CurrentTick >= timelineData.Data.RowLength) Stop();
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Begin();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Stop();
            }
        }
        private void doTick()
        {
            // Go over each row, execute any commands on the current tick.
            for (int i = 0; i < timelineData.Data.RowCount; i++)
            {
                // If this part has a command at this tick, execute it.
                if (timelineData.Data.GetRowColumnData(i, CurrentTick, out ValueCommand valueCommand)) playerManager.Player.GetShipPartFromName(timelineData.Data.GetRowLabel(i)).ExecuteCommand(valueCommand);
            }

            OnTick.Invoke(CurrentTick);

            // Increment the tick counter.
            CurrentTick++;
        }
        #endregion

        #region Command Functions
        public void ResetTimelineData()
        {
            timelineData.ResetData(playerManager.Player.PartNames);
        }

        public void Begin()
        {
            // If the sequencer is still running, don't start again.
            if (HasStarted) return;
            else HasStarted = true;

            // Set the time of the last tick to now.
            timeOfLastTick = Time.time;

            // Set the current tick to 0.
            CurrentTick = 0;

            // Invoke the beginning event.
            OnBegin.Invoke();
        }

        public void Stop()
        {
            // If the sequencer was not running, do nothing.
            if (!HasStarted) return;
            else HasStarted = false;

            OnStop.Invoke();
        }
        #endregion
    }
}