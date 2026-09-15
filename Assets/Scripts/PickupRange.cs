using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PickupRange : MonoBehaviour
{
    [SerializeField] float speed;
    private List<Collider2D> colList = new List<Collider2D>();
    private Rigidbody2D player;

    void Start()
    {
        player = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            colList.Add(collision);
        }if (collision.CompareTag("Pickup"))
        {
            colList.Add(collision);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            colList.Remove(collision);
        }
    }
    void FixedUpdate()
    {
        if(colList.Count > 0)
        {
            foreach(Collider2D xp in colList)
            {
                Vector3 direction = (player.transform.position - xp.transform.position).normalized;
                xp.transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
