using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using pong.factory;

namespace pong.factory
{
    public class GenericSpawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private GenericFactory<T> _factory;

        public event Action<T> OnSpawn;


        public T Spawn()
        {
            T t = _factory.GetNewInstance();

            OnSpawn?.Invoke(t);

            return t;
        }

        
    }
}
