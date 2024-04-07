using UnityEngine;

[System.Serializable]
public class BulletSettings
{
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; } = 1;

    [Range(0.2f, 1f)]
    public float ShootDelay = 0.5f;
}


