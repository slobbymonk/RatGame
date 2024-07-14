using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
    public float boiledNess, cookedNess;
    public float whenBoiled, whenCooked, whenBurned;

    public CookState cookState;

    public Food(float whenBoiled, float whenCooked, float whenBurned)
    {
        this.whenBoiled = whenBoiled;
        this.whenCooked = whenCooked;
        this.whenBurned = whenBurned;
    }

    public event EventHandler ChangedActiveState;

    public enum CookState
    {
        Raw,
        Boiled,
        Cooked,
        Burnt
    }

    public void Update()
    {
        switch (cookState)
        {
            case CookState.Raw:
                if(boiledNess > whenBoiled)
                {
                    cookState = CookState.Boiled; ChangedActiveState?.Invoke(this, EventArgs.Empty);//Set state & then change visuals
                }
                if (cookedNess > whenCooked)
                {
                    cookState = CookState.Cooked; ChangedActiveState?.Invoke(this, EventArgs.Empty);//Set state & then change visuals
                }
                break;
            case CookState.Cooked:
                if (cookedNess > whenBurned)
                { 
                    cookState = CookState.Burnt; ChangedActiveState?.Invoke(this, EventArgs.Empty); //Set state & then change visuals
                }
                break;
        }
    }

    public void IncreaseBoil()
    {
        boiledNess += Time.deltaTime;
    }
    public void IncreaseCook()
    {
        cookedNess += Time.deltaTime;
    }
}
