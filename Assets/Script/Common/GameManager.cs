using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;
    
    // Main Scene���� ������ ���� Ÿ��
    private Constants.GameType _gameType;

    // Panel�� ���� ���� Canvas ����
    private Canvas _canvas;

    // Game Logic
    private GameLogic _gameLogic;

    // Game ���� UI�� ����ϴ� ��ü
    private GameUIController _gameUIController;

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

    /// <summary>
    /// Confirm Panel�� ���� �޼���
    /// </summary>
    /// <param name="message"></param>
    public void OpenConfirmPanel(string message, ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        if(_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClicked);
        }
    }

    /// <summary>
    /// Game Scene���� ���� ǥ���ϴ� UI�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="gameTurnPanelType">ǥ���� Turn ����</param>
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType gameTurnPanelType)
    {
        _gameUIController.SetGameTurnPanel(gameTurnPanelType);
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();

        if(scene.name == "Game")
        {
            // Block �ʱ�ȭ
            var blockController = FindFirstObjectByType<BlockController>();
            if(blockController != null)
            {
                blockController.InitBlocks();
            }
            else
            {
                // TODO : ���� �˾��� ǥ���ϰ� ������ �����ϵ���
            }

                // GameUI Controller �Ҵ� �� �ʱ�ȭ
                _gameUIController = FindFirstObjectByType<GameUIController>();
            if(_gameUIController != null)
            {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            // GameLogic ����
            
            _gameLogic = new GameLogic(blockController, _gameType);
        }
    }
}
