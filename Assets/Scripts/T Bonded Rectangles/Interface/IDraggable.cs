using UnityEngine;

namespace T_Bonded_Rectangles.Interface
{
    
    interface IDraggable
    {
        
        void OnMouseDrag();
        void OnMouseDown();
        Vector2 GetMouseAsWorldPoint();
        void OnMouseUp();
    }
}
