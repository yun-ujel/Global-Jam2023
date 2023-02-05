using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Attack : MonoBehaviour
{
    [SerializeField] float bossHealth;
    [SerializeField] float maxDamage;
    [SerializeField] BossMovement bM;
    float damage;

    [SerializeField] Slider slider;

    private void Awake()
    {
        slider.maxValue = bossHealth;
    }

    private void Update()
    {
        slider.value = bossHealth;

        if (bossHealth < 0f)
        {
            Destroy(bM.hitBox);
            Destroy(bM.gameObject);
        }
    }

    public void DealDamage()
    {

        damage = Mathf.MoveTowards(damage, maxDamage, 2 * Time.deltaTime);

        bossHealth -= damage;
    }

    public void ResetDamage()
    {
        damage = 0f;
    }
}
