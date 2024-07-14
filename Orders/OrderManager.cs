using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static OrdersSO;

public class OrderManager : MonoBehaviour
{
    //Every comment is probably functionality that should be in a different script, but...yeah

    //Order logic
    public int _maxOrders, _ordersAtStart, _maxConcurrentOrders;

    public Vector2 _minMaxDelayBetweenOrders;
    private float _currentDelay, _delayed;

    public OrdersSO _orderTemplate;

    //Orders themselves
    [HideInInspector] public Dictionary<int, List<Ingerdients>> _orderAndIngredients = new Dictionary<int, List<Ingerdients>>();
     public List<int> _completedOrders = new List<int>(), _inProgressOrders = new List<int>();

    //Visuals
    public GameObject v_OrderPrefab;

    private bool _completed;

    private void Awake()
    {
        FillOrdersForGameplayLoop();

        FillCurrentOrders();

        for (int i = 0; i < _inProgressOrders.Count; i++)
        {
            AddOrdersVisual(_inProgressOrders[i]);
        }

        //Get the delay for the next order
        _currentDelay = GetRandomDelay();
    }
    private void LateUpdate()
    {
        CheckForNewOrders();
    }
    private void Update()
    {
        if (_completedOrders.Count >= _maxOrders && !_completed)
        {
            Debug.Log("Orders done");
            _completed = true;
            GameManager.instance.HasCompleted();
        }
    }
    void FillCurrentOrders()
    {
        for (int i = 0; i < _ordersAtStart; i++)
        {
            _inProgressOrders.Add(i);
        }
    }
    public void OrderCompleted(int completedOrder)
    {
        _completedOrders.Add(completedOrder);
    }
    private void CheckForNewOrders()
    {
        //Check if not overflowing with orders
        if(_inProgressOrders.Count < _maxConcurrentOrders)
        {
            //Check if the delay is done
            if(_currentDelay <= _delayed)
            {
                //Get new order that has not been completed yet
                int randomOrder = GetNewRandomOrder();
                _inProgressOrders.Add(randomOrder);
                AddOrdersVisual(randomOrder);
                //Reset delayed
                _delayed = 0;
            }
            else
            {
                //Increase delayed
                _delayed += Time.deltaTime;
            }
        }
    }
    public void MakeRandomOrder()
    {
        var tempListForStoringIngredients = new List<Ingerdients>();
        //Make an order that's a certain size list of ingredients
        for (int i = 0; i < _orderTemplate.maxOrderSize; i++)
        {
            //Get a random ingredient
            OrdersSO.Ingerdients randomIngredient = (Ingerdients)Random.Range(0, _orderTemplate.ingredients.Length);
            //Put random ingredient in list of order ingredients
            tempListForStoringIngredients.Add(randomIngredient);
        }
        //Add ingredients to last order
        _orderAndIngredients.Add(_orderAndIngredients.Count, tempListForStoringIngredients);
        /*//Debug
        Debug.Log("New Order: ");
        foreach (var ingredient in _orderIngredients[_orderIngredients.Count-1])
        {
            Debug.Log(ingredient);
        }*/
    }
    public void AddOrdersVisual(int whatOrderIsThis)
    {
        var newVOrder = Instantiate(v_OrderPrefab, this.transform);
        newVOrder.GetComponent<Order>().ingredients = _orderAndIngredients[whatOrderIsThis];
        newVOrder.GetComponent<Order>().orderIndex = whatOrderIsThis;
    }
    public void RemoveOrdersVisual(int whatOrderToRemove)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Order>().orderIndex == whatOrderToRemove)
            {
                Destroy(transform.GetChild(i).gameObject);
                break;
            }
        }
    }
    private int GetNewRandomOrder()
    {
        int order = 0;
        bool isPossible = false;
        while (!isPossible)
        {
            order = Random.Range(0, _orderAndIngredients.Count);

            if (!_completedOrders.Contains(order)) isPossible = true;
            else isPossible = false;
        }
        return order;
    }
    private float GetRandomDelay()
    {
        return Random.Range(_minMaxDelayBetweenOrders.x, _minMaxDelayBetweenOrders.y);
    }
    private void FillOrdersForGameplayLoop()
    {
        //Instantiate & populate
        for (int i = 0; i < _maxOrders; i++)
        {
            MakeRandomOrder();
        }
        /*for (int i = 0; i < _maxOrders; i++) // From an old system with preset order that were pulled from a database
        {
            int randomOrder = Random.Range(0, _possibleOrders.Length);
            _allOrders.Add(_possibleOrders[randomOrder]);
        }*/
    }
}
