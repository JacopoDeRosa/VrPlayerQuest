using UnityEngine;

namespace Interface
{
   public class CanvasCameraFollower : MonoBehaviour
   {
      [SerializeField] private Transform _cameraPivot;

      [SerializeField, Range(1, 10)] private float _hardness;

      private void Update()
      {
         transform.rotation = Quaternion.Lerp(transform.rotation, _cameraPivot.transform.rotation, Mathf.Clamp01(_hardness * Time.deltaTime));
      }
   }
}
