using UnityEngine;

public class ColorInkArea : MonoBehaviour
{
    [SerializeField] private PlayerColor _targetColor;
    [SerializeField] private int _useLimit = 3;
    [SerializeField] private bool _isInfinite = true;
    private int _useCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerColorController>(out var playerColorController))
        {
            if(_isInfinite || _useCount < _useLimit)
            {
                playerColorController.ChangeColor(_targetColor, true);
            }
        }
    }
}
