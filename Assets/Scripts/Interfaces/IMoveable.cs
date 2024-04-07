using UnityEngine;

public interface IMoveable
{
    public float Speed { get; set; }
    public void Move(Vector2 direction);
}
