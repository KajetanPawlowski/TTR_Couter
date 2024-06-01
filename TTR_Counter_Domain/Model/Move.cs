namespace TTR_Counter_Domain.Model;

public class Move
{
    public string MoveType { get; set; }
    public int MovePoints { get; set; }
    public int TrainsUsed { get; set; }
    public int StationUsed { get; set; }
    public Player Player { get; set; }
        
    public Move(string moveType, int movePoints, Player player)
    {
        MoveType = moveType;
        MovePoints = movePoints;
        Player = player;
        TrainsUsed = 0;
        StationUsed = 0;
    }
    public Move(string moveType, int movePoints, Player player, int trainsUsed, int stationUsed)
    {
        MoveType = moveType;
        MovePoints = movePoints;
        Player = player;
        TrainsUsed = trainsUsed;
        StationUsed = stationUsed;
    }

    public override string ToString()
    {
        return Player.Login + " - " + MoveType;
    }
}