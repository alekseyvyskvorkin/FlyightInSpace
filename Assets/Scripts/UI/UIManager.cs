using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _gamePlayPanel;
    [SerializeField] private GameObject _pausePanel;

    [SerializeField] private Button _startPlayButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private TMP_Text _contactCountText;
    
    private GameController _gameController;

    [Inject]
    private void Initialize(GameController gameController)
    {
        _gameController = gameController;
    }

    private void Start()
    {
        InitializeButtons();
    }

    public void ShowRestartPanel()
    {
        _gameController.LoseGame();

        _pausePanel.SetActive(true);
        _restartButton.gameObject.SetActive(true);
        _gamePlayPanel.SetActive(false);
        _continueButton.gameObject.SetActive(false);
    }

    public void AddContactCount()
    {
        _contactCountText.text = (int.Parse(_contactCountText.text) + 1).ToString();
        _contactCountText.transform.DORewind();
        _contactCountText.transform.DOPunchScale(Vector3.one, 0.3f);        
    }

    private void InitializeButtons()
    {
        _startPlayButton.onClick.AddListener(() => OnStartGame());
        _pauseButton.onClick.AddListener(() => OnPauseButtonClick());
        _continueButton.onClick.AddListener(() => OnContinueButtonClick());
        _restartButton.onClick.AddListener(() => OnStartGame());
        _quitButton.onClick.AddListener(() => Application.Quit());
    }

    private void OnContinueButtonClick()
    {
        _gameController.UnPauseGame();

        _pausePanel.SetActive(false);
        _gamePlayPanel.SetActive(true);
    }

    private void OnPauseButtonClick()
    {
        _gameController.PauseGame();

        _pausePanel.SetActive(true);
        _continueButton.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(false);
        _gamePlayPanel.SetActive(false);
        _restartButton.gameObject.SetActive(false);
    }

    private void OnStartGame()
    {
        _gameController.StartPlay();

        ResetCountText();

        _gamePlayPanel.SetActive(true);
        _pausePanel.gameObject.SetActive(false);
        _startPlayButton.gameObject.SetActive(false);
    }

    private void ResetCountText() => _contactCountText.text = "0";
}
