using Zenject;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    private PoolContainer _poolContainer;
    private ScreenService _screenService;
    private Config _config;

    private Vector2 _minSpawnPosition { get; set; }

    [Inject]
    private void Initialize(PoolContainer poolContainer, Config config, ScreenService screenService)
    {
        _poolContainer = poolContainer;
        _config = config;
        _screenService = screenService;
    }

    private void Start()
    {
        _minSpawnPosition = _screenService.MinSpawnAsteroidPosition;

        CreateShip();
    }    

    public void StartSpawn()
    {
        StartCoroutine(SpawnAsteroidsRoutine());
        StartCoroutine(SpawnBulletsRoutine());
    }

    private void CreateShip()
    {
        var ship = _poolContainer.CreateShip();
        ship.OnSpawn();
        ship.OnDie += StopSpawn;
        ship.OnDie += _poolContainer.StopBullets;
    }

    private IEnumerator SpawnAsteroidsRoutine()
    {
        yield return new WaitForSeconds(_config.AsteroidSettings.SpawnDelay);
        var asteroid = _poolContainer.GetAsteroid();
        asteroid.transform.position = new Vector2(Random.Range(_minSpawnPosition.x, Mathf.Abs(_minSpawnPosition.x)), _minSpawnPosition.y);
        asteroid.OnSpawn();
        StartCoroutine(SpawnAsteroidsRoutine());
    }

    private IEnumerator SpawnBulletsRoutine()
    {
        yield return new WaitForSeconds(_config.BulletSettings.ShootDelay);
        var bullet = _poolContainer.GetBullet();
        bullet.transform.position = _poolContainer.Ship.SpawnBulletPosition.position;
        bullet.OnSpawn();
        StartCoroutine(SpawnBulletsRoutine());
    }

    private void StopSpawn()
    {
        StopAllCoroutines();
    }
}
