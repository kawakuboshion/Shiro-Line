using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _stageNameText;
    [SerializeField] private LoadScene _loadScene;
    [SerializeField] private string _stageName;
    [SerializeField] private string _sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMove player))
        {
            AudioManager.Instance.PlaySE(AudioManager.SE.Clear);
            _canvas.gameObject.SetActive(true);
            _stageNameText.text = _stageName + "に入りますか？";
            _startButton.onClick.RemoveAllListeners();
            _startButton.onClick.AddListener(() => _loadScene.OnLoadScene(_sceneName));
            player.SetCanMove(false);
        }
    }
}
