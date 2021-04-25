using T_Bonded_Rectangles.Interface;
using UnityEngine;

namespace T_Bonded_Rectangles
{
    public class InputController : MonoBehaviour
    {
        private float _lastClickTime=0f;
        private float _timeSinceLastClick = 0f;
        private const float DoubleClickTime = 0.2f;
        private RaycastHit2D _hit;
        private Camera _camera;




        private void Start()
        {
            _camera = Camera.main;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IfRayCollidedWithSmth(out _hit))
                {
                    //Учет разницы во времени с преыдущим кликом, для проверки факта двойного клика
                    _timeSinceLastClick = Time.time - _lastClickTime;
                    if (_timeSinceLastClick > DoubleClickTime)
                    {
                        ILeftClickable leftClickable = _hit.transform.GetComponent<ILeftClickable>();
                        leftClickable?.OnLeftClick();
                    }
                    else
                    {
                        IDoubleClickable doubleClickable = _hit.transform.GetComponent<IDoubleClickable>();
                        doubleClickable?.OnDoubleClick();
                    }
                    _lastClickTime = Time.time;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (IfRayCollidedWithSmth(out _hit))
                {
                    IRightClickable rightClickable = _hit.transform.GetComponent<IRightClickable>();
                    rightClickable?.OnRightClick(); 
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                
            }
        }

        private bool IfRayCollidedWithSmth(out RaycastHit2D hit)
        { 
            Vector3 mouseposition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseposition2D = new Vector2(mouseposition.x, mouseposition.y);
            hit =  Physics2D.Raycast(mouseposition2D, Vector2.zero);
            return (!ReferenceEquals(hit.collider, null));
        }
        
        
        
        

    }
}
