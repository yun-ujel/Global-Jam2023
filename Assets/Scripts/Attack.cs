using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Attack : MonoBehaviour
{
    [SerializeField] float bossHealth;
    [SerializeField] float maxDamage;
    float damage;

    [SerializeField] Slider slider;

    private void Awake()
    {
        slider.maxValue = bossHealth;
    }

    private void Update()
    {
        slider.value = bossHealth;
    }

    public void DealDamage()
    {

        damage = Mathf.MoveTowards(damage, maxDamage, Time.deltaTime);

        bossHealth -= damage;
    }

    public void ResetDamage()
    {
        damage = 0f;
    }
}
