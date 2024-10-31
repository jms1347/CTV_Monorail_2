using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class FadeManager : Singleton<FadeManager>
{
    public CanvasGroup Fade_img;
    float fadeDuration = 2; //�����Ǵ� �ð�
    public GameObject Loading;
    public TextMeshProUGUI Loading_text; //�ۼ�Ʈ ǥ���� �ؽ�Ʈ
    public string nextScene;

    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // �̺�Ʈ�� �߰�
    }
    IEnumerator LoadScene(string sceneName)
    {
        Loading.SetActive(true); //�ε� ȭ���� ���

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false; //�ۼ�Ʈ �����̿�

        float past_time = 0;
        float percentage = 0;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true; //�� ��ȯ �غ� �Ϸ�
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }
            Loading_text.text = percentage.ToString("0") + "%"; //�ε� �ۼ�Ʈ ǥ��
        }
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ���� ����*
    }


    public void ChangeScene(string sceneName)
    {
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() =>
        {
            Fade_img.blocksRaycasts = true; //�Ʒ� ����ĳ��Ʈ ����
        })
        .OnComplete(() =>
        {
            //�ε�ȭ�� ����, �� �ε� ����
            StartCoroutine(nameof(LoadScene), sceneName); /// �� �ε� �ڷ�ƾ ���� ///
        });
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration)
        .OnStart(() => {
            Loading.SetActive(false);
        })
        .OnComplete(() => {
            Fade_img.blocksRaycasts = false;
        });
    }



    public void OnLoading()
    {
        Loading.SetActive(true); //�ε� ȭ���� ���

        Loading_text.gameObject.SetActive(false);
    }

    public void OffLoading()
    {
        Loading.SetActive(false); //�ε� ȭ���� ���

        Loading_text.gameObject.SetActive(true);
    }


    private void OpenScene()
    {
        Fade_img.alpha = 1;

        Fade_img.DOFade(0, fadeDuration)
        .OnStart(() => {
            Loading.SetActive(false);
        })
        .OnComplete(() => {
            Fade_img.blocksRaycasts = false;
            Fade_img.DOKill();
        });
    }
}
