using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    public Transform boiledPosition;
    public CookingUtensilSO utensil;
    private List<GameObject> foodInUtensil = new List<GameObject>();

    public GameObject cookingEffect;
    public GameObject burntEffect;
    public GameObject cookedEffect;

    private void Awake()
    {
        if(boiledPosition != null)
            utensil.boiledPosition = boiledPosition;
    }

    private void Update()
    {
        if (foodInUtensil.Count > 0)
        {
            cookingEffect?.SetActive(true);
            Cook();
        }
        else
        {
            cookingEffect?.SetActive(false);
            burntEffect?.SetActive(false);
            cookedEffect?.SetActive(false);
        }
    }
    void Cook()
    {
        var itemsToBeRemoved = new List<GameObject>();
        foreach (var food in foodInUtensil)
        {
            utensil.Cook(food);
            //Put food in to be removed list if it's been picked up
            if (!food.GetComponent<Collider>().enabled)
                itemsToBeRemoved.Add(food);

            //Effects
            if (food.GetComponent<FoodView>().Food.cookState == Food.CookState.Burnt)
            {
                burntEffect?.SetActive(true);
            }
            else if (food.GetComponent<FoodView>().Food.cookState == Food.CookState.Cooked)
            {
                cookedEffect?.SetActive(true);
            }
            else
            {
                burntEffect?.SetActive(false);
                cookedEffect?.SetActive(false);
            }
        }
        //Remove it after the foreach as to not disturb it
        foreach (var item in itemsToBeRemoved)
        {
            foodInUtensil.Remove(item);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FoodView>() != null && !foodInUtensil.Contains(other.gameObject))
        {
            foodInUtensil.Add(other.gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (foodInUtensil.Contains(other.gameObject))
        {
            foodInUtensil.Remove(other.gameObject);
        }
    }
}
