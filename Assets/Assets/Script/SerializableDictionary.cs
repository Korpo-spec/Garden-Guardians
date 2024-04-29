using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [Serializable]
    private struct SerializablePair<K, V>
    {
        public K Key;
        public V Value;

        public SerializablePair(K Key, V Value)
        {
            this.Key = Key;
            this.Value = Value;
        }

        public override string ToString()
        {
            return $"<{Key},{Value}>";
        }
    }

    //Serialized list of keyvalue pairs used to initialize dictionary
    [SerializeField]
    private List<SerializablePair<TKey, TValue>> KeyValuePairs = new List<SerializablePair<TKey, TValue>>();

#if UNITY_EDITOR
    [Tooltip("Enable this when adding new keys as adding new elements to the pair list will create duplicate keys. Disable to enable serialization.")]
    [SerializeField]
    public bool editorSerialize;
#endif

    public void OnAfterDeserialize()
    {
#if UNITY_EDITOR
        if (editorSerialize)
        {
            return;
        }
#endif
        Clear();
        for (int i = 0; i < KeyValuePairs.Count; ++i)
        {
            var key = KeyValuePairs[i].Key;
            if (ContainsKey(key))
            {
                continue;
            }
            Add(key, KeyValuePairs[i].Value);
        }
    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (editorSerialize)
        {
            return;
        }
#endif
        KeyValuePairs.Clear();
        foreach (var pair in this)
        {
            KeyValuePairs.Add(new SerializablePair<TKey, TValue>(pair.Key, pair.Value));
        }
    }
}
