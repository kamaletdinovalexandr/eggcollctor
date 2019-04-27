using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFloor : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other) {
        var egg = other.gameObject.GetComponent<Egg>();
        if (!egg.HasCollided) {
            GameController.Instance.LooseLive();
            egg.HasCollided = true;
        }
        
    }
}
