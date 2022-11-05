using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    [Header("¤è¦V")]
    public Vector3 direction;
    public float speed;
    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Zombie")
        {
            Destroy(gameObject);
            collision.GetComponent<ZombieNormal>().ChangeHealth(-damage);
        }
    }
}
