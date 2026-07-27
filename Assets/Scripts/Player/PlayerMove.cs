using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [Header("移動の設定")]
    [SerializeField] private float _moveSpeed = 15f;     // 移動の最高速度
    [SerializeField] private float _acceleration = 5f;  // 加速度（高いほどすぐ最高速になる）
    [SerializeField] private float _deceleration = 2f;  // 減速度（指を離したときの滑り具合。高いほどすぐ止まる）
    [SerializeField] private float _burstMultiply = 2f; //バーストモードになったときに最高速度と加速度にかける値
    [SerializeField] private float _burstTime = 5.0f; //バーストモードの継続時間

    private Rigidbody2D _rb;
    private Vector2 _velocity;
    private Vector2 _dragStartPos;
    private Vector2 _currentInputDirection;
    private float _currentBurstTime;
    private bool _isDragging = false;
    private bool _isBurstMode = false;
    private bool _canMove = true;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // プレイヤーが勝手に回転しないように固定する
        _rb.freezeRotation = true;
        // 2Dアクション特有の重力をこのオブジェクトだけ0にする（宇宙空間のような浮遊移動のため）
        _rb.gravityScale = 0f;
    }

    void Update()
    {
        HandleInput();

        if(_currentBurstTime >= _burstTime)
        {
            EndBurstMode();
        }
        _currentBurstTime += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (!_canMove) 
        { 
            _rb.linearVelocity = Vector2.zero;  
            return; 
        }
        MovePlayer();
    }

    /// <summary>
    /// マウスドラッグ / スマホスワイプの入力を検知する
    /// </summary>
    private void HandleInput()
    {
        // クリック / タッチされた瞬間
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _dragStartPos = Input.mousePosition;
        }

        // ドラッグ中（指を動かしている間）
        if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector2 dragCurrentPos = Input.mousePosition;
            Vector2 dragVector = dragCurrentPos - _dragStartPos;

            // ドラッグされた距離が一定以上あれば方向を計算
            if (dragVector.magnitude > 10f)
            {
                _currentInputDirection = dragVector.normalized;
            }
        }

        // 指を離した瞬間
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _currentInputDirection = Vector2.zero;
        }
    }

    /// <summary>
    /// Rigidbody2Dを使って慣性のある移動処理を行う
    /// </summary>
    private void MovePlayer()
    {
        if (_isDragging && _currentInputDirection != Vector2.zero)
        {
            // 目標とする速度を計算
            Vector2 targetVelocity = _currentInputDirection * _moveSpeed;
            // 現在の速度から目標の速度へ、加速度（acceleration）に応じて滑らかに近づける
            _velocity = Vector2.Lerp(_rb.linearVelocity, targetVelocity, _acceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = _velocity;
        }
        else
        {
            // 指が離されたら、減速度（deceleration）に応じてじわっと停止させる
            _velocity = Vector2.Lerp(_rb.linearVelocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
            _rb.linearVelocity = _velocity;
        }
    }

    public void StartBurstMode()
    {
        if (!_isBurstMode)
        {
            _isBurstMode = true;
            _moveSpeed *= _burstMultiply;
            _acceleration *= _burstMultiply;
            _currentBurstTime = 0;
        }
    }

    public void SetCanMove(bool canMove) { _canMove = canMove; }

    private void EndBurstMode()
    {
        if(_isBurstMode)
        {
            _isBurstMode = false;
            _moveSpeed /= _burstMultiply;
            _acceleration /= _burstMultiply;
        }
    }
}
