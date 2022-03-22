using Assets.Scripts.Ship;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlayerFollow : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField]
        private PlayerManager playerManager;
        #endregion

        #region Track Functions
        public void CentreOnPlayer()
        {
            if (!playerManager.PlayerExists) return;
            transform.position = playerManager.Player.MainPart.transform.position - new Vector3(0, 0, 10);
        }
        #endregion

        #region Update Functions
        private void LateUpdate()
        {
            CentreOnPlayer();
        }
        #endregion
    }
}