using System;
using UnityEngine;

public interface IInputService
{
    public Action<Vector2> OnTouchMove { get; set; }
}
