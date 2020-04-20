using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class RoomAlarm : MonoBehaviour
{
    private int _countRogue;

    public int CountRogue
    {
        get
        {
            return _countRogue;
        }
        private set
        {
            _countRogue = Mathf.Clamp(value, 0, int.MaxValue);

            if (_countRogue == 0)
                AlarmDeactivated?.Invoke();
            else
                AlarmActivated?.Invoke();
        }
    }

    public event UnityAction AlarmActivated;
    public event UnityAction AlarmDeactivated;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Rogue>(out Rogue rogue))
        {
            CountRogue++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rogue>(out Rogue rogue))
        {
            CountRogue--;
        }
    }
}
