using TMPro;
using System;
using UnityEngine;

namespace Interface
{
    public class DebugTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;

        private float _timer = -2;

        private void Awake()
        {
            if(Debug.isDebugBuild == false) gameObject.SetActive(false);
                
        }

        void Update()
        {
            _timer += Time.deltaTime;
            _timerText.text = TimeSpan.FromSeconds(_timer).ToString("hh':'mm':'ss");
        }
    }
}
