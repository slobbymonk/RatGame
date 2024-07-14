using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public int orderIndex;
    public OrdersSO orderTemplate;

    public Image[] _ingredientSlots;
    [HideInInspector] public List<OrdersSO.Ingerdients> ingredients; 

    private void Start()
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            _ingredientSlots[i].sprite = orderTemplate.icons[(int)ingredients[i]];
        }
    }
}
