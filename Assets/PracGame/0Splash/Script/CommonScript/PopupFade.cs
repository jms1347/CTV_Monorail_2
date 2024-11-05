using UnityEngine;
using DG.Tweening;

public class PopupFade : MonoBehaviour
{
    public float duration = 0.5f; // �ִϸ��̼� ���� �ð�
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

    }
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenPopup()
    {
        canvasGroup.alpha = 0; // �ʱ� ���� ����

        this.gameObject.SetActive(true);
        canvasGroup.DOFade(1, duration).SetEase(Ease.OutQuad);
    }
        
    public void ClosePopup()
    {
        canvasGroup.DOFade(0, duration).SetEase(Ease.InQuad).OnComplete(()=> {
            this.gameObject.SetActive(false);
        });

    }
}