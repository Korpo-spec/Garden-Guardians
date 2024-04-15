using UnityEngine;

namespace Script.PlayerMovement.SO
{
    public interface IResetable<T> where T : ScriptableObject
    {
        public T originalInstance { get; set; }
        public T Reset(T currentInstance)
        {
            if (originalInstance == null)
            {
                originalInstance = currentInstance;
            }
            T instance = ScriptableObject.CreateInstance<T>();
            return instance;
        }
    }
}