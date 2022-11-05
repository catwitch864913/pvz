using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : MonoBehaviour
{
    public GameObject sunPrefab;
    private Animator animator;
    public float readyTime;
    private float timer;
    private int SunNum;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > readyTime)
        {
            animator.SetBool("Ready", true);
        }
    }
    public void BornSunOver()
    {
        BornSun();
        animator.SetBool("Ready", false);
        timer = 0;
    }
    private void BornSun()
    {
        GameObject newSun = Instantiate(sunPrefab);
        SunNum += 1;
        float randomX;
        if (SunNum % 2 == 1)
        {
            randomX = Random.Range(transform.position.x - 30, transform.position.x - 20);
        }
        else
        {
            randomX = Random.Range(transform.position.x + 20, transform.position.x + 30);
        }
        float randomY = Random.Range(transform.position.y - 20, transform.position.y + 20);
        newSun.transform.position = new Vector2(randomX, randomY);
    }
}
