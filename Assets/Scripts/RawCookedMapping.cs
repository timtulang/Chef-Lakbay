using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewTileItemMapping", menuName = "ScriptableObjects/RawCookedMapping")]
public class RawCookedMapping : ScriptableObject
{
    public GameObject raw;
    public GameObject processed;
    public GameObject over;
}
