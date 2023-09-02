using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pong.core;

namespace pong.factory
{
    public class BonusFactory : GenericFactory<BonusView>
    {
        [SerializeField] private BoxCollider _spawnArea;

        private Vector3 GetSpawnPosition()
        {
            float nextX = Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x);
            float nextZ = 0f;
            float nextY = Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y);

            return new Vector3(nextX, nextY, nextZ);
        }

        public override BonusView GetNewInstance()
        {
            return Instantiate(_prefab, GetSpawnPosition(), Quaternion.identity);            
        }
    }
}
