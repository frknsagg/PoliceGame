using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
     public Slider slider;
     public Gradient gradient;
     public Image fill;

     private float maxHealth = 100;
     private float _currentHealth;

     public Transform mLookAt;
     private Transform _localTrans;
     
 private void Awake()
    {
        
     SetMaxHealth(maxHealth);
     _currentHealth = maxHealth;
     _localTrans = GetComponent<Transform>();
     slider.onValueChanged.AddListener(SetHealth);

    }

 private void SetHealth(float health)
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
 private void SetMaxHealth(float health)
 {
     slider.maxValue = health;
     slider.value = health;

     fill.color = gradient.Evaluate(1f);
 }
 public  void DecreaseDamage( )
 {
     _currentHealth -= 10;
     slider.value = _currentHealth;

    
 }

 public void DestroyHealthBar()
 {
     Destroy(gameObject);
 }

 private void LateUpdate()
 {
     if (mLookAt)
     {
         _localTrans.LookAt(2*_localTrans.position-mLookAt.position);
     }
 }
}
