using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

public class SplashController : MonoBehaviour
{
    private void Start()
    {
        // isActive�� true�� ����� �� ȣ��Ǵ� �Լ�
        //GoogleSheetManager.instance.IsSetData.Where(x => x).Subscribe(_ => SetAllGoogleData()).AddTo(this);
        SetAllGoogleData();
        // ���÷� 2�� �Ŀ� isActive�� true�� ����
        //Observable.Timer(System.TimeSpan.FromSeconds(2)).Subscribe(_ => GoogleSheetManager.instance.IsSetData.Value = true).AddTo(this);

    }
    private void SetAllGoogleData()
    {
       // Debug.Log("�κ�� �̵�");
        //FadeManager.instance.ChangeScene("MainScene");
        LoadingSceneManager.LoadScene("MainScene");

    }
}
