using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Daxi.InfrastructureLayer.MonoUtils
{
    public class MonoService:MonoBehaviour
    {
        #region Methods
        public void DestroyObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }

        #endregion

    }
}
