﻿using AirSeaBattle.Game.Control;
using AirSeaBattle.Game.Simulation;
using UnityEngine;

namespace AirSeaBattleUnity
{
    public class UnitySimulationInputManager : MonoBehaviour
    {
        public ControlSchema Controls { get; set; }
        public SimulationInput SimulationInput { get; private set; }

        private void Update()
        {
            if (Controls != null && SimulationInput != null)
            {
                for (int i = 0; i < Controls.Down.Length; i++)
                {
                    UpdateKey(SimulationInput.Up, Controls.Up);
                    UpdateKey(SimulationInput.Down, Controls.Down);
                    UpdateKey(SimulationInput.Fire, Controls.Fire);
                }
            }
        }

        private void UpdateKey(InputButton button, KeyCode[] keyCode)
        {
            for (int i = 0; i < keyCode.Length; i++)
            {
                var key = keyCode[i];

                if (Input.GetKeyDown(key))
                {
                    button.SimulateButtonDown();
                }
                else if (Input.GetKeyUp(key))
                {
                    button.SimulateButtonUp();
                }
            }
        }

        public void AttachInput(SimulationInput input)
        {
            SimulationInput = input;
        }
    }
}
