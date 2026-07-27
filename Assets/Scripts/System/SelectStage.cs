using TMPro;
using UnityEngine;

public class SelectStage : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _stageNameText;
    [SerializeField] private string _stageName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMove player))
        {
            AudioManager.Instance.PlaySE(AudioManager.SE.Clear);
            _canvas.gameObject.SetActive(true);
            _stageNameText.text = _stageName + "に入りますか？";
            player.SetCanMove(false);
        }
    }
}
