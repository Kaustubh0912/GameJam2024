using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;
    [SerializeField] private bool back;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!back)
        {
            if(collision.tag=="Player")
            {
                if (collision.transform.position.x < transform.position.x || collision.transform.position.y<transform.position.y)
                    cam.MoveToNewRoom(nextRoom);
                else
                    cam.MoveToNewRoom(previousRoom);
            }
        }
        else
        {
            if (collision.tag == "Player")
            {
                if (collision.transform.position.x > transform.position.x || collision.transform.position.y > transform.position.y)
                    cam.MoveToNewRoom(nextRoom);
                else
                    cam.MoveToNewRoom(previousRoom);
            }
        }
    }
}
