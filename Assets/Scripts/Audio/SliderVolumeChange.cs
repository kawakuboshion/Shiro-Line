using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderVolumeChange : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private string volumeName;
    private void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener((value) =>
            {
                // valueは0～1の値を期待する。それを保証するための処理
                value = Mathf.Clamp01(value);

                float decibel = 20f * Mathf.Log10(value);
                decibel = Mathf.Clamp(decibel, -80f, 0f);
                audioMixer.SetFloat(volumeName, decibel);

                Debug.Log($"Changed: {volumeName},{decibel}");
            });
        }
    }
}
