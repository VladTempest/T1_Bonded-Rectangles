using System.Collections.Generic;
using UnityEngine;

namespace T_Bonded_Rectangles
{
    public class LineClass : MonoBehaviour
    {
        
        public GameObject firstRectangleToBond;
        public GameObject seconRectangleToBond;
        LineRenderer line;
        private void Awake()
        {
            //Установить ссылки на связанные прямоугольники 
            firstRectangleToBond=BondManager.Instance.firstRectangleToBond;
            seconRectangleToBond = BondManager.Instance.secondRectangleToBond;
        }
        void Start()
        {
        
            //Кэш ссылки на объект лайн, для дальнейшего использования в апдейте
            line = gameObject.AddComponent<LineRenderer>();

        }



        
        void Update()
        {
            List<Vector3> position = new List<Vector3>();
            position.Add(firstRectangleToBond.transform.position);
            position.Add(seconRectangleToBond.transform.position);
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;
            line.SetPositions(position.ToArray());
            line.useWorldSpace = true;
        }

        
    }
}
