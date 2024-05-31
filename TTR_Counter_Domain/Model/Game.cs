namespace TTR_Counter_Domain.Model;

public class Game
{
    public int GameId { get; set; }
    public List<Player> Players { get; set; }
    public string GameState { get; set; }
    
    public List<Move> Moves { get; set; }
    
    public Player CurrentPlayer { get; set; }
    
    public int MovesLeft { get; set; }

    public Game(int gameId, List<Player> players)
    {
        GameId = gameId;
        Players = players;
        GameState = "Play"; // LastRound, Finished
        Moves = new List<Move>();
        MovesLeft = players.Count;
        CurrentPlayer = players[0];
    }
}