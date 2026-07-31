using UnityEngine;

public class ColorEngine : MonoBehaviour
{
    [SerializeField] private PlayerColorManager _playerColorManager;
    [SerializeField] private LineRenderer _lineRenderer;
    [Header("対応する色属性")]
    public PlayerColor _requiredColor;

    [Header("起動状態")]
    public bool _isActive = false;

    [Header("起動時に見た目（色）を変える場合の設定")]
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, transform.position);

        // 初期状態の見た目を少し暗め（オフ状態）にしておく
        UpdateAppearance();
    }

    public void SetLinePos(Vector3 pos)
    {
        _lineRenderer.SetPosition(1, pos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突した相手がプレイヤーかチェック
        
        if (collision.TryGetComponent<PlayerColorController>(out var player))
        {
            // プレイヤーの色が、このエンジンが求めている色と【一致】しているか
            if (player.PlayerColor == _requiredColor || player.PlayerColor == PlayerColor.White || player.PlayerColor == PlayerColor.None)
            {
                // エンジンを起動
                _isActive = true;
                UpdateAppearance();

                // 起動音を鳴らす（AudioManagerがある場合）
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySE(AudioManager.SE.WallPass);
                }
            }
        }
    }

    /// <summary>
    /// エンジンの起動状態に合わせて見た目（輝き）を更新する
    /// </summary>
    private void UpdateAppearance()
    {
        if (_spriteRenderer == null) return;

        // プレイヤーのカラーコントローラーから設定色を一時的に借りる、または個別に設定
        Color baseColor = _playerColorManager.GetColor(_requiredColor);

        if (_isActive)
        {
            // 起動時は鮮やかなネオンカラー（HDR）にする
            _spriteRenderer.color = baseColor;
            _lineRenderer.startColor = baseColor;
            _lineRenderer.endColor = baseColor;
        }
        else
        {
            // 未起動時は暗めの色にしておく
            _spriteRenderer.color = baseColor * 0.4f;
            _spriteRenderer.color += new Color(0, 0, 0, 1f);
            _lineRenderer.startColor = baseColor * 0.4f;
            _lineRenderer.endColor = baseColor * 0.4f;
        }
    }
}
