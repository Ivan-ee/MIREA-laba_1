using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static event Action LoseGameAction;
    
    [Header("Player Sideways Movement")] 
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _leftPosition;
    [SerializeField] private GameObject _middlePosition;
    [SerializeField] private GameObject _rightPosition;

    [Header("Player Rotation Speed")] [SerializeField] private float _rotationSpeed;
    
    [Header("Player Health")] 
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _emptyHeart;
    
    private Vector3 _position;
    private Tween _tween;
    private void Awake()
    {
        Rotate();
    }
    private void Update()
    {
        SidewaysMovement();
        CheckHealth();
    }
    void SidewaysMovement()
    {
        _position = transform.position;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (_position.x is >= -46 and <= 0)
            {
                _tween.Kill();
                _tween = transform.DOMove(_leftPosition.transform.position, _speed).SetSpeedBased().SetEase(Ease.Linear);
            }
            else if (_position.x is >= 0 and <= 46)
            {
                _tween.Kill();
                _tween = transform.DOMove(_middlePosition.transform.position, _speed).SetSpeedBased().SetEase(Ease.Linear);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_position.x is >= 0 and <= 46)
            {
                _tween.Kill();
                _tween = transform.DOMove(_rightPosition.transform.position, _speed).SetSpeedBased().SetEase(Ease.Linear);
            }
            else if (_position.x is >= -46 and <= 0)
            {
                _tween.Kill();
                _tween = transform.DOMove(_middlePosition.transform.position, _speed).SetSpeedBased().SetEase(Ease.Linear);
            }
        }
    }

    void CheckHealth()
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < _health)
            {
                _hearts[i].sprite = _fullHeart;
            }
            else
            {
                _hearts[i].sprite = _emptyHeart;
            }
            
            if (i < _maxHealth)
            {
                _hearts[i].enabled = true;
            }
            else
            {
                _hearts[i].enabled = false;
            }
        }
    }
    void Rotate()
    {
        transform.DORotate(new Vector3(360,0,0), _rotationSpeed, RotateMode.FastBeyond360)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }
    public void Damage()
    {
        _health -= 1;

        if (_health <= 0)
        {
            LoseGameAction?.Invoke();
        }
    }
}
