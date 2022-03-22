using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DataStructures;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ParachutePart : ShipPart
    {

        private Rigidbody2D rigidbody;
        private SpriteRenderer spriteRenderer;

        #region Inspector Fields
        [SerializeField]
        private float drag = 0.9f;

        [SerializeField]
        private bool deployed;

        [SerializeField]
        private GameObject parachute;
        #endregion

        #region Fields
        private Rigidbody2D parentBody;
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            parentBody = GetComponentInParent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (deployed)
            {
                Vector2 normalisedParachute = new Vector2(0, 1); // parachute.transform.rotation.eulerAngles.normalized;

                //if (parentBody.velocity.sqrMagnitude < 10) return;

                Vector2 newVelocity;
                newVelocity.x = 0;// Math.Min(parentBody.velocity.x * 0.1f, normalisedParachute.x * drag);
                newVelocity.y = -(parentBody.velocity.y * drag);

                parentBody.AddForce(newVelocity);
                //Vector2 velocityDirection = rigidbody.velocity.normalized;
                //transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(-velocityDirection.y, -velocityDirection.x) * Mathf.Rad2Deg));
            }
        }

        public void Deploy()
        {
            //rigidbody.drag = drag;
            deployed = true;
            parachute.SetActive(true);
            
        }

        public void Retract()
        {
            //rigidbody.drag = 0;
            deployed = false;
            parachute.SetActive(false);
        }

        #region Command Functions
        public override void ExecuteCommand(ValueCommand valueCommand)
        {
            switch (valueCommand.Command.CommandType)
            {
                case CommandType.Deploy:
                    if (valueCommand.GetBoolValue()) Deploy();
                    else Retract();
                    break;
            }
        }
        #endregion
    }
}