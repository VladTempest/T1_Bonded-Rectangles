using System.Collections;
using T_Bonded_Rectangles.Interface;
using UnityEngine;

namespace T_Bonded_Rectangles
{
    public class RectangleSpawner : SingletonBase<RectangleSpawner>, ISubscribers
    {
        private const float PauseForCollisionToHappen = 0.1f;
        
        private int _collisionNumberBeforeSpawn;
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private int _collisionNumber;
        


        private void Awake()
        {
            Subscribe();
        }
        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void SpawnRectangle(Vector2 position)
        {
            
            _collisionNumberBeforeSpawn = _collisionNumber;
            var spawnedRectangle = Instantiate(_prefab, position, transform.rotation);
            StartCoroutine(CheckForCollisions(spawnedRectangle));

        }


        private void IncreaseCollisionNumber()
        {
            _collisionNumber++;
        }



        private IEnumerator CheckForCollisions(GameObject spawnedRectangle)
        {
            //Необходимая пауза для того, что произошел вызов  OnCollisionEnter2D.
            yield return new WaitForSeconds(PauseForCollisionToHappen);
            if (_collisionNumberBeforeSpawn - _collisionNumber != 0)
                Destroy(spawnedRectangle);


        }
        public void Subscribe()
        {
            EventBroker.ScreenClicked += SpawnRectangle;
            EventBroker.CollisionHappen += IncreaseCollisionNumber;
        }
        public void Unsubscribe()
        {
            EventBroker.ScreenClicked -= SpawnRectangle;
            EventBroker.CollisionHappen -= IncreaseCollisionNumber;
        }
    }
}
