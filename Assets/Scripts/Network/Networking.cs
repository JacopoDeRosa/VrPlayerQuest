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

            onComplete?.Invoke(visitor != null && request.result == UnityWebRequest.Result.Success, visitor);
        
            request.Dispose();
        }

        //Questa quando inizia l'esperienza, id deve essere quello del visitor nella prima chiamata
        public static IEnumerator StartVisitorContent(int visitorId)
        {
            string url = AppManifest.Instance.ServerAddress + "/visitor_zones/start_content";

            WWWForm form = new WWWForm();
            
            form.AddField("visitor_id", visitorId);
            form.AddField("zone_id", AppManifest.Instance.DeviceId);
            
            Debug.Log("Sending Post Request To: " + url);
            
            UnityWebRequest request = UnityWebRequest.Post(url, form);
        
            if(AppManifest.Instance.SendServerKey) request.SetRequestHeader(AppManifest.Instance.ServerKeyName, AppManifest.Instance.ServerKey);

            yield return request.SendWebRequest();
            
            Debug.Log("Start Request Result is: " + request.downloadHandler.text);
        }
        
        //Questa quando finisce l'esperienza, id deve essere quello del visitor nella prima chiamata
        public static IEnumerator StopVisitorContent(int visitorId)
        {
            string url = AppManifest.Instance.ServerAddress + "/visitor_zones/stop_content";
            
            WWWForm form = new WWWForm();
            
            form.AddField("visitor_id", visitorId);
            form.AddField("zone_id", AppManifest.Instance.DeviceId);
            
            Debug.Log("Sending Post Request To: " + url);
            
            UnityWebRequest request = UnityWebRequest.Post(url, form);
        
            if(AppManifest.Instance.SendServerKey) request.SetRequestHeader(AppManifest.Instance.ServerKeyName, AppManifest.Instance.ServerKey);

            yield return request.SendWebRequest();
            
            Debug.Log("Stop Request Result is: " + request.downloadHandler.text);
        }
        
    
    }

    [Serializable]
    public class VisitorAudioData
    {
        public int visitor_id;
        public int zone_id;
    }
   
}
