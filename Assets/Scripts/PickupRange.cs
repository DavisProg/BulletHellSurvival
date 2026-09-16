using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PickupRange : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float accelerationSpeed;
    private List<Collider2D> colList = new List<Collider2D>();
    private List<Collider2D> updatedColList = new List<Collider2D>();
    private Rigidbody2D player;

    void Start()
    {
        player = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            updatedColList.Add(collision);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickup"))
        {
            updatedColList.Remove(collision);
        }
    }
    void FixedUpdate()
    {
        if(colList.Count > 0)
        {
            foreach(Collider2D xp in colList)
            {
                Vector3 direction = (player.transform.position - xp.transform.position).normalized;
                Pickup pickup = xp.GetComponent<Pickup>();
                pickup.time += Time.fixedDeltaTime;
                float strength = curve.Evaluate(pickup.time / accelerationSpeed);
                xp.transform.position += direction * speed * strength * Time.fixedDeltaTime;
            }
        }
        colList = updatedColList;
    }
}
