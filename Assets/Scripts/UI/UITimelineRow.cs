using Assets.Scripts.DataStructures;
using Assets.Scripts.Ship;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UITimelineRow : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private GameObject buttonPrefab;
        #endregion

        #region Fields
        private UITimeline timelineContainer;

        private TimelineData timelineData;
        #endregion

        #region Properties
        public int Index { get; private set; }
        #endregion

        #region Initialisation Functions

        int prevCol = -1;
        int prevRow = -1;
        private void Awake()
        {
            timelineContainer = GetComponentInParent<UITimeline>();
            timelineData = GetComponentInParent<TimelineData>();
        }

        public void Initialise(int index, int columnCount)
        {
            // Set the row index.
            Index = index;

            // Initialise the columns.
            initialiseColumns(columnCount);
        }

        private void initialiseColumns(int columnCount)
        {
            for (int i = 0; i < columnCount; i++)
            {
                Button newButton = Instantiate(buttonPrefab, transform).GetComponent<Button>();
                newButton.name = "Step " + i;

                if (timelineData.Data.GetRowColumnData(Index, i, out ValueCommand _))
                {
                    ColorBlock buttonColours = newButton.colors;
                    buttonColours.normalColor = new Color(0, 1, 0);
                    buttonColours.highlightedColor = new Color(0, 1, 0);
                    buttonColours.selectedColor = new Color(0, 1, 0);
                    newButton.colors = buttonColours;
                }

                int iCopy = i;
                newButton.onClick.AddListener(() =>
                {
                    editTimedCommand(iCopy, Input.GetKey(KeyCode.LeftControl), Input.GetKey(KeyCode.LeftShift));
                });
            }
        }
        #endregion

        #region Get Functions
        public Button GetButton(int columnIndex) => transform.GetChild(columnIndex).GetComponent<Button>();
        #endregion

        #region Click Functions
        private void editTimedCommand(int verticalIndex, bool clearVal, bool batchSelect)
        {
            // timelineContainer.ShowEditCommandWindow(Index, verticalIndex);
            if (batchSelect && prevRow == Index && prevCol != verticalIndex)
            {
                if (prevCol < verticalIndex)
                {
                    for (int i = prevCol + 1; i < verticalIndex; i++)
                    {
                        if (Index == 4)
                        {
                            timelineContainer.ToggleTurret(Index, i);

                        }
                        else
                        {
                            timelineContainer.ToggleThrusterStrength(Index, i, clearVal);
                        }
                    }
                }
                else if (prevCol > verticalIndex)
                {
                    for (int i = prevCol - 1; i > verticalIndex; i--)
                    {
                        if (Index == 4)
                        {
                            timelineContainer.ToggleTurret(Index, i);

                        }
                        else
                        {
                            timelineContainer.ToggleThrusterStrength(Index, i, clearVal);
                        }
                    }
                }

                prevRow = -1;
                prevCol = -1;
            }
            else
            {
                prevRow = Index;
                prevCol = verticalIndex;
            }
            if (Index == 4)
            {
                timelineContainer.ToggleTurret(Index, verticalIndex);

            }
            else
            {
                timelineContainer.ToggleThrusterStrength(Index, verticalIndex, clearVal);
            }



        }
        #endregion
    }
}