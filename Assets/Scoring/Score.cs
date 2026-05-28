using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using System;

public class Score : RCCP_GenericComponent {
    private RCCP_CarController carController;

    int metters = 0;
    int score = 0;
    int multiplier = 1;
    int multiplierModifier = 0;
    float dist;
    int timer;


    [Tooltip("Text showing the current score.")]
    [Space()]
    public Text scoreText;

    
    private void Update() {
        //  Getting active player car controller on the scene.
        carController = RCCPSceneManager.activePlayerVehicle;

        //  If no active player car found, return.
        if (!carController)
            return;

        /// <summary>
        /// Calculate the score when drifting.
        /// </summary>
        if (carController.handbrakeInput_V == 1f && carController.speed >= 0 && carController.steerInput_V != 0) {
            timer++;
            metters += (int)Math.Abs(carController.speed) * 10 / 36;
            metters /= timer;
            if (multiplierModifier < metters) {
                if (multiplierModifier + 100 <= metters) {
                    multiplierModifier += 100;
                    multiplier += multiplierModifier / 100;
                }
            }
        }
        else {
            timer = 0;
            metters = 0;
        }

        score += metters * multiplier;
        scoreText.text = "Score: " + score;
    }

}
