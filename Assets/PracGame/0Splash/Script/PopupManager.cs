using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [Header("�⺻ �޽��� �˾�")]
    public GameObject popupPrefab; // Inspector���� �Ҵ�
    public Transform popupParent;
    public GameObject finalOpenPop;

    [SerializeField] private List<Popup> popupList = new List<Popup>();

    [Header("����")]
    [SerializeField] private List<Tooltip> tooltipList = new List<Tooltip>();

    [Header("�¾� �˾�")]
    public GameObject setupPopup;
    public GameObject setupPop;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        OnOffSetUpPopup();
    //    }
    //}

    #region �¾� ���� �Լ�
    public void OnOffSetUpPopup()
    {
        if (setupPopup.activeSelf)
        {
            //Ȱ�����¸�
            CloseAni(setupPopup, setupPop);
        }
        else
        {
            OpenAni(setupPopup, setupPop);
        }
    }
    #endregion

    #region �˾� ���� �Լ�
    public void InitPopupList()
    {
        CloseAllPopup();
    }

    public void OpenPopup(string pContent, float pActiveTime, Sprite pIcon = null)
    {
        for (int i = 0; i < popupList.Count; i++)
        {
            if (!popupList[i].gameObject.activeSelf)
            {
                finalOpenPop = popupList[i].gameObject;
                popupList[i].OpenPopup(pContent, pActiveTime, pIcon);
                popupList[i].transform.SetAsLastSibling();
                break;
            }
        }
    }
    public void OpenPopup(string pContent, int pBtnCnt, Action OneBtnFun, Action TwoBtnFun, Sprite pIcon = null)
    {
        for (int i = 0; i < popupList.Count; i++)
        {
            if (!popupList[i].gameObject.activeSelf)
            {
                finalOpenPop = popupList[i].gameObject;
                popupList[i].OpenPopup(pContent, pBtnCnt, OneBtnFun, TwoBtnFun, pIcon);
                popupList[i].transform.SetAsLastSibling();
                break;
            }
        }
    }
    public void OpenPopup(string pContent, Action OneBtnFun, Action TwoBtnFun, Sprite pIcon = null)
    {
        for (int i = 0; i < popupList.Count; i++)
        {
            if (!popupList[i].gameObject.activeSelf)
            {
                finalOpenPop = popupList[i].gameObject;
                popupList[i].OpenPopup(pContent, OneBtnFun, TwoBtnFun, pIcon);
                popupList[i].transform.SetAsLastSibling();
                break;
            }
        }
    }
    #region ��ü �˾� �ݱ�
    public void CloseAllPopup()
    {
        for (int i = 0; i < popupList.Count; i++)
        {
            popupList[i].ClosePopup();
        }
    }

    public void CloseFinalPopup()
    {
        finalOpenPop?.SetActive(false);
        finalOpenPop = null;
    }
    #endregion
    #endregion

    #region ���� ���� �Լ�
    public void InitTooltipList()
    {
        for (int i = 0; i < tooltipList.Count; i++)
        {
            tooltipList[i].InitTooltip();
        }
    }

    #region ���� ����
    public void OpenTooltip(string pContent, Vector3 pTooltipPos, float pTime = 2.0f)
    {
        for (int i = 0; i < tooltipList.Count; i++)
        {
            if (!tooltipList[i].gameObject.activeSelf)
            {
                tooltipList[i].OpenTooltip(pContent, pTooltipPos, pTime);
                break;
            }
        }
    }
    public void OpenTooltip(string pContent, float pTime = 2.0f)
    {
        for (int i = 0; i < tooltipList.Count; i++)
        {
            if (!tooltipList[i].gameObject.activeSelf)
            {
                tooltipList[i].OpenTooltip(pContent, pTime);
                break;
            }
        }
    }
    #endregion
    #endregion



    #region �ִϸ��̼� ȿ�� �Լ�
    public void OpenAni(GameObject pPopup, GameObject pAniPopup)
    {
        pAniPopup.transform.localScale = Vector3.one * 0.1f;
        pPopup.SetActive(true);
        pAniPopup.transform.DOScale(Vector3.one * 1.1f, 0.2f).OnComplete(() => {
            pAniPopup.transform.DOScale(Vector3.one, 0.1f);
        });
    }
    public void CloseAni(GameObject pPopup, GameObject pAniPopup)
    {
        pAniPopup.transform.DOScale(Vector3.one * 1.1f, 0.1f).OnComplete(() => {
            pAniPopup.transform.DOScale(Vector3.one * 0.1f, 0.2f).OnComplete(() => {
                pPopup.gameObject.SetActive(false);
            });
        });
    }
    #endregion
}
