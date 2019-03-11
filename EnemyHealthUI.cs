using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour {

    public Slider enemyHealthSlider;
    public Slider enemyDefenseSlider;
    public Damageable enemyDamageable;

	// Use this for initialization
	void Start () {
        enemyHealthSlider.maxValue = enemyDamageable.maxHealth;
        enemyDefenseSlider.maxValue = enemyDamageable.maxDefense;
    }
	
	// Update is called once per frame
	void Update () {
        enemyHealthSlider.value = enemyDamageable.currentHealth;
        enemyDefenseSlider.value = enemyDamageable.currentDefense;
    }
}
