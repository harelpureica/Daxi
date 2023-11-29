using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daxi.DataLayer.LevelsData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelsData/LevelData")]
    public class LevelData : ScriptableObject
    {
        #region Fields
        [SerializeField]
        private string _sceneName;

        [SerializeField]
        private Sprite _levelbuttonSprite;

        [SerializeField]
        private bool _locked;

        [SerializeField] 
        private string _levelSelctionSceneName;

        [SerializeField]
        private string _nextlevelSceneName;

        #endregion

        #region Properties
        public string SceneName => _sceneName; 
        public Sprite LevelbuttonSprite => _levelbuttonSprite;

        public bool Locked { get => _locked; set => _locked = value; }

        public string LevelSelctionSceneName => _levelSelctionSceneName;
        public string NextlevelSceneName => _nextlevelSceneName;

        #endregion
    }
}
