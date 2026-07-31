using System;
using UnityEngine;

public class PlayerColorController : MonoBehaviour
{
    [SerializeField] private PlayerColorManager _playerColorManager;
    [SerializeField] private PlayerColor _playerColor;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private float _trailEndAlpha;
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
            GameManager.Instance.GameOver(transform.position, _playerColorManager.GetColor(_playerColor));
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
        if(_playerColor == PlayerColor.White || _playerColor == PlayerColor.None)
        {
            _playerMove.StartBurstMode();
        }
        else
        {
            _playerMove.EndBurstMode();
        }
        GameManager.Instance.UpdateWallCollision();
        Color newColor = _playerColorManager.GetColor(_playerColor);
        _spriteRenderer.color = newColor;
        _trailRenderer.startColor = newColor;
        Color end = newColor;
        end.a = _trailEndAlpha;
        _trailRenderer.endColor = end;
    }
}
