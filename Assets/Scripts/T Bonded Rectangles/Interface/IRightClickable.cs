using UnityEngine;

namespace T_Bonded_Rectangles.Interface
{
    interface IRightClickable
    {
        // Start is called before the first frame update
        void OnRightClick();
        void CreateBondWith(GameObject rectangleToBond);
        void DestroyTheBond();

        void ChooseBondingLine(GameObject bondingLine);
    }
}
