using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Main Scene���� ������ ���� Ÿ��
    private Constants.GameType _gameType;


    /// <summary>
    /// Main���� Game Scene���� ��ȯ�� ȣ��� �޼���
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        // 0 : Single, 1 : Dual, 2 : Multi
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Game���� Main Scene���� ��ȯ�� ȣ��� �޼���
    /// </summary>
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        // TODO : �� ��ȯ�� ó���� �Լ�
    }
}
