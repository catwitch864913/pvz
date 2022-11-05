using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int SunNum;
    public GameObject bornParent;
    public GameObject zombiePrefab;
    public float craeteZombieTi;
    private void Start()
    {
        instance = this;
        //SunNum=100;
        UIManager.instance.InitUI();
        CreateZobie();
    }
    private void Update()
    {
        
    }
    public void ChangSunNum(int changeNum)
    {
        SunNum += changeNum;
        if (SunNum <= 0)
        {
            SunNum = 0;
            //todo
        }
        UIManager.instance.UpdateUI();
    }
    public void CreateZobie()
    {
        StartCoroutine(DalayCreateZombie());
    }
    IEnumerator DalayCreateZombie()
    {
        yield return new WaitForSeconds(craeteZombieTi);
        GameObject zombie = Instantiate(zombiePrefab);
        int index = Random.Range(0, 5);
        Transform zombiePos = bornParent.transform.Find("born" + index.ToString());
        zombie.transform.SetParent(zombiePos, false);
        
        //zombie.transform.parent = zombiePos.transform;
        StartCoroutine(DalayCreateZombie());
    }
}
