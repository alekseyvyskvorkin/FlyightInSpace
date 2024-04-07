using UnityEngine;

[System.Serializable]
public class AsteroidSettings
{
    public Asteroid AsteroidPrefab;
    public Vector2 SpawnOffset = Vector2.up * 3;
    public float Speed = 2f;
    public int Health = 1;
    public int Damage = 1;
    [Range(0.25f, 2f)]
    public float SpawnDelay = 1f;
}
