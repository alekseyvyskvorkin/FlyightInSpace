using System.Collections;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, IPoolable, IMoveable, IDamageDealer
{
    private const float LifeTime = 5f;

    public PoolContainer PoolManager { get; set; }
    public float Speed { get; set; }
    public int Damage { get; set; }

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private ParticleSystem _bulletParticles;
    [SerializeField] private ParticleSystem _flashParticles;
    [SerializeField] private ParticleSystem _hitParticles;   

    [Inject]
    private void Initialize(Config config, PoolContainer poolManager)
    {
        Speed = config.BulletSettings.Speed;
        Damage = config.BulletSettings.Damage;
        PoolManager = poolManager;
        UnParentParticles();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<Asteroid>(out var asteroid)) return;

        asteroid.TakeDamage(Damage);

        _hitParticles.transform.position = collision.ClosestPoint(transform.position);
        _hitParticles.Play();

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(DisableRoutine());
    }

    private void OnDisable()
    {
        DeSpawn();
    }

    public void Move(Vector2 direction)
    {
        _rb.velocity = direction * Speed;
    }

    public void DeSpawn()
    {
        PoolManager.ReturnBullet(this);
    }   

    public void OnSpawn()
    {
        gameObject.SetActive(true);

        Move(Vector2.up);
        _bulletParticles.Play();

        _flashParticles.transform.position = transform.position;
        _flashParticles.Play();
    }

    private void UnParentParticles()
    {
        _flashParticles.transform.parent = null;
        _hitParticles.transform.parent = null;
    }

    private IEnumerator DisableRoutine()
    {
        yield return new WaitForSeconds(LifeTime);
        gameObject.SetActive(false);
    }
}
