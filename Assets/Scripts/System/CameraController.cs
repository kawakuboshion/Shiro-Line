using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] PlayerMove _player;
    private bool _isFollowMode = true;

    private void Start()
    {
        // 起動時にプレイヤーがいなければ1度だけ検索する
        if (_player == null)
        {
            _player = FindFirstObjectByType<PlayerMove>();
        }
    }

    private void Update()
    {
        // _playerが存在し、フォローモードの時だけ追従する
        if (_player != null && _isFollowMode)
        {
            transform.position = _player.transform.position + new Vector3(0, 0, -10);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        // プレイヤーがいない場合はシェイク処理を行わない（エラー防止）
        if (_player == null) yield break;

        _isFollowMode = false;
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        _player.SetCanMove(false);

        while (elapsed < duration)
        {
            // 元の位置をベースにランダムな振動を加える
            transform.position = originalPosition + UnityEngine.Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            Debug.Log(elapsed);
            yield return null; // WaitForEndOfFrameよりnullの方が一般的に推奨されます
        }

        transform.position = originalPosition;
        _isFollowMode = true;

        // 終了時にプレイヤーがまだ存在していれば移動を許可する
        if (_player != null)
        {
            _player.SetCanMove(true);
        }
    }
}