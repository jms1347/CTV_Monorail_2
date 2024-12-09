using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CountDown : MonoBehaviour
{
    public string countdownSoundKey;
    public TextMeshProUGUI countdownText;

    IEnumerator countdownCour;
    private bool isClickStartBtn = false;


    public void StartCountdown()
    {
        if (countdownCour != null)
            StopCoroutine(countdownCour);
        countdownCour = CountDownCour();
        StartCoroutine(countdownCour);
    }

    public void StartCountDown(string pText, float pEndScale = 1.5f)
    {
        //countdownText.color = new Color32(255, 255, 255, 0);
        countdownText.transform.localScale = Vector3.one;
        Vector3 endScale = Vector3.one * pEndScale;
        //countdownText.canvasRenderer.SetAlpha(0f); // �ؽ�Ʈ ���̵带 ���� ���İ� �ʱ�ȭ

        countdownText.text = pText;
        countdownText.transform.DOScale(endScale, 1).SetEase(Ease.OutQuad); // ������ �ִϸ��̼�

        // ���̵� �ִϸ��̼�
        countdownText.DOFade(1f, 0.5f).SetEase(Ease.OutQuad); // ���� ��Ÿ��
        countdownText.DOFade(0f, 0.5f).SetDelay(0.5f); // ���� �����
    }

    public IEnumerator CountDownCour()
    {
        var time = new WaitForSeconds(0.1f);
        SetActiveCountDown(true);

        countdownText.text = "";
        yield return new WaitUntil(() => isClickStartBtn);
       
        StartCountDown("3");
        SoundManager.instance.PlaySFXByKey(countdownSoundKey);
        for (int i = 0; i < 10; i++) yield return time;
        SoundManager.instance.PlaySFXByKey(countdownSoundKey);
        StartCountDown("2");
        for (int i = 0; i < 10; i++) yield return time;
        SoundManager.instance.PlaySFXByKey(countdownSoundKey);
        StartCountDown("1");
        for (int i = 0; i < 10; i++) yield return time;
        SoundManager.instance.PlaySFXByKey(countdownSoundKey);
        StartCountDown("START!", 1.0f);

        for (int i = 0; i < 10; i++) yield return time;
        countdownText.gameObject.SetActive(false);
        SetActiveCountDown(false);
    }


    #region Ʃ�丮�� �˾� ����
    public void SetActiveCountDown(bool pBool)
    {
        //SoundManager.instance.PlayClickSFX1();

        this.gameObject.SetActive(pBool);
    }
    #endregion

    #region 
    public void StartBtn()
    {
        //SoundManager.instance.PlaySFXByKey("click1");
        isClickStartBtn = true;
    }
    #endregion


    #region üũ Ʃ�丮�� ����
    public bool CheckEndCountDown()
    {
        return isClickStartBtn;
    }

    public void StartCountDown()
    {
        isClickStartBtn = true;
    }
    #endregion
}
