using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EggSpawner : MonoBehaviour {
    private static EggSpawner _instance;
    public static EggSpawner Instance {
        get { return _instance; }
    }

    [SerializeField] private List<Transform> _eggSpawnPositions;
    [SerializeField] private GameObject _eggGO;
    [SerializeField] private GameObject _superEggGO;
    [SerializeField] private float _superEggCoolDownTime;
    [SerializeField] private float _startEggsCoolDown;
    [SerializeField] private float _coolDownStep;
    [SerializeField] private int _coolDownIncrementCounter;
    [SerializeField] private float _coolDownIncrementCounterMinimal;

    private float _eggTimer;
    private float _superEggTimer;
    private int _counter;
    private float _currentEggsCooldown;

    private void Start() {
        _instance = this;
        Init();
    }

    public void Init() {
        _superEggTimer = _superEggCoolDownTime;
        _currentEggsCooldown = _startEggsCoolDown;
    }

    private void FixedUpdate() {
        if (GameController.Instance.IsGameOver)
            return;

        _eggTimer -= Time.deltaTime;
        _superEggTimer -= Time.deltaTime;

        if (_counter >= _coolDownIncrementCounter) {
            _currentEggsCooldown -= _coolDownStep;
            if (_currentEggsCooldown < _coolDownIncrementCounterMinimal)
                _currentEggsCooldown = _coolDownIncrementCounterMinimal;
            
            _counter = 0;
            Debug.Log("New spawn cooldown: " + _currentEggsCooldown);
        }

        if (_eggTimer <= 0) {
            SpawnRandomSpawnerEgg();
            _eggTimer = _currentEggsCooldown;
        }
        
        if (_superEggTimer <= 0) {
            SpawnRandomSpawnerEgg(true);
            _superEggTimer = _superEggCoolDownTime;
        }
    }
    
    private void SpawnRandomSpawnerEgg(bool isSuper = false) {
        var spawnPosition = Random.Range(0, 3);
        var egg = isSuper ? _superEggGO : _eggGO;
        Instantiate(egg, _eggSpawnPositions[spawnPosition].position, Quaternion.identity);
        _counter++;
    }
}
