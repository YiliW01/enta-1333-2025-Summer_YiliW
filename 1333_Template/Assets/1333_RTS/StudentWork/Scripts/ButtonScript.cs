using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private RectTransform buttonRect;
    private float rectStartWidth;
    [SerializeField] private Vector3 scaleWidthffff;
    private float scaleWidth = 2;

    private void Start()
    {
        
    }

    public void Highlight()
    {
        buttonRect.DOScaleX(scaleWidth, 1f);
    }
}
