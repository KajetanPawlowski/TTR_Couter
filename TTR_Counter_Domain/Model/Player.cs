namespace TTR_Counter_Domain.Model;

public class Player
{
    public string Login { get; set; }
    public string Color { get; set; }
    public int TrainCount { get; set; }
    public int StationCount { get; set; }
    public int Points { get; set; }

    public Player(string login, string color)
    {
        Login = login;
        Color = color;
        TrainCount = 45;
        Points = 0;
        StationCount = 3;
    }
}