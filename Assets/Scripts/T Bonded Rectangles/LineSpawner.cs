using UnityEngine;
namespace T_Bonded_Rectangles
{
    public class LineSpawner : SingletonBase<LineSpawner>
    {
        [SerializeField]
        private GameObject line;
        
        public GameObject InstantiateBondingLine()
        {
            return Instantiate(line, transform.position, transform.rotation);
        } 
    }
}
