using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bascket : MonoBehaviour {
    
    [SerializeField] private Transform basket;

    [SerializeField] private Transform _leftUp;
    [SerializeField] private Transform _rightUp;
    [SerializeField] private Transform _leftDown;
    [SerializeField] private Transform _rightDown;

    private bool _isUp;
    private bool _isLeft;

    private void Start() {
        basket.position = _leftUp.position;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            _isUp = true;
        }  
        
        if (Input.GetKey(KeyCode.DownArrow)) {
            _isUp = false;
        } 
        
        if (Input.GetKey(KeyCode.LeftArrow)) {
            _isLeft = true;
        } 
        
        if (Input.GetKey(KeyCode.RightArrow)) {
            _isLeft = false;
        }

        if (_isUp && _isLeft)
            basket.position = _leftUp.position;
        
        if (_isUp && !_isLeft)
            basket.position = _rightUp.position;
        
        if (!_isUp && _isLeft)
            basket.position = _leftDown.position;
        
        if (!_isUp && !_isLeft)
            basket.position = _rightDown.position;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "egg") {
            GameController.Instance.IncrementScore();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "superegg") {
            GameController.Instance.IncrementScore();
            GameController.Instance.SlowMotionEffect();
            Destroy(other.gameObject);
        }
    }
}
