using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TextCore.Text;

public class FoodView : MonoBehaviour
{
    public Food Food { get; private set; }

    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _boiledMaterial, _cookedMaterial, _burntMaterial;

    //[SerializeField] private float whenBoiled, whenCooked, whenBurned;

    public FoodScriptableObject foodTypeSO;

    public OrdersSO.Ingerdients ingredientType;

    public void Awake()
    {
        Food = new Food(foodTypeSO.whenBoiled, foodTypeSO.whenCooked, foodTypeSO.whenBurned);

        Assert.IsNotNull(_renderer);
        Assert.IsNotNull(_boiledMaterial);
        Assert.IsNotNull(_cookedMaterial);
        Assert.IsNotNull(_burntMaterial);

        Food.ChangedActiveState += UpdateVisuals;
    }
    private void Update()
    {
        Food.Update();
    }
    private void UpdateVisuals(object sender, EventArgs e)
    {
        if (Food.cookState == Food.CookState.Boiled)
            _renderer.material = _boiledMaterial;
        else if(Food.cookState == Food.CookState.Cooked)
            _renderer.material = _cookedMaterial;
        else if(Food.cookState == Food.CookState.Burnt)
            _renderer.material = _burntMaterial;
    }
}
