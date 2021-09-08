using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private int _platform;
    [SerializeField] private float _speed = 5f;


    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_platform].position, _speed * Time.deltaTime);
        if(transform.position == _wayPoints[_platform].position)
        {
            if (_platform == _wayPoints.Count -1)
            {
                _platform = 0;
            }
            else
            {
                _platform++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
