using UnityEngine;
using Firebase.Analytics;

public class GameController
{
    private Spawner _spawner;
    private IInputService _inputService;
    private PoolContainer _poolContainer;

    public GameController(Spawner spawner, IInputService inputService, PoolContainer pool)
    {
        _spawner = spawner;
        _inputService = inputService;
        _poolContainer = pool;
        PauseGame();
    }

    public void StartPlay()
    {
        UnPauseGame();
        _inputService.OnTouchMove += _poolContainer.Ship.Move;
        _poolContainer.DeactivateAsteroids();
        _poolContainer.Ship.OnSpawn();
        _spawner.StartSpawn();

        FirebaseAnalytics.LogEvent("StartPlay");
    }

    public void LoseGame()
    {
        PauseGame();
        _inputService.OnTouchMove -= _poolContainer.Ship.Move;

        FirebaseAnalytics.LogEvent("LoseGame");
    }

    public void PauseGame() => Time.timeScale = 0;

    public void UnPauseGame() => Time.timeScale = 1;
}
