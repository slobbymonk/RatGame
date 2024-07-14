using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CookingUtensilSO : ScriptableObject
{
    public Transform boiledPosition;
    public abstract void Cook(GameObject foodToCook);
}
