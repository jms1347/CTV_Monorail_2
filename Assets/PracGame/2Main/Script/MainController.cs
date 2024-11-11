using System.Collections;
using UnityEngine;
using ZenFulcrum.Track;

public class MainController : MonoBehaviour
{
    [Header("���� ����")]
    public TrackCart Cart;
    public Station station;
    public Track endTrack;
    public CountDown countDown;

    public GameObject restartBtn;
    IEnumerator startCour;
    public bool isGameStart = false;

    public float maxSpeed = 10f;
    public float initialAcceleration = 0.25f; // �ʱ� ���ӵ�
    private float currentAcceleration; // ���� ���ӵ�
    public float accelerationIncreaseRate = 0.1f; // ���ӵ� ������

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
        currentAcceleration = initialAcceleration; // ���� ���ӵ��� �ʱ� ���ӵ��� ����

        while (isGameStart)
        {
            if (station.startingForce.targetSpeed < maxSpeed)
            {
                StartCart();

                yield return null;
            }
            else
            {
                break;
            }
        }
        yield return new WaitUntil(() => Cart.CurrentTrack == endTrack);
        StopCart();

    }

    public void StartCart()
    {
        station.startingForce.targetSpeed += currentAcceleration * Time.deltaTime; // ���ӵ� ����
        currentAcceleration += accelerationIncreaseRate * Time.deltaTime; // ���ӵ� ����
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
