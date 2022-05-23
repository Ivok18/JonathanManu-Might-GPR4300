using UnityEngine;
using TMPro;
using System;

namespace Might.UI
{
    public class UIWaveTextBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI waveText;
        private int currentWaveNum;

        private void Start()
        {
            currentWaveNum = 1;
        }

        public void UpdateWaveText()
        {
            currentWaveNum++;
            waveText.text = "WAVE " + currentWaveNum.ToString();
        }
       
    }
}
