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
    private float closedPos = -1050f;
    [SerializeField] private float transitionDuration;

    public void OpenStart()
    {
        startPanel.DOAnchorPosY(openPos, transitionDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            startCanvas.blocksRaycasts = true;
        });
    }

    public void CloseStart()
    {
        startPanel.DOAnchorPosY(closedPos, transitionDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            startCanvas.blocksRaycasts = false;
        });
    }

    public void OpenOptions()
    {
        optionsPanel.DOAnchorPosY(openPos, transitionDuration).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            optionsCanvas.blocksRaycasts = true;
        });
    }

    public void CloseOptions()
    {
        optionsPanel.DOAnchorPosY(closedPos, transitionDuration).SetEase(Ease.OutQuad).OnComplete(() =>
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
