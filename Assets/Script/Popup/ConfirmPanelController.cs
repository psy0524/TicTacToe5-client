using TMPro;
using UnityEngine;

public class ConfirmPanelController : PanelController
{
    [SerializeField] private TMP_Text messageText;

    /// <summary>
    /// Confirm Panel�� ǥ���ϴ� �޼���
    /// </summary>
    public void Show(string message)
    {
        messageText.text = message;
        base.Show();
    }
    
    /// <summary>
    /// Ȯ�� ��ư Ŭ���� ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickConfirmButton()
    {
        Hide();
        GameManager.Instance.ChangeToMainScene();
    }

    /// <summary>
    /// X ��ư Ŭ���� ȣ��Ǵ� �޼���
    /// </summary>
    public void OnClickCloseButton()
    {
        Hide();
    }
}
