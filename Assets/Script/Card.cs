using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public GameObject objectPrefab;//�d������������w�m��
    private GameObject curGameObject;//������e�ЫإX�Ӫ�����
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
    //�즲�}�l
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
    //�즲
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
        //���칫�ЩҦb��m���I����
        Collider2D[] col = Physics2D.OverlapPointAll(TranlatScreenToWorldPoint(pointerEventData.position));
        //�K�Q�I����
        foreach (Collider2D i in col)
        {
            //�P�_���鬰"�g�a",�åB�g�a�W�S����L�Ӫ�
            if (i.tag == "Land" && i.transform.childCount == 0)
            {
                //���e����K�[���g�a���l����
                curGameObject.transform.parent = i.transform;
                curGameObject.transform.localPosition = Vector3.zero;
                //���s�q�{��,�ͦ�����
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
