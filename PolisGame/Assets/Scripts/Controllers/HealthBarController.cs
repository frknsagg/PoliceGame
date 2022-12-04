using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
     public Slider slider;
     public Gradient gradient;
     public Image fill;

     private float maxHealth;
     
     public Transform mLookAt;
     private Transform _localTrans;
     [SerializeField] private EnemyManager enemyManager;
    
 private void Start()
 {
     
     maxHealth = enemyManager.Health;
     SetMaxHealth(maxHealth);
     _localTrans = GetComponent<Transform>();
 }

 public void SetHealth()
 {
     slider.value = enemyManager.Health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
 private void SetMaxHealth(float health)
 {
     slider.maxValue = health;
     slider.value = health;

     fill.color = gradient.Evaluate(1f);
 }

 public void DestroyHealthBar()
 {
     Destroy(gameObject);
 }

 private void FixedUpdate()
 {
     if (mLookAt)
     {
         _localTrans.LookAt(2*_localTrans.position-mLookAt.position);
     }
 }
}
