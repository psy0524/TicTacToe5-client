
public class GameLogic
{
    public BlockController blockController;

    private Constants.PlayerType[,] _board; // ������ ���� ����

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B
    private BasePlayerState _currentPlayerState; // ���� ���� Player
    
    /// <summary>
    /// ������
    /// </summary>
    /// <param name="blockController"></param>
    /// <param name="gameType"></param>
    public GameLogic(BlockController blockController, Constants.GameType gameType)
    {
        this.blockController = blockController;

        // ������ ���� ���� �ʱ�ȭ
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // GameType �ʱ�ȭ
        switch(gameType)
        {
            case Constants.GameType.SinglePlay:
                break;
            case Constants.GameType.DualPlay:
                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }

    // ���� �ٲ� ��, ���� �����ϴ� ���¸� Exit �ϰ�
    // �̹� ���� ���¸� currentPlayerState�� �Ҵ��ϰ�
    // �̹� ���� ���¿� Enter ȣ��
    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(gameLogic : this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(gameLogic : this);
    }
}
