using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DataStructures;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class Gyro : ShipPart
    {
        private float angle = 0;

        private Rigidbody2D rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            //float distance = Mathf.DeltaAngle(rigidbody.transform.rotation.eulerAngles.z, angle);
            //rigidbody.AddTorque(-distance * Time.fixedDeltaTime);
            //float velocity = 0;
            //Mathf.SmoothDampAngle(rigidbody.transform.rotation.eulerAngles.z, angle, ref velocity, 1);
            //rigidbody.MoveRotation(-Mathf.LerpAngle(rigidbody.transform.rotation.eulerAngles.z, angle, .9f));
            rigidbody.transform.SetPositionAndRotation(rigidbody.transform.position, Quaternion.Euler(0, 0, -Mathf.LerpAngle(rigidbody.transform.rotation.eulerAngles.z, angle, .999f)));
        }

        #region Command Functions
        public override void ExecuteCommand(ValueCommand valueCommand)
        {
            switch (valueCommand.Command.CommandType)
            {
                case CommandType.SetGyro:
                    angle = (valueCommand.GetFloatValue() % 360) + 90;
                    break;
            }
        }
        #endregion
    }
}
