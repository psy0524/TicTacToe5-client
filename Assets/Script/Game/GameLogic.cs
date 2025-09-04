
using UnityEngine;

public class GameLogic
{
    public BlockController blockController;

    private Constants.PlayerType[,] _board; // 보드의 상태 정보

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B

    public enum GameResult { None, Win, Lose, Draw}
    private BasePlayerState _currentPlayerState; // 현재 턴의 Player
    
    
    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="blockController"></param>
    /// <param name="gameType"></param>
    public GameLogic(BlockController blockController, Constants.GameType gameType) // 생성자
    {
        this.blockController = blockController;

        // 보드의 상태 정보 초기화
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // GameType 초기화
        switch(gameType)
        {
            case Constants.GameType.SinglePlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new AIState();

                SetState(firstPlayerState);
                break;

            case Constants.GameType.DualPlay:
                firstPlayerState = new PlayerState(true);
                secondPlayerState = new PlayerState(false);

                // 게임 시작
                SetState(firstPlayerState);

                break;
            case Constants.GameType.MultiPlay:
                break;
        }
    }

    public Constants.PlayerType[,] GetBoard()
    {
        return _board;
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

    // _board 배열에 새로운 Marker 값을 할당
    public bool SetNewBoardValue(Constants.PlayerType playerType, int row, int col)
    {
        if (_board[row, col] != Constants.PlayerType.None) return false;

        if (playerType == Constants.PlayerType.PlayerA)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarkerType.O, row, col);
            return true;
        }
        else if(playerType == Constants.PlayerType.PlayerB)
        {
            _board[row, col] = playerType;
            blockController.PlaceMarker(Block.MarkerType.X, row, col);
            return true;
        }
        return false;
    }



    // 게임의 결과 확인
    

    // Game Over 처리
    public void EndGame(GameResult gameResult)
    {
        // TODO : Game Logic 정리
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        // 유저에게 Game Over 표시
        GameManager.Instance.OpenConfirmPanel("게임오버", () =>
        {
            GameManager.Instance.ChangeToMainScene();
        });
    }

    public GameResult CheckGameResult()
    {
        if(TicTacToeAI.CheckGameWin(Constants.PlayerType.PlayerA, _board)) { return GameResult.Win; }
        if(TicTacToeAI.CheckGameWin(Constants.PlayerType.PlayerB, _board)) { return GameResult.Lose; }
        if(TicTacToeAI.CheckGameDraw(_board)) { return GameResult.Draw; }
        return GameResult.None;
    }

}
