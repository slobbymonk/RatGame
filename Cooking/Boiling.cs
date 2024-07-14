using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boiling : MonoBehaviour
{
    [SerializeField] private List<GameObject> boilingFood = new List<GameObject>();

    public Transform boiledPosition;
    public float floatStrength;

    private void Update()
    {
        if (boilingFood.Count > 0)
            Boil();
    }
    public void Boil()
    {
        foreach (var food in boilingFood)
        {
            food.GetComponent<FoodView>().Food.IncreaseBoil();

            if(food.GetComponent<FoodView>().Food.cookState == Food.CookState.Boiled)
            {
                Vector3 floatDirection = boiledPosition.position - food.transform.position;
                food.GetComponent<Rigidbody>().AddForce(floatDirection * floatStrength, ForceMode.Force);
            }
        }
        boilingFood.Clear();
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<FoodView>() != null)
        {
            boilingFood.Add(other.gameObject);
        }
    }
}
