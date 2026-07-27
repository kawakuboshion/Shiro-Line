using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("オーディオソース")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _seSource;

    [Header("効果音（SE）の素材")]
    [SerializeField] private AudioClip[] _bgm;

    [Header("効果音（SE）の素材")]
    [SerializeField] private AudioClip[] _se;    

    public enum BGM
    {
        Title,
        StageSelect,
        Stage,
    }
    
    public enum SE
    {
        InkGet,
        WallPass,
        Death,
        Clear,
    }


    void Awake()
    {
        // 他のシーンにいってもこのAudioManagerを破壊せずに使い回す設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 効果音を1回だけ再生する関数
    /// </summary>
    public void PlaySE(SE se)
    {
        var clip = _se[(int)se];
        if (clip != null && _seSource != null)
        {
            _seSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM(BGM bgm)
    {
        var clip = _bgm[(int)bgm];
        if(clip != null && _bgmSource != null)
        {
            _bgmSource.clip = clip;
            _bgmSource.loop = true;
            _bgmSource.Play();
        }
    }

    /// <summary>
    /// BGMの音量を一時的に下げる（死亡時などの演出用）
    /// </summary>
    public void SetBGMVolume(float volume)
    {
        if (_bgmSource != null) _bgmSource.volume = volume;
    }
}
