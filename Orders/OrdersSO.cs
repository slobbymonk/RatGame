using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Orders", menuName = "Orders/Order")]
public class OrdersSO : ScriptableObject
{
    //This holds all the ingredients and their icons as well as how big an order can be
    //It would've been better to call this one ingredients and move the maxordesize somewhere else, but oh well (too lazy)
    public enum Ingerdients
    {
        CARROTS_GRILLED,
        CARROTS_BOILED,
        STEAK,
        Fries
    }
    public Ingerdients[] ingredients;
    public Sprite[] icons;

    [Range(1, 3)]
    public int maxOrderSize = 2;
}
