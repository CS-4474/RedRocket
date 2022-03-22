using Assets.Scripts.DataStructures;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class ThrusterPart : ShipPart
    {
        #region Inspector Fields
        [SerializeField]
        private int maxForce;

        [SerializeField]
        private int minForce;

        [SerializeField]
        private ParticleSystem particles;

        [SerializeField]
        private AudioSource rocketSound;
        #endregion

        #region Fields
        private ConstantForce2D force;
        #endregion

        #region Properties
        public float CurrentForce => force.relativeForce.y;
        #endregion

        #region Constructors

        #endregion

        #region Initialisation Functions
        private void Start()
        {
            force = GetComponent<ConstantForce2D>();
            force.force = Vector2.zero;
        }
        #endregion

        #region Command Functions
        public override void ExecuteCommand(ValueCommand valueCommand)
        {
            switch (valueCommand.Command.CommandType)
            {
                case CommandType.SetValue:
                    force.relativeForce = new Vector2(0, valueCommand.GetFloatValue() * maxForce);

                    ParticleSystem.EmissionModule emissionModule = particles.emission;
                    ParticleSystem.MainModule mainModule = particles.main;

                    emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(valueCommand.GetFloatValue() * 50);
                    mainModule.startSpeed = new ParticleSystem.MinMaxCurve(valueCommand.GetFloatValue());

                    rocketSound.volume = valueCommand.GetFloatValue() / 6.0f;
                    rocketSound.mute = rocketSound.volume == 0;
                    break;
            }
        }
        #endregion
    }
}
