using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private RoomAlarm _roomAlarm;

    private Coroutine _changeVolume;
    private AudioSource _sound;
    private float _volumeChangeValue;
    private bool _isActivated;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _roomAlarm.AlarmActivated += Activation;
        _roomAlarm.AlarmDeactivated += Deactivation;
    }

    private void OnDisable()
    {
        _roomAlarm.AlarmActivated -= Activation;
        _roomAlarm.AlarmDeactivated -= Deactivation;
    }

    private void Activation()
    {
        if (_isActivated)
            return;

        _isActivated = true;
        _sound.Play();
        _sound.volume = 0.2f;
        _volumeChangeValue = 0.01f;
        _changeVolume = StartCoroutine(ChangeVolume());
    }

    private void Deactivation()
    {
        _isActivated = false;
        _sound.Stop();
        StopCoroutine(_changeVolume);
    }

    private IEnumerator ChangeVolume()
    {
        while (true)
        {
            if (_sound.volume == 1 || _sound.volume == 0)
                _volumeChangeValue *= -1;

            _sound.volume += _volumeChangeValue;

            yield return null;
        }
    }
}
