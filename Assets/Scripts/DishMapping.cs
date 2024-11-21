using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewTileItemMapping", menuName = "ScriptableObjects/DishMapping")]
public class DishMapping : ScriptableObject
{
    public List <GameObject> ingredients;
    public GameObject finalDish;
    public GameObject over;
}
