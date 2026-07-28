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
        if(_IsActive && collision.gameObject.TryGetComponent(out ColorWall wall))
        {
            Destroy(wall.gameObject);
        }
    }
}
