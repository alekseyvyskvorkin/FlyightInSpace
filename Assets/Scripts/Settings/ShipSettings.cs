using UnityEngine;

[System.Serializable]
public class ShipSettings
{
    public Ship ShipPrefab;
    public float Speed = 2f;
    public int Health = 10;
    public Vector2 ShipBorderOffset = new Vector2(0.5f, 1f);
}


