using System.Collections.Generic;
using Script.Entity;
using UnityEngine;


namespace Script
{
    [CreateAssetMenu(menuName = "Custom/data/TransformHealthDictionary")]
    public class TransformHealthDictionary : ScriptableObject
    {
        private readonly Dictionary<Transform, EntityHealth> _dictionary = new Dictionary<Transform, EntityHealth>();

        public void Clear()
        {
            _dictionary.Clear();
        }
        public void Add(Transform transform, EntityHealth entityHealth)
        {
            _dictionary.Add(transform, entityHealth);
            
        }

        public void Remove(Transform transform)
        {
            if (!_dictionary.Remove(transform))
                return;
        }

        public bool TryGetHealth(Transform transform, out EntityHealth health)
        {
            return _dictionary.TryGetValue(transform, out health);
        }

        public bool Contains(Transform transform)
        {
            return _dictionary.ContainsKey(transform);
        }

        public bool IsEmpty()
        {
            return _dictionary.Count == 0;
        }
        
    }
}