using UnityEngine;

public class ColorWall : MonoBehaviour
{
    [SerializeField] private PlayerColor _wallColor;
    [SerializeField] private Collider2D _wallCollider;

    void Start()
    {
        _wallCollider = GetComponent<Collider2D>();
    }

    public void CheckCollision(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerColorController>(out var player))
        {
            var playerColor = player.PlayerColor;
            if (playerColor == _wallColor || playerColor == PlayerColor.White || playerColor == PlayerColor.None)
            {
                // 衝突判定を「無視する（すり抜ける）」に設定
                Physics2D.IgnoreCollision(collision, _wallCollider, true);
            }
            else
            {
                // 衝突判定を「有効にする（ぶつかる）」に設定
                Physics2D.IgnoreCollision(collision, _wallCollider, false);
            }
        }
    }
}
