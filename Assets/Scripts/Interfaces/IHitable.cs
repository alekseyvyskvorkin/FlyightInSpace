using System;

public interface IHitable
{
    public Action OnDie { get; set; }
    public int Health { get; set; }

    public void TakeDamage(int damage);
}
