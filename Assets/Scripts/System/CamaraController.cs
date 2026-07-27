using UnityEngine;

public class CamaraController : MonoBehaviour
{
    [SerializeField] PlayerMove _player;

    private void Update()
    {
        if(_player != null)
        {
            transform.position = _player.transform.position + new Vector3(0, 0, -10);
        }
        else
        {
            _player = FindFirstObjectByType<PlayerMove>();
        }
    }
}
