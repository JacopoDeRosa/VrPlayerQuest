using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine;
using VrVideoPlayer;

namespace Interface
{
    public class DebugTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private VrDeviceManager _deviceManager;

        private float _timer = -2;

        private void Awake()
        {
            if(Debug.isDebugBuild == false) gameObject.SetActive(false);
        }

        private void Start()
        {
            _deviceManager.onUserPresenceChanged += us =>
            {
                if (us)
                {
                    _timer = -2;
                }
            };
        }

        void Update()
        {
            _timer += Time.deltaTime;
            _timerText.text = TimeSpan.FromSeconds(_timer).ToString("hh':'mm':'ss");
        }
    }
}
