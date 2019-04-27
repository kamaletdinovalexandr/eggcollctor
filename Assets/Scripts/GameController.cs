using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private static GameController _instance;

    public static GameController Instance {
        get { return _instance; }
    }

    [SerializeField] private int _maxLives;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _livesText;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private Button _restart;

    private int _currentlives;
    private float _currentScore;
    private float _currentTime;
    public bool IsGameOver;

    [SerializeField] private float _slowMotionTime;
    [SerializeField] private float _slowMotionScale;
    private bool _isSlowMotion;
    private float _slowMotionTimer;
    
    private void Start() {
        _restart.onClick.AddListener(Restart);
        Init();
    }

    private void Init() {
        _isSlowMotion = false;
        _instance = this;
        _currentlives = _maxLives;
        ToggleGameOver(false);
        IsGameOver = false;
    }

    private void FixedUpdate() {
        if (IsGameOver) {
            return;
        }

        if (_isSlowMotion) {
            _slowMotionTimer -= Time.unscaledDeltaTime;
            if (_slowMotionTimer <= 0) {
                Time.timeScale = 1f;
                _isSlowMotion = false;
            }
        }

        _currentTime += Time.unscaledDeltaTime;
        UpdateUi();

        if (_currentlives <= 0) {
            IsGameOver = true;
            ToggleGameOver(true);
        }
    }

    public void IncrementScore() {
        _currentScore++;
    }

    public void SlowMotionEffect() {
        _isSlowMotion = true;
        _slowMotionTimer = _slowMotionTime;
        Time.timeScale = _slowMotionScale;
    }

    private void UpdateUi() {
        _scoreText.text = "Score: " + _currentScore;
        _timeText.text = "Time: " + (int)_currentTime;
        _livesText.text = "Lives: " + _currentlives;
    }

    public void LooseLive() {
        _currentlives--;
    }
    
    private void ToggleGameOver(bool enable) {
        _gameOver.gameObject.SetActive(enable);
        _restart.gameObject.SetActive(enable);
    }

    public void Restart() {
        DestroyEggs(GameObject.FindGameObjectsWithTag("egg"));
        DestroyEggs(GameObject.FindGameObjectsWithTag("superegg"));
        
        Init();
        EggSpawner.Instance.Init();
    }

    private void DestroyEggs(IEnumerable<GameObject> eggs) {
        if (eggs == null)
            return;
        
        foreach (var egg in eggs) {
            Destroy(egg);
        }
    }

    private void OnDestroy() {
        _restart.onClick.RemoveListener(Restart);
    }
}
