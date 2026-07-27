using UnityEngine;

public class ColorFilter : MonoBehaviour
{
    [SerializeField] private PlayerColor _targetColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerColorController>(out var playerColorController))
        {
            playerColorController.ChangeColor(_targetColor,false);
        }
    }
}
