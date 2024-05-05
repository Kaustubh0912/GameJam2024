using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
           collision.GetComponent<Health>().AddHealth(health);
            gameObject.SetActive(false);
        }
    }

}
