using UnityEngine;

public class TriggerExitPlaySE : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerColorController>(out var player))
        {
            AudioManager.Instance.PlaySE(AudioManager.SE.WallPass);
        }
    }
}
