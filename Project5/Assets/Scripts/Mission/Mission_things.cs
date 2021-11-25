using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission_things
{
    [SerializeField]
    public string Name;
    
    
    [SerializeField]
    public GameObject[] Objects;
    [SerializeField]
    public Transform Place;
    public bool isCompleted;
}
