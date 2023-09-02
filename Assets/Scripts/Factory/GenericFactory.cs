using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFactory<T> : MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] protected T _prefab;

    public virtual T GetNewInstance()
    {
        return Instantiate(_prefab);
    }
}
