using T_Bonded_Rectangles.GameplayEntities;
using UnityEngine;

namespace T_Bonded_Rectangles
{
    public class BondManager : SingletonBase<BondManager>
    {
        private bool IsBondingActive { set; get; }

        public GameObject firstRectangleToBond = null;
        public GameObject secondRectangleToBond = null;

        public bool CheckIfBondingActive()
        {
            return IsBondingActive;
        }

        public void ChooseFirstRectangleForBonding(GameObject chosenRectangle)
        {
            IsBondingActive = true;
            firstRectangleToBond = chosenRectangle;
        }

        public void CreateBondBetweenRectangles(GameObject secondRectangleToBond)
        {
            
            IsBondingActive = false;

            //Установить ссылку на связанный прямоугольник у каждого прямоугольника из связанной пары
            this.secondRectangleToBond = secondRectangleToBond;

            var firstRectangleToBondRectangleCalss = firstRectangleToBond.GetComponent<RectangleClass>();
            var secondRectangleToBondRectangleCalss = secondRectangleToBond.GetComponent<RectangleClass>();

            firstRectangleToBondRectangleCalss.CreateBondWith(secondRectangleToBond);
            secondRectangleToBondRectangleCalss.CreateBondWith(firstRectangleToBond);

            //Создать объект "Связывающая линия"
            var bondingLine = LineSpawner.Instance.InstantiateBondingLine();

            //Установить ссылку на связывающую линию у каждого прямоуглоьника из связанной пары
            firstRectangleToBondRectangleCalss.ChooseBondingLine(bondingLine);
            secondRectangleToBondRectangleCalss.ChooseBondingLine(bondingLine);


        }

    }
}
