using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private string _nextScene;
    [SerializeField] private float _waitTime = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerColorController>(out PlayerColorController player))
        {
            if(player.PlayerColor == PlayerColor.White || player.PlayerColor == PlayerColor.None)
            {
                GameManager.Instance.StageClear(_nextScene, transform.position, _waitTime);
                AudioManager.Instance.PlaySE(AudioManager.SE.Clear);
            }
            else
            {
                GameManager.Instance.SetInfoText("『白』の状態でなければ、先には進めない！", Color.red);
            }
        }
    }
}
