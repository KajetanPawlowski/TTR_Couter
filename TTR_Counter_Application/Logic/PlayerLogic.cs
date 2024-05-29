using TTR_Counter_Application.Logic_Interfaces;
using TTR_Counter_Domain.Model;

namespace TTR_Counter_Application.Logic;

public class PlayerLogic : IPlayerLogic
{
    string[] _allowedColors = { "Red", "Blue", "Green", "Yellow", "Black", "Grey" };

    public Task<Player> CreatePlayer(string login)
    {
        return Task.FromResult(new Player(login, "Grey"));
    }

    public Task<Player> SelectColor(Player player, string color)
    {
        if (Array.Exists(_allowedColors, c => c.Equals(color, StringComparison.OrdinalIgnoreCase)))
        {
            player.Color = color;
        }
        else
        {
            throw new ArgumentException("Invalid colour. Allowed colours are: Red, Blue, Green, Yellow, Black, White.");
        }

        return Task.FromResult(player);
    }
}