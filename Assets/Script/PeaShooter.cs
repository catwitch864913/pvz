using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooter : Plant
{
    [Header("§ðÀ»¶¡¹j")]
    public float interval;
    public float timer;
    public GameObject bullet;
    public Transform bulletPos;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0;
                Instantiate(bullet, bulletPos.position, Quaternion.identity);
            }
        }
    }
    

}
