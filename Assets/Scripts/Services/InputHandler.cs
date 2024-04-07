using System;
using UnityEngine;

public class InputHandler : MonoBehaviour, IInputService
{
    public Action<Vector2> OnTouchMove { get; set; }

    private float _halfScreenSize;

    private void Start()
    {
        _halfScreenSize = Screen.width / 2;
    }

    private void OnDestroy()
    {
        OnTouchMove = null;
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        if (Input.mousePosition.x < _halfScreenSize)
            OnTouchMove?.Invoke(Vector2.left);
        else
            OnTouchMove?.Invoke(Vector2.right);
    }
}
