using System;
using System.Collections;
using Manifest;
using UnityEngine;
using UnityEngine.Networking;

namespace Network
{
    public static class Networking
    {
        public static IEnumerator GetVisitorInZoneRoutine(Action<bool, VisitorInZone> onComplete)
        {
            string url = AppManifest.Instance.ServerAddress + "/visitors?zone_id=" + AppManifest.Instance.DeviceId;
        
            UnityWebRequest request = UnityWebRequest.Get(url);
        
            if(AppManifest.Instance.SendServerKey) request.SetRequestHeader(AppManifest.Instance.ServerKeyName, AppManifest.Instance.ServerKey);
        
            request.timeout = 2;
        
            yield return request.SendWebRequest();

            string result = request.downloadHandler.text;
        
            Debug.Log("Error is: " + request.error);
        
            Debug.Log("Result is: " + result);

            VisitorsInZone visitors = JsonUtility.FromJson<VisitorsInZone>(result);

            VisitorInZone visitor = null;
        
            if(visitors != null)  visitor = visitors.visitor[0];
        
            if(visitor != null) Debug.Log(visitor.language);

            onComplete?.Invoke(visitor != null && request.isNetworkError == false && request.isHttpError == false, visitor);
        
            request.Dispose();
        }
    
    }
}
