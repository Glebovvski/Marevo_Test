using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action OnProductClick;
    public void ProductCLick() => OnProductClick?.Invoke();
}
