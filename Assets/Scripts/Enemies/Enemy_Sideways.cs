using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;
    [SerializeField] private bool sideways;

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private bool movingUp;
    private float topEdge;
    private float bottomEdge;

    private void Awake()
    {
        // Calculate the movement boundaries based on the initial position and movement distance
        leftEdge = sideways ? transform.position.x - movementDistance : transform.position.x;
        rightEdge = sideways ? transform.position.x + movementDistance : transform.position.x;
        bottomEdge = sideways ? transform.position.y : transform.position.y - movementDistance;
        topEdge = sideways ? transform.position.y : transform.position.y + movementDistance;
    }

    private void Update()
    {
        // Update movement based on direction and boundaries
        if (sideways)
        {
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position -= new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);
                }
                else
                    movingLeft = false;
            }
            else
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position += new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);
                }
                else
                    movingLeft = true;
            }
        }
        else
        {
            if (movingUp)
            {
                if (transform.position.y > bottomEdge)
                {
                    transform.position -= new Vector3(0f, movementSpeed * Time.deltaTime, 0f);
                }
                else
                    movingUp = false;
            }
            else
            {
                if (transform.position.y < topEdge)
                {
                    transform.position += new Vector3(0f, movementSpeed * Time.deltaTime, 0f);
                }
                else
                    movingUp = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
