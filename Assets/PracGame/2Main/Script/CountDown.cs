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
        //countdownText.canvasRenderer.SetAlpha(0f); // 텍스트 페이드를 위해 알파값 초기화

        countdownText.text = pText;
        countdownText.transform.DOScale(endScale, 1).SetEase(Ease.OutQuad); // 스케일 애니메이션

        // 페이드 애니메이션
        countdownText.DOFade(1f, 0.5f).SetEase(Ease.OutQuad); // 점점 나타남
        countdownText.DOFade(0f, 0.5f).SetDelay(0.5f); // 점점 사라짐
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


    #region 튜토리얼 팝업 세팅
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


    #region 체크 튜토리얼 여부
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
