using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [Header("�s�b����")]
    public float duration;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        GameManager.instance.ChangSunNum(25);
        //TODO:����UI�Ӷ��Ҧb��m,�M��P��
        GameObject.Destroy(gameObject);
    }
}
