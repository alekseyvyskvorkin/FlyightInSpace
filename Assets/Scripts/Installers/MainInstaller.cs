using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private Config _config;
    [SerializeField] private InputHandler _inputHandlerPrefab;
    [SerializeField] private UIManager _uiService;
    [SerializeField] private ScreenService _screenService;
    [SerializeField] private Spawner _spawner;    

    public override void InstallBindings()
    {
        BindGameController();
        BindServices();
        BindConfig();
        BindFactories();
        BindPool();
        BindSpawner();
        BindUI();
    }

    private void BindGameController()
    {
        Container.Bind<GameController>().AsSingle();
    }

    private void BindConfig()
    {
        Container.Bind<Config>().FromInstance(_config).AsSingle();
    }

    private void BindServices()
    {
        Container.Bind<IInputService>().To<InputHandler>().FromComponentInNewPrefab(_inputHandlerPrefab).AsSingle();        
        Container.Bind<ScreenService>().FromInstance(_screenService).AsSingle();
    }

    private void BindUI()
    {
        Container.Bind<UIManager>().FromInstance(_uiService).AsSingle();
    }

    private void BindFactories()
    {
        Container.Bind<Factory>().AsSingle();
    }

    private void BindPool()
    {
        Container.Bind<PoolContainer>().AsSingle();
    }

    private void BindSpawner()
    {
        Container.Bind<Spawner>().FromInstance(_spawner).AsSingle();
    }
}
