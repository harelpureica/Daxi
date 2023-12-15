

using Daxi.DataLayer.GameItems;
using Daxi.InfrastructureLayer.Signals;
using Daxi.DataLayer.MissionsData;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System;

namespace Daxi.VisualLayer.Missions
{
    public class MissionsTracker:IInitializable
    {

        #region Injects
        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private MissionData _missionData;

      

        #endregion

        #region Fields
        private bool _completed;

        private int _itemsRequirmentPassed;

        private Dictionary<string, bool> _lettersRequirments = new();

        #endregion

        #region Events
        public event Action<bool, int,int, Dictionary<string, bool>> OnMissionTracking;
        #endregion

        #region Properties
        public bool Completed => _completed;

        public int ItemsRequirmentPassed => _itemsRequirmentPassed;

        #endregion
        #region Methods
        public void Initialize()
        {
            _completed = false;
            _signalBus.Subscribe<OnCollectableCollected>(OnCollectableCollectedHandler);
            _itemsRequirmentPassed = 0;
            for (int i = 0; i < _missionData.Items.Count; i++)
            {
                _lettersRequirments.Add(_missionData.Items[i].MyName, false);
            }
            
        }
        public void OnCollectableCollectedHandler(OnCollectableCollected onCollectableCollected)
        {
            if(_completed)
            {
                return;
            }
            if(_missionData.Mode==MissionData.MissionMode.letters)
            {
                if(_lettersRequirments.ContainsKey(onCollectableCollected.Data.MyName))
                {
                    _lettersRequirments[onCollectableCollected.Data.MyName] = true;
                    _itemsRequirmentPassed++;
                }
            }
            else
            {
                for (int i = 0; i < _missionData.Items.Count; i++)
                {
                    if (onCollectableCollected.Data.MyName == _missionData.Items[i].MyName)
                    {
                        _itemsRequirmentPassed++;
                        break;
                    }
                }
            }
           
            if(_itemsRequirmentPassed>=_missionData.ItemsCount||_missionData.Mode==MissionData.MissionMode.dammaging)
            {
                _completed = true;

            }
            OnMissionTracking?.Invoke(_completed, _itemsRequirmentPassed,_missionData.ItemsCount,_lettersRequirments);

        }
        #endregion
    }
   
}
