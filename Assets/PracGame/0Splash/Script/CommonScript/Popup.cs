using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    [Header("�ؽ�Ʈ")]
    [SerializeField] private Image iconImg;
    [SerializeField] private TextMeshProUGUI contentText;

    [Header("��ư")]
    [SerializeField] private Button oneBtn;
    [SerializeField] private Button twoBtn;
    [SerializeField] private Button xBtn;
    [SerializeField] private Button dimmedBtn;

    [Header("���� �˾�")]
    [SerializeField] private GameObject pop;


    private void Awake()
    {
        //oneBtn?.onClick.AddListener(() => { OneBtnFun(); });
        //twoBtn?.onClick.AddListener(() => { TwoBtnFun(); });
        xBtn?.onClick.AddListener(() => { XBtnFun(); });
        dimmedBtn?.onClick.AddListener(() => { DimmedBtnFun(); });
    }

    public virtual void OpenPopup()
    {
        OpenAni();
    }
    public void OpenPopup(string pContent, Action OneBtnFun, Action TwoBtnFun, Sprite pIcon = null)
    {
        oneBtn.onClick.RemoveAllListeners();
        twoBtn.onClick.RemoveAllListeners();
        if (pIcon != null) iconImg.sprite = pIcon;
        contentText.text = pContent;

        oneBtn.transform.parent.gameObject.SetActive(true);
        oneBtn.gameObject.SetActive(true);
        twoBtn.gameObject.SetActive(true);

        oneBtn?.onClick.AddListener(() => {
            OneBtnFun();
            ClosePopup();
        });
        twoBtn?.onClick.AddListener(() => {
            TwoBtnFun();
            ClosePopup();
        });

        OpenAni();
    }

    public void OpenPopup(string pContent, float pActiveTime, Sprite pIcon = null)
    {
        if (pIcon != null) iconImg.sprite = pIcon;
        contentText.text = pContent;

        oneBtn.gameObject.SetActive(false);
        twoBtn.gameObject.SetActive(false);
        oneBtn.transform.parent.gameObject.SetActive(false);
        OpenTooltipAni(pActiveTime);
    }
    public void OpenPopup(string pContent, int pBtnCnt, Action OneBtnFun, Action TwoBtnFun, Sprite pIcon = null)
    {
        oneBtn.onClick.RemoveAllListeners();
        twoBtn.onClick.RemoveAllListeners();
        if (pIcon != null) iconImg.sprite = pIcon;
        contentText.text = pContent;

        if (pBtnCnt >= 2)
        {
            oneBtn.transform.parent.gameObject.SetActive(true);
            oneBtn.gameObject.SetActive(true);
            twoBtn.gameObject.SetActive(true);
        }
        else if (pBtnCnt == 0)
        {
            oneBtn.gameObject.SetActive(false);
            twoBtn.gameObject.SetActive(false);
            oneBtn.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            oneBtn.transform.parent.gameObject.SetActive(true);
            oneBtn.gameObject.SetActive(true);
            twoBtn.gameObject.SetActive(false);
        }

        oneBtn?.onClick.AddListener(() => {
            OneBtnFun();
            ClosePopup();
        });
        twoBtn?.onClick.AddListener(() => {
            TwoBtnFun();
            ClosePopup();
        });

        OpenAni();
    }
    public virtual void ClosePopup()
    {
        CloseAni();
    }
    #region ��ư �ݹ�
    public virtual void OneBtnFun()
    {
    }
    public virtual void TwoBtnFun()
    {
    }
    public virtual void XBtnFun()
    {
        ClosePopup();
    }
    public virtual void DimmedBtnFun()
    {
    }
    #endregion


    #region �ִϸ��̼� ȿ�� �Լ�
    public void OpenAni()
    {
        pop.transform.localScale = Vector3.one * 0.1f;
        this.gameObject.SetActive(true);
        pop.transform.DOScale(Vector3.one * 1.1f, 0.2f).OnComplete(() => {
            pop.transform.DOScale(Vector3.one, 0.1f);
        });
    }
    public void CloseAni()
    {
        pop.transform.DOScale(Vector3.one * 1.1f, 0.1f).OnComplete(() => {
            pop.transform.DOScale(Vector3.one * 0.1f, 0.2f).OnComplete(() => {
                this.gameObject.SetActive(false);
            });
        });
    }
    #endregion
    #region �ִϸ��̼� ���� ȿ�� �Լ�
    public void OpenTooltipAni(float pTime)
    {
        pop.transform.localScale = Vector3.one * 0.1f;
        this.gameObject.SetActive(true);
        pop.transform.DOScale(Vector3.one * 1.1f, 0.2f).OnComplete(() => {
            pop.transform.DOScale(Vector3.one, 0.1f);
            Invoke(nameof(CloseToolTip), pTime);
        });
    }
    public void CloseToolTipAni()
    {
        pop.transform.DOScale(Vector3.one * 1.1f, 0.1f).OnComplete(() => {
            pop.transform.DOScale(Vector3.one * 0.1f, 0.2f).OnComplete(() => {
                this.gameObject.SetActive(false);
            });
        });
    }

    public void CloseToolTip()
    {
        CloseToolTipAni();
    }
    #endregion
}