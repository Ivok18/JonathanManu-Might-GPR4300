using Might.WaveSystem;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Might.UI
{
    public class UIWinWavePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI panelText;
        [SerializeField] private Button panelButton;
        
        private void OnEnable()
        {
            WaveSpawner.OnWaveEndedCallback += HandleWaveEnd;           
        }

        private void OnDisable()
        {
            WaveSpawner.OnWaveEndedCallback -= HandleWaveEnd;
        }

        private void HandleWaveEnd()
        {
            panelText.gameObject.SetActive(true);
            panelButton.gameObject.SetActive(true);
        }  
    }
}
