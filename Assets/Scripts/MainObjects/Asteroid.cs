using System;
using UnityEngine;
using Zenject;

public class Asteroid : MonoBehaviour, IMoveable, IHitable, IDamageDealer, IPoolable
{
    public PoolContainer PoolManager { get; set; }
    public Action OnDie { get; set; }

    public int Health { get; set; }
    public int Damage { get; set; }
    public float Speed { get; set; }

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private Rigidbody2D _rb;

    private int _maxHealth { get; set; }
    private float _endPositionY { get; set; }   

    [Inject]
    private void Initialize(Config config, PoolContainer poolManager, ScreenService screenService)
    {
        PoolManager = poolManager;

        _maxHealth = config.AsteroidSettings.Health;
        Damage = config.AsteroidSettings.Damage;
        Speed = config.AsteroidSettings.Speed;
        _endPositionY = screenService.DisableAsteroidPositionY;

        _explosionParticles.transform.parent = null;

        OnDie += DeSpawn;
        OnDie += PlayParticles;
    }

    private void OnDestroy()
    {
        OnDie -= DeSpawn;
        OnDie -= PlayParticles;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Ship>(out var ship)) return;

        ship.TakeDamage(Damage);
        OnDie?.Invoke();
    }

    private void Update()
    {
        if (transform.position.y > _endPositionY) return;

        DeSpawn();
    }

    public void Move(Vector2 direction) => _rb.velocity = direction * Speed;

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            OnDie?.Invoke();
        }
    }    

    public void OnSpawn()
    {
        gameObject.SetActive(true);
        Health = _maxHealth;
        Move(Vector2.down);
    }

    public void DeSpawn()
    {
        PoolManager.ReturnAsteroid(this);
        gameObject.SetActive(false);
    }

    private void PlayParticles()
    {
        _explosionParticles.transform.position = transform.position;
        _explosionParticles.Play();
    }
}
