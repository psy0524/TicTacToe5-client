
using UnityEngine;

public class GameLogic
{
    public BlockController blockController;

    private Constants.PlayerType[,] _board; // ������ ���� ����

    public BasePlayerState firstPlayerState; // Player A
    public BasePlayerState secondPlayerState; // Player B

    public enum GameResult { None, Win, Lose, Draw}
    private BasePlayerState _currentPlayerState; // ���� ���� Player
    
    
    /// <summary>
    /// ������
    /// </summary>
    /// <param name="blockController"></param>
    /// <param name="gameType"></param>
    public GameLogic(BlockController blockController, Constants.GameType gameType) // ������
    {
        this.blockController = blockController;

        // ������ ���� ���� �ʱ�ȭ
        _board = new Constants.PlayerType[Constants.BlockColumnCount, Constants.BlockColumnCount];

        // GameType �ʱ�ȭ
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

                // ���� ����
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

    // ���� �ٲ� ��, ���� �����ϴ� ���¸� Exit �ϰ�
    // �̹� ���� ���¸� currentPlayerState�� �Ҵ��ϰ�
    // �̹� ���� ���¿� Enter ȣ��
    public void SetState(BasePlayerState state)
    {
        _currentPlayerState?.OnExit(gameLogic : this);
        _currentPlayerState = state;
        _currentPlayerState?.OnEnter(gameLogic : this);
    }

    // _board �迭�� ���ο� Marker ���� �Ҵ�
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



    // ������ ��� Ȯ��
    

    // Game Over ó��
    public void EndGame(GameResult gameResult)
    {
        // TODO : Game Logic ����
        SetState(null);
        firstPlayerState = null;
        secondPlayerState = null;

        // �������� Game Over ǥ��
        GameManager.Instance.OpenConfirmPanel("���ӿ���", () =>
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
