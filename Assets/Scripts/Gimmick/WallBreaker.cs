using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WallBreaker : MonoBehaviour
{
    [SerializeField] public bool _IsActive = false;
    [SerializeField] private Rigidbody2D _rb;

    public void AddForce(Vector2 force)
    {
        _rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_IsActive && collision.gameObject.TryGetComponent(out ColorWall wall))
        {
            // 修正前: StartCoroutine(...) 自分のオブジェクトで実行していた
            // 修正後: カメラのコンポーネント経由で、カメラ自身にコルーチンを実行させる
            var cameraController = Camera.main.GetComponent<CameraController>();
            if (cameraController != null)
            {
                cameraController.StartCoroutine(cameraController.Shake(0.3f, 0.5f));
            }

            Destroy(wall.gameObject);
            Destroy(gameObject); // これで自分が消えても、カメラのコルーチンは止まりません
        }
    }
}
