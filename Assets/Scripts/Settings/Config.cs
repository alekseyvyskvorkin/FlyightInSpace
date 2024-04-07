using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "Data/Config")]
public class Config : ScriptableObject
{
    [field: SerializeField] public ShipSettings ShipSettings { get; private set; }
    [field: SerializeField] public BulletSettings BulletSettings { get; private set; }
    [field: SerializeField] public AsteroidSettings AsteroidSettings { get; private set; }
}


