using System.Collections;
using UnityEngine;
using ZenFulcrum.Track;

public class MainController : MonoBehaviour
{
    [Header("게임 변수")]
    public TrackCart Cart;
    public Station station;
    public Track endTrack;
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
        station.startingForce.targetSpeed = 0f;

    }

    public IEnumerator GameStartCour()
    {
        var time = new WaitForSeconds(0.1f);
        GameInit();
        yield return StartCoroutine(countDown.CountDownCour());
        isGameStart = true;
        StartCart();
        yield return new WaitUntil(() => Cart.CurrentTrack == endTrack);
        StopCart();

    }

    public void StartCart()
    {
        //카드시작
        station.startingForce.targetSpeed = 1f;

    }

    public void StopCart()
    {
        restartBtn.SetActive(true);
        station.startingForce.targetSpeed = 0f;
    }

    public void ReStart()
    {
        restartBtn.SetActive(false);

        //SoundManager.instance.PlaySFXByKey("click1");
        //FadeManager.instance.ChangeScene("MainScene");
        LoadingSceneManager.LoadScene("MainScene");
    }

    public void End()
    {
        Application.Quit();
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
