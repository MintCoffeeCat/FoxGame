using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[Serializable]
public class MyDictionary : Serialization<int,int>
{
    public MyDictionary() : base(new Dictionary<int, int>()) { }

    public void Add(int k, int v)
    {
        this.target.Add(k, v);
    }
}