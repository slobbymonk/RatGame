using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cooking;

[CreateAssetMenu(fileName = "Pan", menuName = "Utensils/Pan")]
public class PanSO : CookingUtensilSO
{
    public override void Cook(GameObject food)
    {
        food.GetComponent<FoodView>().Food.IncreaseCook();

        if (food.GetComponent<FoodView>().Food.cookState == Food.CookState.Cooked)
        {
            //Do something to signal when Cooked
        }
        if (food.GetComponent<FoodView>().Food.cookState == Food.CookState.Burnt)
        {
            //Do something to signal when burnt
        }
    }
}
