
using UnityEngine;

namespace Daxi.InfrastructureLayer.Inputs
{
    public class InputManager : IInputManager
    {
        public bool IsClicking()
        {
            var isClikcing = false;
            if(SystemInfo.deviceType==DeviceType.Handheld)
            {
                if (Input.touchCount == 0)
                {
                    isClikcing= false;
                }
                if (Input.touches[0].phase==TouchPhase.Began)
                {
                    isClikcing= true;
                }
                
            }
            else
            {
                isClikcing= Input.GetKeyDown(KeyCode.Mouse0);
            }
            return isClikcing;
        }
    }
}
