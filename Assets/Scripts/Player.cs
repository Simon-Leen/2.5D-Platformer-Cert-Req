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
    private Animator _anim;
    private bool _jumping;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(_cc.isGrounded == true)
        {
            if(_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }
            float h = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, h) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(h));

            if(h != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpH;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }
        }
        _direction.y -= _gravity * Time.deltaTime;

        _cc.Move(_direction * Time.deltaTime);
    }
}
