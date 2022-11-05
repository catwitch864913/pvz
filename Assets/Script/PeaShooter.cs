using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    [Header("§ðÀ»¶¡¹j")]
    public float interval;
    public float timer;
    public GameObject bullet;
    public Transform bulletPos;
    public float Health = 100;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = Health;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }
    public float ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, Health);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        return currentHealth;
    }

}
