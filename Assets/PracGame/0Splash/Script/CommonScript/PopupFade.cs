using UnityEngine;
using DG.Tweening;

public class PopupFade : MonoBehaviour
{
    public float duration = 0.5f; // 애니메이션 지속 시간
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
        canvasGroup.alpha = 0; // 초기 투명도 설정

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