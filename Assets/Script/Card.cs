using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public GameObject objectPrefab;//卡片對應的物體預置物
    private GameObject curGameObject;//紀錄當前創建出來的物件
    private GameObject darkBg;
    private GameObject progressBar;
    public float waitTime;
    public int useSun;
    private float timer;
    private void Start()
    {
        darkBg = transform.Find("dark").gameObject;
        progressBar = transform.Find("progress").gameObject;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        UpdateDarkBg();
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        float per = Mathf.Clamp(timer / waitTime, 0, 1);
        progressBar.GetComponent<Image>().fillAmount = 1 - per;
    }

    void UpdateDarkBg()
    {
        if (progressBar.GetComponent<Image>().fillAmount == 0&&GameManager.instance.SunNum>=useSun)
        {
            darkBg.SetActive(false);
        }
        else
        {
            darkBg.SetActive(true);
        }
    }
    //拖曳開始
    public void OnBeginDrag(BaseEventData data)
    {
        if (darkBg.activeSelf)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        curGameObject = Instantiate(objectPrefab);
        curGameObject.transform.position = TranlatScreenToWorldPoint(pointerEventData.position);
    }
    //拖曳
    public void OnDrag(BaseEventData data)
    {
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        curGameObject.transform.position = TranlatScreenToWorldPoint(pointerEventData.position);
    }
    public void OnEndDrag(BaseEventData data)
    {
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        //拿到鼠標所在位置的碰撞體
        Collider2D[] col = Physics2D.OverlapPointAll(TranlatScreenToWorldPoint(pointerEventData.position));
        //便利碰撞體
        foreach (Collider2D i in col)
        {
            //判斷物體為"土地",並且土地上沒有其他植物
            if (i.tag == "Land" && i.transform.childCount == 0)
            {
                //把當前物體添加為土地的子物件
                curGameObject.transform.parent = i.transform;
                curGameObject.transform.localPosition = Vector3.zero;
                //重製默認值,生成結束
                GameManager.instance.ChangSunNum(-useSun);
                curGameObject = null;
                break;
            }
        }
        if (curGameObject != null)
        {
            GameObject.Destroy(curGameObject);
            curGameObject = null;
        }
    }
    public static Vector3 TranlatScreenToWorldPoint(Vector3 position)
    {
        Vector3 cameraTranslatePos = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(cameraTranslatePos.x, cameraTranslatePos.y, 0);
    }
}
