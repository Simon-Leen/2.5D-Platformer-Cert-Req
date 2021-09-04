using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _cc;
    private Vector3 _direction;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _gravity = 30.0f;
    [SerializeField] private float _jumpH = 12.0f;
    private Animator _anim;
    private bool _jumping = false;
    private bool _onLedge = false;
    private Ledge _activeLedge;
    private int _coins;
    private UIManager _uIManager;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uIManager == null)
        {
            Debug.LogError("UIManager null in Player");
        }
        _uIManager.UpdateCoinsDisplay(_coins);
    }

    void Update()
    {
        if(_cc.enabled == true)
        {
            CalcMovement();
        }
        if(_onLedge == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("ClimbUp");
            }
        }
    }

    void CalcMovement()
    {
        if (_cc.isGrounded == true)
        {
            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }
            float h = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, h) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(h));

            if (h != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpH;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }
        }
        _direction.y -= _gravity * Time.deltaTime;
        _cc.Move(_direction * Time.deltaTime);
    }

    public void GrabLedge(Vector3 handPos, Ledge currentLedge)
    {
        _cc.enabled = false;
        _anim.SetBool("GrabLedge", true);
        //_anim.SetBool("Jumping", false);
        transform.position = handPos;
        _onLedge = true;
        _activeLedge = currentLedge;
    }

    public void ClimbComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        _cc.enabled = true;
    }

    public void UpdateCoins()
    {
        _coins++;
        _uIManager.UpdateCoinsDisplay(_coins);
    }
}
