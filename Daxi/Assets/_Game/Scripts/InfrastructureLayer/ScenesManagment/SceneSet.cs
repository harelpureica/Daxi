using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Daxi.InfrastructureLayer.ScenesManagment
{
    [Serializable]
    public struct SceneSet
    {
        public string MyScene;

        public List<string> MyDependency;
    }
}
