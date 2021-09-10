using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private List<Transform> _floorStops;
    [SerializeField] private int _floorNumber;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _floorStopTime = 5f;
    private bool _down;
    private bool _canChange;

    private void Start()
    {
        _floorNumber = _floorStops.Count - 1;
        _down = true;
        _canChange = true;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _floorStops[_floorNumber].position, _speed * Time.deltaTime);
        if(transform.position == _floorStops[_floorNumber].position && _canChange == true)
        {
            _canChange = false;
            StartCoroutine(FloorChange());
        }
    }

    IEnumerator FloorChange()
    {
        yield return new WaitForSeconds(_floorStopTime);
        if (_down == true)
        {
            if (_floorNumber == 0)
            {
                _down = false;
                _floorNumber++;
            }
            else
            {
                _floorNumber--;
            }
        }
        else
        {
            if (_floorNumber == _floorStops.Count - 1)
            {
                _down = true;
                _floorNumber--;
            }
            else
            {
                _floorNumber++;
            }
        }
        _canChange = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
