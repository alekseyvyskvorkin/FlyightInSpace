using UnityEngine;
using Zenject;

public class ScreenService : MonoBehaviour
{
    public Vector2 MinPlayerPosition { get; private set; }
    public Vector2 MinSpawnAsteroidPosition { get; private set; }
    public float DisableAsteroidPositionY { get; private set; }

    private Vector3[] _frustumCorners = new Vector3[4];

    private Config _config;
    private Camera _camera;

    [Inject]
    private void Initialize(Config config)
    {
        _camera = Camera.main;
        _config = config;

        CalculateScreenPositions();
    }

    private void CalculateScreenPositions()
    {
        _camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), _camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, _frustumCorners);
        MinPlayerPosition = (Vector2)_camera.transform.TransformVector(_frustumCorners[0]) + _config.ShipSettings.ShipBorderOffset;
        MinSpawnAsteroidPosition = (Vector2)_camera.transform.TransformVector(_frustumCorners[1]) + _config.AsteroidSettings.SpawnOffset;
        DisableAsteroidPositionY = ((Vector2)_camera.transform.TransformVector(_frustumCorners[0]) - _config.AsteroidSettings.SpawnOffset).y;
    }
}
