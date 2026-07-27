using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private ParticleSystem _clearEffect;
    [SerializeField] private ParticleSystem _gameOverEffect;
    [SerializeField] private float _infoClearTime = 2f;
    public static GameManager Instance;
    private ColorWall[] _walls;
    private bool _isCleared = false;
    private bool _isGameOver = false;
    private float _currentInfoClearTime;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        Time.timeScale = 1.0f;
        _walls = FindObjectsByType<ColorWall>(FindObjectsSortMode.None);
        _playerMove.SetCanMove(true);
    }

    private void Update()
    {
        if(_currentInfoClearTime >= _infoClearTime)
        {
            _infoText.text = string.Empty;
        }
        _currentInfoClearTime += Time.deltaTime;
    }

    public void UpdateWallCollision()
    {
        foreach (var wall in _walls)
        {
            wall.CheckCollision(_playerCollider);
        }
    }

    public void StageClear(string nextScene, Vector3 pos, float waitTime = 1f)
    {
        if (_isCleared || _isGameOver) { return; }

        SetInfoText("StageClear",Color.white);
        ParticleSystem effect = Instantiate(_clearEffect,pos,Quaternion.identity) as ParticleSystem;
        effect.Play();
        _isCleared = true;
        _currentInfoClearTime = 0;
        _playerMove.SetCanMove(false);
        StartCoroutine(LoadScene(nextScene, waitTime));
    }

    private IEnumerator LoadScene(string sceneName, float waitTime = 1f)
    {
        var async = SceneManager.LoadSceneAsync(sceneName);

        async.allowSceneActivation = false;
        yield return new WaitForSeconds(waitTime);
        async.allowSceneActivation = true;
    }

    public void SetInfoText(string infoText, Color color)
    {
        _infoText.text = infoText;
        _infoText.color = color;
        _infoText.material.SetColor("_GlowColor", color);
        _currentInfoClearTime = 0;
    }

    public void GameOver(Vector3 pos, Color color, float waitTime = 1f)
    {
        if(_isGameOver || _isCleared) { return; }
        _isGameOver = true;
        _playerMove.SetCanMove(false);
        SetInfoText("GameOver",Color.red);
        ParticleSystem effect = Instantiate(_gameOverEffect, pos, Quaternion.identity) as ParticleSystem;
        ParticleSystem.MainModule mainModule = effect.main;
        mainModule.startColor = color;
        effect.Play();
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name, waitTime));
    }
}
