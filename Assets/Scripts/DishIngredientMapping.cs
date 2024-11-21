using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTileItemMapping", menuName = "ScriptableObjects/DishIngredientMapping")]
public class DishIngredientMapping : ScriptableObject
{
    public GameObject finalDish;
    public GameObject[] ingredients;
}
