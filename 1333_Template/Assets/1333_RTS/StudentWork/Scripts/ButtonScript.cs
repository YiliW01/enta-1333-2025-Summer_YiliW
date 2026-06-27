using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private RectTransform startPanel;
    [SerializeField] private CanvasGroup startCanvas;

    [SerializeField] private RectTransform optionsPanel;
    [SerializeField] private CanvasGroup optionsCanvas;

    private float openPos = 0f;
    private float closedPosOptions = -1050f;
    private float closedPosStart = 1050f;
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private float easeOvershoot;

    public void OpenStart()
    {
        startPanel.DOAnchorPosY(openPos, transitionDuration).SetEase(Ease.OutElastic, easeOvershoot).OnComplete(() =>
        {
            startCanvas.blocksRaycasts = true;
        });
    }

    public void CloseStart()
    {
        startPanel.DOAnchorPosY(closedPosStart, transitionDuration).SetEase(Ease.OutElastic, easeOvershoot).OnComplete(() =>
        {
            startCanvas.blocksRaycasts = false;
        });
    }

    public void OpenOptions()
    {
        optionsPanel.DOAnchorPosY(openPos, transitionDuration).SetEase(Ease.OutElastic, easeOvershoot).OnComplete(() =>
        {
            optionsCanvas.blocksRaycasts = true;
        });
    }

    public void CloseOptions()
    {
        optionsPanel.DOAnchorPosY(closedPosOptions, transitionDuration).SetEase(Ease.OutElastic, easeOvershoot).OnComplete(() =>
        {
            optionsCanvas.blocksRaycasts = false;
        });
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("RTS");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
