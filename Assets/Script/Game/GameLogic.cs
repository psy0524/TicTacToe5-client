
public class GameLogic
{
    public BlockController blockController;

    private Constants.PlayerType[,] _board; // 보드의 상태 정보

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B
    private BasePlayerState _currentPlayerState; // 현재 턴의 Player
    
    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="blockController"></param>
    /// <param name="gameType"></param>
    public GameLogic(BlockController blockController, Constants.GameType gameType)
    {
        this.blockController = blockController;

        // 보드의 상태 정보 초기화
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // GameType 초기화
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

    // 턴이 바뀔 때, 기존 진행하던 상태를 Exit 하고
    // 이번 턴의 상태를 currentPlayerState에 할당하고
    // 이번 턴의 상태에 Enter 호출
    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(gameLogic : this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(gameLogic : this);
    }
}
