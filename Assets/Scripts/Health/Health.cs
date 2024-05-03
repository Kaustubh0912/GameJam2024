using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currenthealth {  get; private set; }
    private Animator anim;
    private bool dead;
    private void Awake()
    {
        currenthealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - _damage, 0, startingHealth);
        
        if(currenthealth > 0 ) 
        {
            anim.SetTrigger("Hurt");
        }
        else
        {
            if(!dead) 
            {
                anim.SetTrigger("Die");
                GetComponent<PlayerController>().enabled=false;
                dead = true;
            }
        }

    }
    
}
