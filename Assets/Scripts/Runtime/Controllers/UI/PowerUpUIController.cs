using System;
using System.Collections.Generic;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Controllers.UI
{
    public class PowerUpUIController: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tornadoText = new TextMeshProUGUI();
        [SerializeField] private TextMeshProUGUI magnetText = new TextMeshProUGUI();
        [SerializeField] private TextMeshProUGUI timerText = new TextMeshProUGUI();


        private void Start()
        {
            tornadoText.text = "3";
            magnetText.text = "3";
            timerText.text = "3";
        }

        public void FireTornadoPowerUp()
        {
            string tornadoValue = tornadoText.text;
            if (tornadoValue.Equals("0") )
            {
                return;
                
            }

            int newTextValue;
            if (int.TryParse(tornadoText.text.ToString(), out newTextValue))
            {
                newTextValue -= 1;
                tornadoText.text = newTextValue.ToString();
            }
            
            UISignals.Instance.onTornadoPowerUpFired?.Invoke();

        }

        public void FireMagnetPowerUp()
        {
            string magnetValue = magnetText.text;
            if (magnetValue.Equals("0") )
            {
                return;
                
            }

            int newTextValue;
            if (int.TryParse(magnetText.text.ToString(), out newTextValue))
            {
                newTextValue -= 1;
                magnetText.text = newTextValue.ToString();
            }
            UISignals.Instance.onMagnetPowerUpFired?.Invoke();

        }

        public void FireTimerPowerUp()
        {
            string timerValue = timerText.text;
            if (timerValue.Equals("0") )
            {
                return;
                
            }

            int newTextValue;
            if (int.TryParse(timerText.text.ToString(), out newTextValue))
            {
                newTextValue -= 1;
                timerText.text = newTextValue.ToString();
            }
            UISignals.Instance.onTimerHelpPowerUpFired?.Invoke();

        }
        
        
        
    }
}