using T_Bonded_Rectangles.Interface;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace T_Bonded_Rectangles.GameplayEntities
{
    public class RectangleClass:MonoBehaviour,IDoubleClickable,IDraggable,IRightClickable 
    {
        
       
        private Vector2 _mouseOffset=Vector2.zero;
        
        [SerializeField]
        private bool _haveCollision = false;
        
        [SerializeField]
        private Color _color = Color.black;
        [SerializeField]
        private Vector2 _startPosition = Vector2.zero;
        [SerializeField]
        private GameObject _bondedRectangle = null;
        [SerializeField]
        private GameObject _bondingLine = null;
        
        private bool Dragged { get; set; } = false;
        private bool Bonded { get; set; } = false;
        private bool HaveCollision
        {
            get => _haveCollision;
            set
            {
                if (value)
                {
                    //Вызвать событие CollisionHappen
                    EventBroker.CallCollisionHappen();
                }
                _haveCollision = value;
            }
        }
        private Vector2 CurrentPosition { get; set; }
        private Vector2 StartPosition
                {
                    get => _startPosition;
                    set
                    {
                        //Запомнить новое стартовое положение только в том случае, если прямоугольник уже не перетаксивают   
                        if (Dragged==false)
                        {
                            _startPosition = value;
                        }
                    }
                }
       
        private void Awake()
        {
            ChooseOwnColor();
            CurrentPosition=GetRectanglePosition();
            StartPosition = GetRectanglePosition();
            
        }
        

        private void ChooseOwnColor()
        {
            _color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
            gameObject.GetComponent<SpriteRenderer>().color = _color;
        }
        private Vector2 GetRectanglePosition()
                {
                    return transform.position;
                }
        public void OnDoubleClick()
        {
            if (Bonded)
            {
                DestroyTheBond();
            }
            Destroy(gameObject);
        }
        
        public void OnMouseDrag()
        {
            
            CurrentPosition = GetMouseAsWorldPoint() + _mouseOffset;
            StartPosition = transform.position;
            if (!HaveCollision&&Dragged)
            {
                transform.position =CurrentPosition;  
            }
            
        }
        public void OnMouseDown()
        {
            Dragged = true;
            
            var position = gameObject.transform.position;
            //Запомнить смещение указателя мыши от координаты центра перетаскиваемого прямоугольника
            _mouseOffset = new Vector2(position.x,position.y) - GetMouseAsWorldPoint();;
        }
        public Vector2 GetMouseAsWorldPoint()
        {
            Vector2 mousePoint = Input.mousePosition;
            Vector3 mousePosition=Camera.main.ScreenToWorldPoint(mousePoint);
            return new Vector2(mousePosition.x, mousePosition.y);
        }
        public void OnMouseUp()
        {
            Dragged = false;
            StartPosition = transform.position;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            HaveCollision = true;
            Dragged = false;
            transform.position = StartPosition;
            
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            HaveCollision = false;
        }
        
        public void OnRightClick()
        {
            if (!Bonded)
            {
                Bonded = true;
                if (!BondManager.Instance.CheckIfBondingActive())
                {
                    BondManager.Instance.ChooseFirstRectangleForBonding(gameObject);
                }
                else
                {
                    BondManager.Instance.CreateBondBetweenRectangles(gameObject);
                }
            }
            else if (!BondManager.Instance.CheckIfBondingActive())
            {
                DestroyTheBond();
            }
        }
        public void CreateBondWith(GameObject rectangleToBond)
        {
            _bondedRectangle=rectangleToBond;
        }
        public void DestroyTheBond()
        {
            var bondToDestroy = _bondingLine;
            //Деактивация связывающей линии ,чтобы избежать nullReferenceException 
            _bondingLine.SetActive(false);
            //Сброс всех ссылок на связи в связанном прямоугольнике
            var bondedRectangleClass = _bondedRectangle.GetComponent<RectangleClass>();
            
            bondedRectangleClass.Bonded = false;
            bondedRectangleClass._bondedRectangle = null;
            bondedRectangleClass._bondingLine = null;
            //Сброс всех ссылок на связи в текущем прямоугольнике
            Bonded = false;
            _bondedRectangle = null;
            _bondingLine = null;
            
            Destroy(bondToDestroy);

        }
        public void ChooseBondingLine(GameObject bondingLine)
        {
            _bondingLine = bondingLine;
        }

    }
        
}

