//작성자 : 주현해

//초기 볼륨 관련 코드 일부 수정 by 정현우
//(PlayerPrefs 추가해서 오디오 사운드 볼륨 설정값 저장) by 정현우

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Start()
    {
        // PlayerPrefs에서 저장된 값을 불러와 슬라이더에 설정
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            _musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey);
            audioManager.instance.MusicVolume(_musicSlider.value); // 초기 볼륨 설정
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            _sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey);
            audioManager.instance.SFXVolume(_sfxSlider.value); // 초기 볼륨 설정
        }
    }

    public void ToggleMusic()
    {
        audioManager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        audioManager.instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        audioManager.instance.MusicVolume(_musicSlider.value);
        PlayerPrefs.SetFloat(MusicVolumeKey, _musicSlider.value); // 슬라이더 값을 저장
    }

    public void SFXVolume()
    {
        audioManager.instance.SFXVolume(_sfxSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, _sfxSlider.value); // 슬라이더 값을 저장
    }

    private void OnDisable()
    {
        // 씬이 전환될 때 PlayerPrefs에 저장
        PlayerPrefs.SetFloat(MusicVolumeKey, _musicSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, _sfxSlider.value);
        PlayerPrefs.Save(); // 저장
    }

    private void OnApplicationQuit()
    {
        // 애플리케이션 종료 시 PlayerPrefs에 저장
        PlayerPrefs.SetFloat(MusicVolumeKey, _musicSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, _sfxSlider.value);
        PlayerPrefs.Save(); // 저장
    }
}