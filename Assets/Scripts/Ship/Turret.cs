using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DataStructures;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class Turret : ShipPart
    {
        #region Inspector Fields
        [SerializeField]
        private GameObject bulletPrefab;

        [SerializeField]
        private Transform spawner;

        [SerializeField]
        [Range(1, 30)]
        private float bulletSpeed;
        #endregion

        void FireBullet()
        {
            Rigidbody2D bullet = Instantiate(bulletPrefab, spawner.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z)).GetComponent<Rigidbody2D>();
            bullet.AddForce(transform.parent.up * bulletSpeed, ForceMode2D.Impulse);
        }

        #region Command Functions
        public override void ExecuteCommand(ValueCommand valueCommand)
        {
            switch (valueCommand.Command.CommandType)
            {
                case CommandType.Fire:
                    FireBullet();
                    break;
            }
        }
        #endregion
    }
}