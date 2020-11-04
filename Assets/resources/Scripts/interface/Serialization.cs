﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]

public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    protected List<TKey> keys;
    [SerializeField]
    protected List<TValue> values;

    protected Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {

        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }
    }
}
