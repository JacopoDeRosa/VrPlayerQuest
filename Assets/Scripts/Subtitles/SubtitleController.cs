using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

namespace Subtitles
{
    public class SubtitleController : MonoBehaviour
    {
       
       
        [SerializeField] private float _characterInterval;
        [SerializeField] private TMP_Text _nameText, _contentText;
        
        private SubtitleAsset _activeAsset;
        private SubtitleDescriptor _activeSubtitle;

        private float _totalTime;

        public void RegisterNewAsset(SubtitleAsset asset)
        {
            if (asset == null) return;
            
            _totalTime = 0;
            asset.ResetActivation();
            _activeSubtitle = null;
            _activeAsset = asset;
        }
        
        private void Update()
        {
            if (_activeAsset == null) return;
            
            _totalTime += Time.deltaTime;
            if (_activeSubtitle != null)
            {
                CheckActiveDescriptor();
            }
            else
            {
                CheckAllDescriptors();
            }
        }

        private void CheckActiveDescriptor()
        {
            if (_activeSubtitle.ShouldDie(_totalTime))
            {
                _activeSubtitle = null;
                _nameText.text = "";
                _contentText.text = "";
            }
        }
        
        private void CheckAllDescriptors()
        {
            foreach (SubtitleDescriptor descriptor in _activeAsset.subtitles)
            {
                if(descriptor.hasActivated) continue;
                
                if (descriptor.ShouldActivate(_totalTime))
                {
                    descriptor.SetActivated(true);
                    _activeSubtitle = descriptor;
                    StartCoroutine(ReadDescriptor(descriptor));
                    break;
                }
            }
        }
        
        private IEnumerator ReadDescriptor(SubtitleDescriptor descriptor)
        {
            _nameText.text = descriptor.speaker;
            _nameText.color = descriptor.speakerColor;
            string currentString = "";
            WaitForSeconds wait = new WaitForSeconds(_characterInterval);
            foreach (var character in descriptor.content)
            {
                currentString += character;
                _contentText.text = currentString;
                yield return wait;
            }
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateSubtitleTemplate()
        {
            string templatePath = Application.persistentDataPath + "/SubTemplate.json";
            
            if (File.Exists(templatePath) == false)
            {
                SubtitleAsset subAsset = new SubtitleAsset();

                string template = JsonUtility.ToJson(subAsset, true);
            
                FileStream stream = File.Create(templatePath);
            
                StreamWriter writer = new StreamWriter(stream);
            
                writer.Write(template);
                writer.Close();
                stream.Close();
            }
        }
    }
}