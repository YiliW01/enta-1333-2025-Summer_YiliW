using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlidePanelScript : MonoBehaviour
{
    [SerializeField] private float _openPosition;
    [SerializeField] private float _closedPosition;
    [SerializeField] private float transitionDuration = 0.5f;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _closedPosition = -rectTransform.rect.width;
        _openPosition = 0;
    }

    public void Open()
    {
        //slide the panel in using a tween
        rectTransform.DOAnchorPosX(_openPosition, transitionDuration).SetEase(Ease.OutBounce).OnComplete(() => {
            //once panel completes its slide transition, make its button clickable and animate the button in
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            GetComponentInChildren<Button>().GetComponent<RectTransform>().DOAnchorPosY(0f, 0.5f).SetEase(Ease.InQuad);
        });
    }

    public void Close()
    {
        //make any buttons in the panel un-clickable
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //slide the panel out, reset the button to its default position once the panel transition animation is complete
        rectTransform.DOAnchorPosX(_closedPosition, transitionDuration).SetEase(Ease.InQuad).OnComplete(() => {
            GetComponentInChildren<Button>().GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -160f);
        });
    }
}
