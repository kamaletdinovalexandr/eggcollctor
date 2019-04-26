using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EggSpawner : MonoBehaviour {
    [SerializeField] private List<Transform> _eggSpawnPositions;
    [SerializeField] private GameObject Egg;
    private float _currentCoolDownTime;
    private float _timer;

    private void SpawnRandomEgg() {
        var num = Random.Range(0, 3);
        switch (num) {
            case 0:
                Instantiate(Egg, _eggSpawnPositions[0].position, Quaternion.identity);
                break;
            case 1:
                Instantiate(Egg, _eggSpawnPositions[1].position, Quaternion.identity);
                break;
            case 2:
                Instantiate(Egg, _eggSpawnPositions[2].position, Quaternion.identity);
                break;
            case 3:
                Instantiate(Egg, _eggSpawnPositions[3].position, Quaternion.identity);
                break;
        }
    }

    private void Update() {
        _timer += Time.deltaTime;
        
        if (_currentCoolDownTime == 0)
            _currentCoolDownTime = Random.Range(0.7f, 2f);

        if (_timer > _currentCoolDownTime) {
            SpawnRandomEgg();
            _timer = 0;
        }
            
    }
}
