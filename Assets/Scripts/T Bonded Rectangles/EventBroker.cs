using System;
using UnityEngine;

namespace T_Bonded_Rectangles
{
    public static class EventBroker
    {
        #region Actions/Gameplay

        public static event Action<Vector2> ScreenClicked;
        public static event Action CollisionHappen;

        #endregion Actions/Gameplay
        
        #region Calls/Gameplay

        public static void CallScreenClicked(Vector2 position)
        {
          ScreenClicked?.Invoke(position);
        }
        public static void CallCollisionHappen()
        {
            CollisionHappen?.Invoke();
        }
        

        #endregion Calls/Gameplay
    }
}
