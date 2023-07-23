using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

namespace VrVideoPlayer
{
    public class VrDeviceManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _debugText;

        private bool lastUserPresent = true;

        public event Action<bool> onUserPresenceChanged;
      
        private void FixedUpdate()
        {
            InputDevice head = InputDevices.GetDeviceAtXRNode(XRNode.Head);
            
            if (head.isValid)
            {
                bool presenceFeatureSupported = head.TryGetFeatureValue(CommonUsages.userPresence, out bool userPresent);

                if (Debug.isDebugBuild)
                {
                    _debugText.text = "User Present: " + userPresent;
                    _debugText.color = (userPresent ? Color.green : Color.red);
                }
            
                if (presenceFeatureSupported && userPresent != lastUserPresent)
                {
                    onUserPresenceChanged?.Invoke(userPresent);
                    lastUserPresent = userPresent;
                }
            }
        }
    }
}
