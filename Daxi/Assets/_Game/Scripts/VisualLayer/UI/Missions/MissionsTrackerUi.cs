using Cysharp.Threading.Tasks;
using Daxi.DataLayer.MissionsData;
using Daxi.VisualLayer.Levels;
using Daxi.VisualLayer.Missions;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static Daxi.DataLayer.MissionsData.MissionData;

namespace Daxi.VisualLayer.UI.Missions
{
    public class MissionsTrackerUi:MonoBehaviour
    {
        #region Injects
        [Inject]
        private MissionsTracker missionsTracker;

        [Inject]
        private MissionData _missionData;

        [Inject]
        private LevelManager _levelManager;

        #endregion

        #region Fields

        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private AudioClip _clip;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private ImageIndicator _imageIndicatorPrefab;

        [SerializeField]
        private RectTransform _parentLayout;

        [SerializeField]
        private Button _popupButton;

        [SerializeField]
        [Range(0f, 1f)]
        private float _lettersInActiveAlpha; 

        private Dictionary<string,ImageIndicator>_lettersIndicators= new();

        private bool playedAudio;


        #endregion

        #region Methods
        private async void Start()
        {
            missionsTracker.OnMissionTracking -= OnMissionTrackingHandler;
            missionsTracker.OnMissionTracking += OnMissionTrackingHandler;
          
            
            if(_missionData.Mode==MissionMode.collectables)
            {
                _text.gameObject.SetActive(true);
            }else
            {
                _text.gameObject.SetActive(false);
            }
           
            if (_missionData.Mode == MissionMode.letters)
            {                                
                for (int x = 0; x < _missionData.Items.Count; x++)
                {
                    var indiactor = Instantiate(_imageIndicatorPrefab, _parentLayout);
                    indiactor.SetImage(_missionData.Items[x].Sprite);
                    indiactor.SetAlpha(_lettersInActiveAlpha);
                    _lettersIndicators.Add(_missionData.Items[x].MyName,indiactor);
                }
            }
            OnMissionTrackingHandler(false, 0, _missionData.ItemsCount, null);                    
        }
       
        public void SubscribeToButton(UnityEngine.Events.UnityAction callback)
        {
            _popupButton.onClick.RemoveAllListeners();
            _popupButton.onClick.AddListener(callback);
        }
        public void OnMissionTrackingHandler(bool completed, int requirmentsPassed, int requirments,Dictionary<string,bool>lettersRequierments)
        {
            if(completed&&!playedAudio)
            {
                playedAudio = true;
                _audioSource.PlayOneShot(_clip);
            }
            if (_missionData.Mode == MissionMode.letters)
            {
                if (lettersRequierments == null )
                {
                    for (int i = 0; i < _missionData.Items.Count; i++)
                    {
                        var key = _missionData.Items[i].MyName;
                        _lettersIndicators[key].SetAlpha(_lettersInActiveAlpha);
                    }
                    return;
                }

                for (int i = 0; i < _missionData.Items.Count; i++)
                {
                    var key= _missionData.Items[i].MyName;
                    if( lettersRequierments[key])
                    {
                        _lettersIndicators[key].SetAlpha(1);
                    }
                    else
                    {
                        _lettersIndicators[key].SetAlpha(_lettersInActiveAlpha);
                    }
                }
                
                
            }
            if (_missionData.Mode == MissionMode.collectables)
            {
                _text.text = $"{requirmentsPassed}/{requirments}";

            }
            else
            {
                _text.text ="";
            }
        }

        #endregion
    }
}
