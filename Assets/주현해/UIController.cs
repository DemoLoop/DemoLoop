//�ۼ��� : ������

//�ʱ� ���� ���� �ڵ� �Ϻ� ���� by ������
//(PlayerPrefs �߰��ؼ� ����� ���� ���� ������ ����) by ������

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
        // PlayerPrefs���� ����� ���� �ҷ��� �����̴��� ����
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            _musicSlider.value = PlayerPrefs.GetFloat(MusicVolumeKey);
            audioManager.instance.MusicVolume(_musicSlider.value); // �ʱ� ���� ����
        }

        if (PlayerPrefs.HasKey(SFXVolumeKey))
        {
            _sfxSlider.value = PlayerPrefs.GetFloat(SFXVolumeKey);
            audioManager.instance.SFXVolume(_sfxSlider.value); // �ʱ� ���� ����
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
        PlayerPrefs.SetFloat(MusicVolumeKey, _musicSlider.value); // �����̴� ���� ����
    }

    public void SFXVolume()
    {
        audioManager.instance.SFXVolume(_sfxSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, _sfxSlider.value); // �����̴� ���� ����
    }

    private void OnDisable()
    {
        // ���� ��ȯ�� �� PlayerPrefs�� ����
        PlayerPrefs.SetFloat(MusicVolumeKey, _musicSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, _sfxSlider.value);
        PlayerPrefs.Save(); // ����
    }

    private void OnApplicationQuit()
    {
        // ���ø����̼� ���� �� PlayerPrefs�� ����
        PlayerPrefs.SetFloat(MusicVolumeKey, _musicSlider.value);
        PlayerPrefs.SetFloat(SFXVolumeKey, _sfxSlider.value);
        PlayerPrefs.Save(); // ����
    }
}