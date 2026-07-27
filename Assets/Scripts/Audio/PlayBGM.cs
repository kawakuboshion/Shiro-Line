using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    [SerializeField] private AudioManager.BGM _bgm;
    [SerializeField] private bool _startPlayBGM = false;
    private void Start()
    {
        if(_startPlayBGM)
        {
            OnPlayBGM(_bgm);
        }
    }
    public void OnPlayBGM(AudioManager.BGM bgm)
    {
        AudioManager.Instance.PlayBGM(bgm);
    }
}
