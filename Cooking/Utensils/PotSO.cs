using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pot", menuName = "Utensils/Pot")]
public class PotSO : CookingUtensilSO
{
    public float floatStrength;

    public override void Cook(GameObject food)
    {
        food.GetComponent<FoodView>().Food.IncreaseBoil();

        if (food.GetComponent<FoodView>().Food.cookState == Food.CookState.Boiled)
        {
            Vector3 floatDirection = boiledPosition.position - food.transform.position;
            food.GetComponent<Rigidbody>().AddForce(new Vector3(0, floatDirection.y,0) * floatStrength, ForceMode.Force);
        }
    }
}