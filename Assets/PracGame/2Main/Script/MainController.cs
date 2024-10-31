using System.Collections;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [Header("게임 변수")]
    public GameObject Cart;

    public CountDown countDown;

    public GameObject restartBtn;
    IEnumerator startCour;
    public bool isGameStart = false;

    public void Start()
    {
        GameStart();
    }
    public void GameStart()
    {
        if (startCour != null)
            StopCoroutine(startCour);
        startCour = GameStartCour();
        StartCoroutine(startCour);
    }
    public void GameInit()
    {
        SoundManager.instance.StopBGM();
        SoundManager.instance.StopSfx();
        SoundManager.instance.PlayBGMByKey("pado");
    }

    public IEnumerator GameStartCour()
    {
        var time = new WaitForSeconds(0.1f);
        GameInit();
        yield return StartCoroutine(countDown.CountDownCour());
        isGameStart = true;
        StartCart();
        for (int i = 0; i < 1550; i++) yield return time;
        StopCart();

    }

    public void StartCart()
    {
        Cart.GetComponent<Animator>().SetTrigger("Start");
        Cart.GetComponent<Animator>().speed = 1f;
        SoundManager.instance.PlaySFXByKey("monorail");
    }

    public void StopCart()
    {
        Cart.GetComponent<Animator>().speed = 0f;
        SoundManager.instance.StopSfx();
        restartBtn.SetActive(true);
    }

    public void ReStart()
    {
        restartBtn.SetActive(false);

        //SoundManager.instance.PlaySFXByKey("click1");
        //FadeManager.instance.ChangeScene("MainScene");
        LoadingSceneManager.LoadScene("MainScene");
    }

    public void ClickSound()
    {
        SoundManager.instance.PlayUIMByKey("click1");
    }

    public void HoverSound()
    {
        SoundManager.instance.PlayUIMByKey("hover");
    }
}
