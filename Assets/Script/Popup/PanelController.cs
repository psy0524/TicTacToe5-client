using DG.Tweening;
using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    // �˾�â RectTransform
    [SerializeField] private RectTransform panelRectTransform;
    
    private CanvasGroup _backgroundCanvasGroup;

    // Panel�� Hide �� �� �ؾ� �� ����
    public delegate void PanelControllerHideDelegate();
    

    private void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup> ();
    }

    /// <summary>
    ///  Panel ǥ��
    /// </summary>
    public virtual void Show()
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(1, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    /// <summary>
    /// Panel �����
    /// </summary>
    public void Hide(PanelControllerHideDelegate hideDelegate = null)
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        _backgroundCanvasGroup.DOFade(0, 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(0, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            hideDelegate?.Invoke ();
            Destroy(gameObject);
        });

        // TODO : 
    }

    protected void Shake()
    {
        panelRectTransform.DOShakeAnchorPos(0.3f);
    }
}
