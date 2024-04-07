using System;
using UnityEngine;
using Zenject;

public class Ship : MonoBehaviour, IMoveable, IHitable, ISpawnable
{
    [field: SerializeField] public Transform SpawnBulletPosition { get; private set; }       

    public Action OnTakeDamage { get; set; }
    public Action OnDie { get; set; }
    public int Health { get; set; }
    public float Speed { get; set; }

    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private SpriteRenderer _shipRenderer;
    [SerializeField] private Collider2D _collider;

    private int _maxHealth { get; set; }
    private Vector2 _minPosition { get; set; }

    [Inject]
    private void Initialize(Config config, ScreenService screenService, UIManager uiService)
    {
        Speed = config.ShipSettings.Speed;
        _maxHealth = config.ShipSettings.Health;
        _minPosition = screenService.MinPlayerPosition;
        
        OnDie += DeSpawn;
        OnDie += uiService.ShowRestartPanel;
        OnTakeDamage += uiService.AddContactCount;
    }

    private void OnDestroy()
    {
        OnDie = null;
        OnTakeDamage = null;
    }

    public void Move(Vector2 direction)
    {
        if (CanMove(direction))
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, Speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        OnTakeDamage?.Invoke();

        if (Health <= 0)
        {
            OnDie?.Invoke();
        }
    }

    private bool CanMove(Vector2 direction)
    {
        return direction.x < 0 && transform.position.x > _minPosition.x || direction.x > 0 && transform.position.x < Mathf.Abs(_minPosition.x);
    }

    public void OnSpawn()
    {
        Health = _maxHealth;
        _shipRenderer.enabled = true;
        _collider.enabled = true;
        transform.position = new Vector2(0, _minPosition.y);
    }

    public void DeSpawn()
    {
        _shipRenderer.enabled = false;
        _collider.enabled = false;
        _explosionParticles.Play();
    }
}
