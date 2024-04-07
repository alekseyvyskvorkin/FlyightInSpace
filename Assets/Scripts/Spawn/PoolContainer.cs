using Zenject;
using System.Collections.Generic;

public class PoolContainer
{
    public Ship Ship { get; private set; }

    private Queue<Asteroid> _asteroids = new Queue<Asteroid>();
    private Queue<Bullet> _bullets = new Queue<Bullet>();

    private List<Asteroid> _activatedAsteroids = new List<Asteroid>();
    private List<Bullet> _activatedBullets = new List<Bullet>();

    private Factory _factory;
    private Config _config;

    [Inject]
    public PoolContainer(Config config, Factory factory)
    {
        _config = config;
        _factory = factory;
    }

    public Ship CreateShip()
    {
        Ship = _factory.Create<Ship>(_config.ShipSettings.ShipPrefab);
        return Ship;
    }

    public Asteroid GetAsteroid()
    {
        Asteroid asteroid = null;
        if (_asteroids.Count > 0)
            asteroid = _asteroids.Dequeue();
        else
            asteroid = _factory.Create<Asteroid>(_config.AsteroidSettings.AsteroidPrefab);

        _activatedAsteroids.Add(asteroid);
        return asteroid;
    }

    public Bullet GetBullet()
    {
        Bullet bullet = null;
        if (_bullets.Count > 0)
            bullet = _bullets.Dequeue();
        else
            bullet = _factory.Create<Bullet>(_config.BulletSettings.BulletPrefab);

        _activatedBullets.Add(bullet);
        return bullet;
    }

    public void ReturnAsteroid(Asteroid asteroid)
    {
        _activatedAsteroids.Remove(asteroid);
        _asteroids.Enqueue(asteroid);
    }

    public void ReturnBullet(Bullet bullet)
    {
        _activatedBullets.Remove(bullet);
        _bullets.Enqueue(bullet);
    }

    public void DeactivateAsteroids()
    {        
        var activatedAsteroids = new List<Asteroid>();
        activatedAsteroids.AddRange(_activatedAsteroids);

        foreach (var asteroid in activatedAsteroids)
            asteroid.DeSpawn();
    }

    public void StopBullets()
    {
        var activatedBullets = new List<Bullet>();
        activatedBullets.AddRange(_activatedBullets);

        foreach (var bullet in activatedBullets)
            bullet.gameObject.SetActive(false);
    }
}
