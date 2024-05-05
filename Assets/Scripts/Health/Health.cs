using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currenthealth {  get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        currenthealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currenthealth = Mathf.Clamp(currenthealth - _damage, 0, startingHealth);
        
        if(currenthealth > 0 ) 
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
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
    public void AddHealth(float _health)
    {
        currenthealth = Mathf.Clamp(currenthealth + _health, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8,9,true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1,0,0,0.5f); 
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2)); 
            spriteRenderer.color=Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
    
}
