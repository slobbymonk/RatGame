using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Review;

public class DeliveryStation : MonoBehaviour
{
    public OrderManager orderManager;
    //Make and initialize list for ingredients found on plate
    public List<OrdersSO.Ingerdients> plateIngredients = new List<OrdersSO.Ingerdients>();

    public ReviewManager _reviewManager;

    private int[] _foodCritics;

    private void Awake()
    {
        _foodCritics = PopulateArray(3, 0, orderManager._maxOrders-2);

        Debug.Log("Populated Array: " + string.Join(", ", _foodCritics));
    }
    public int[] PopulateArray(int size, int min, int max)
    {
        int[] result = new int[size];
        List<int> availableNumbers = new List<int>();

        // Populate the list with numbers from min to max
        for (int i = min; i <= max; i++)
        {
            availableNumbers.Add(i);
        }

        // Populate the array with unique random numbers
        for (int i = 0; i < size; i++)
        {
            int randomIndex = Random.Range(0, availableNumbers.Count);
            result[i] = availableNumbers[randomIndex];
            availableNumbers.RemoveAt(randomIndex);
        }

        return result;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Plate"))
        {
            plateIngredients.Clear();
            //Populate this list
            GetAllIngredientsOnPlate(collision);


            //Gets all currently opened orders and their ingredients
            foreach (var order in orderManager._inProgressOrders)
            {
                Debug.Log("Order is being checked");
                //Gets the ingredients from the order we have selected
                var orderIngredientList = orderManager._orderAndIngredients[order];

                //First check if plate doesn't have too many or too little ingredients
                if (orderIngredientList.Count != plateIngredients.Count)
                {
                    OrderCompleted(Experiences.Incomplete, order, collision.gameObject);
                    Debug.Log("Order Incomplete");
                    return;
                }
                else
                {
                    bool isBurnt = false;
                    bool isRaw = false;
                    int correctIngredients = 0;
                    //Compare the ingredients in the currently selected available order and the ones on the plate
                    foreach (var ingredient in orderIngredientList)
                    {
                        foreach (var plateIngredient in plateIngredients)
                        {
                            if(ingredient == plateIngredient)
                                correctIngredients++;
                        }
                    }
                    if (correctIngredients >= orderIngredientList.Count)
                    {
                        //Check if food is not up to par
                        for (int i = 0; i < collision.transform.childCount; i++)
                        {
                            if (collision.transform.GetChild(i).gameObject.GetComponent<FoodView>().Food.cookState == Food.CookState.Burnt)
                                isBurnt = true;
                            if (collision.transform.GetChild(i).gameObject.GetComponent<FoodView>().Food.cookState == Food.CookState.Raw)
                                isRaw = true;
                        }

                        //Check if burnt
                        if (!isBurnt)
                            OrderCompleted(Experiences.Perfect, order, collision.gameObject);
                        else if(isRaw)
                            OrderCompleted(Experiences.Raw, order, collision.gameObject);
                        else
                            OrderCompleted(Experiences.Burnt, order, collision.gameObject);
                        break;
                    }
                    else
                    {
                        OrderCompleted(Experiences.Incomplete, order, collision.gameObject);
                    }
                }
            }


        }
    }
    private void OrderCompleted(Experiences experience, int completedOrder, GameObject plate)
    {
        Destroy(plate);
        foreach (var critic in _foodCritics)
        {
            if (orderManager._completedOrders.Count == critic)
                _reviewManager.AddReview(experience);
        }
        orderManager.RemoveOrdersVisual(completedOrder);
        orderManager._completedOrders.Add(completedOrder);
        Debug.Log("Order Complete");
    }

    private void GetAllIngredientsOnPlate(Collider collision)
    {
        foreach (Transform child in collision.transform)
        {
            plateIngredients.Add(child.gameObject.GetComponent<FoodView>().ingredientType);
        }
    }
}
