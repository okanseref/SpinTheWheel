using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class GenericObjectPool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private Transform _parent;
        private Stack<T> _pool = new Stack<T>();
        private HashSet<T> _activeObjects = new HashSet<T>();

        public GenericObjectPool(T prefab, int initialSize = 0, Transform parent = null)
        {
            this._prefab = prefab;
            this._parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                CreateNewObject();
            }
        }

        public T Get()
        {
            T obj;

            if (_pool.Count > 0)
            {
                obj = _pool.Pop();
            }
            else
            {
                obj = CreateNewObject();
            }

            _activeObjects.Add(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Return(T obj)
        {
            if (_activeObjects.Remove(obj))
            {
                obj.gameObject.SetActive(false);
                _pool.Push(obj);
            }
            else
            {
                Debug.LogWarning("Trying to return an object that wasn't from this pool");
            }
        }

        public void ReturnAll()
        {
            foreach (var obj in _activeObjects)
            {
                obj.gameObject.SetActive(false);
                _pool.Push(obj);
            }

            _activeObjects.Clear();
        }
        
        private T CreateNewObject()
        {
            T obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
            return obj;
        }
    }
}