using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private TextMeshProUGUI _InfoText;
    public static GameManager Instance;
    private ColorWall[] _walls;

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
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _walls = FindObjectsByType<ColorWall>(FindObjectsSortMode.None);
    }

    public void UpdateWallCollision()
    {
        foreach (var wall in _walls)
        {
            wall.CheckCollision(_playerCollider);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _InfoText.text = "GameOver";
    }
}
