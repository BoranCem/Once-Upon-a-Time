
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Settings Panel")]
    public GameObject settingsPanel;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Audio Sources")]
    public AudioSource[] musicSources;
    public AudioSource[] sfxSources;

    private const string MusicKey = "MusicVolume";
    private const string SFXKey = "SFXVolume";

    private void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicKey, 0.3f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXKey, 1f);

        musicSlider.SetValueWithoutNotify(musicVolume);
        sfxSlider.SetValueWithoutNotify(sfxVolume);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        ApplyVolume(musicSources, musicVolume);
        ApplyVolume(sfxSources, sfxVolume);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    private void SetMusicVolume(float volume)
    {
        ApplyVolume(musicSources, volume);

        PlayerPrefs.SetFloat(MusicKey, volume);
        PlayerPrefs.Save();
    }

    private void SetSFXVolume(float volume)
    {
        ApplyVolume(sfxSources, volume);

        PlayerPrefs.SetFloat(SFXKey, volume);
        PlayerPrefs.Save();
    }

    private void ApplyVolume(AudioSource[] sources, float volume)
    {
        if (sources == null)
            return;

        foreach (AudioSource source in sources)
        {
            if (source != null)
                source.volume = volume;
        }
    }
}
