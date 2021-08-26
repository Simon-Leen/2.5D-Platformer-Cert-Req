using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _cc;
    private Vector3 _direction;
    [SerializeField] private float _speed = 12.0f;
    [SerializeField] private float _gravity = 30.0f;
    [SerializeField] private float _jumpH = 15.0f;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(_cc.isGrounded == true)
        {
            float h = Input.GetAxis("Horizontal");
            _direction = new Vector3(0, 0, h) * _speed;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpH;
            }
        }
        _direction.y -= _gravity * Time.deltaTime;

        _cc.Move(_direction * Time.deltaTime);
    }
}
