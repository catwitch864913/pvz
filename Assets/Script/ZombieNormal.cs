using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNormal : MonoBehaviour
{
    public Vector3 direction = new Vector3(-1, 0, 0);
    public float speed = 10;
    private bool isWalk;
    private Animator animator;
    public float damage;
    public float damageInterval = 0.5f;
    private float damageTimer;
    public float health = 100;
    private float currentHealth;
    private GameObject head;
    private bool lostHead;
    private bool isDie;
    public float lostHeadHealth = 40;
    // Start is called before the first frame update
    void Start()
    {
        isWalk = true;
        animator = GetComponent<Animator>();
        damageTimer = 0;
        currentHealth = health;
        head = transform.Find("Head").gameObject;
        isDie = false;
        lostHead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie)
            return;
        Move();
    }

    private void Move()
    {
        if (isWalk)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie)
            return;
        if (collision.tag == "Plant")
        {
            isWalk = false;
            animator.SetBool("Walk", false);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (isDie)
            return;
        if (collision.tag == "Plant")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                damageTimer = 0;
                //todo:對植物造成傷害
                Plant peaShooter = collision.GetComponent<Plant>();
                float newHealth = peaShooter.ChangeHealth(-damage);
                if (newHealth <= 0)
                {
                    isWalk = true;
                    animator.SetBool("Wlak", true);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Plant")
        {
            isWalk = true;
            animator.SetBool("Walk",true );
        }
    }
    public void ChangeHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        if (currentHealth < lostHeadHealth && !lostHead)
        {
            lostHead = true;
            gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("LostHead");
            animator.SetBool("LostHead", true);
            head.SetActive(true);
        }
        if (currentHealth <= 0&&!isDie)
        {
            animator.SetTrigger("Die");
            isDie = true;
        }
    }
    public void DieAniOver()
    {
        animator.enabled = false;
        Destroy(gameObject);
    }
}
