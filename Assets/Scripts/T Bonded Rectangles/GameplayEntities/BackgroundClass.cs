using T_Bonded_Rectangles.Interface;
using UnityEngine;

namespace T_Bonded_Rectangles
{
    public class BackgroundClass: MonoBehaviour, ILeftClickable
    {
        [SerializeField]
        private Vector2 _worldPosition = Vector2.zero;
        private Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
        }
        
        public void OnLeftClick()
        {
            _worldPosition = _camera.ScreenToWorldPoint(Input.mousePosition); 
            EventBroker.CallScreenClicked(_worldPosition);
        }
    }
}
