using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableList<T>
{
    [SerializeField] List<T> List;

    public List<T> ToList() { return List; }
    public SerializableList(List<T> t) { List = t; }
}
