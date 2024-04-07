using UnityEngine;
using Zenject;

public class Factory
{
    protected DiContainer _diContainer;
    protected Config _config;

    public Factory(DiContainer container, Config config)
    {
        _diContainer = container;
        _config = config;
    }

    public T Create<T>(MonoBehaviour monoBehaviour)
    {
        return _diContainer.InstantiatePrefabForComponent<T>(monoBehaviour);
    }
}
