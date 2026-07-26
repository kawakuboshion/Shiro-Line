using UnityEngine;

public class ColorInkArea : MonoBehaviour
{
    [SerializeField] private PlayerColor _targetColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerColorController = collision.gameObject.GetComponent<PlayerColorController>();
        if (playerColorController != null)
        {
            playerColorController.ChangeColor(_targetColor);
        }
    }
}
