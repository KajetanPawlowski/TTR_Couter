namespace TTR_Counter_Domain.Model;

public class Move
{
    public string MoveType { get; set; }
    public int MovePoints { get; set; }
    public Player Player { get; set; }
        
    public Move(string moveType, int movePoints, Player player)
    {
        MoveType = moveType;
        MovePoints = movePoints;
        Player = player;
    }

    public override string ToString()
    {
        return Player.Login + " - " + MoveType;
    }
}