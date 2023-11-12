using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensions
{
    public static Vector2 GetScreenWorldSize()
    {
    	return Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}
