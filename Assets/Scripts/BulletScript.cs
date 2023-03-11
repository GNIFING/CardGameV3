using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f; // The speed at which the bullet will move
    public float lifetime = 3f; // The time (in seconds) that the bullet will exist before being destroyed
    public GameObject target;
    private Vector3 direction = new(1, 0); // The direction in which the bullet should move

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (target != null && Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Debug.Log("Bullet has reached the target");
            Destroy(gameObject);
        }
    }
}
