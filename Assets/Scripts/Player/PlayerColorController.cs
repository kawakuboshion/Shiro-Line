using System;
using UnityEngine;

public class PlayerColorController : MonoBehaviour
{
    [SerializeField] private PlayerColor _playerColor;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _trailEndAlpha;
    [SerializeField]
    private Color[] _colors = new Color[]
    {
        Color.white, Color.red, Color.blue, Color.magenta, Color.green, Color.yellow, Color.cyan, Color.white, 
    };
    [SerializeField] private float _trailStartAlpha;

    public PlayerColor PlayerColor { get { return _playerColor; } set { ChangeColor(value); } }

    private void Start()
    {
        ChangeColor(PlayerColor, true);
    }

    public void ChangeColor(PlayerColor color, bool plus = true)
    {
        if(color == PlayerColor.Black)
        {
            GameManager.Instance.GameOver(transform.position, _colors[(int)_playerColor]);
            AudioManager.Instance.PlaySE(AudioManager.SE.Death);
            Destroy(gameObject);
            return;
        }
        AudioManager.Instance.PlaySE(AudioManager.SE.InkGet);
        if(plus)
        {
            _playerColor = _playerColor == PlayerColor.White ? color : PlayerColor | color;
        }
        else
        {
            _playerColor &= ~color;
        }
        if(_playerColor == PlayerColor.White)
        {
            _playerMove.StartBurstMode();
        }
        GameManager.Instance.UpdateWallCollision();
        Color newColor = _colors[(int)_playerColor];
        _spriteRenderer.color = newColor;
        _trailRenderer.startColor = newColor;
        Color end = newColor;
        end.a = _trailEndAlpha;
        _trailRenderer.endColor = end;
    }
}

[Flags]
public enum PlayerColor:uint
{
    Red = 1 << 0,//1
    Blue = 1 << 1,//2
    Green = 1 << 2,//4

    //混色
    Magenta = Red | Blue,//3
    Yellow = Red | Green,//5
    Cyan = Blue | Green,//6
    White = Red | Blue | Green,//7

    Black = 1 << 30,
}
