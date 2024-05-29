using TTR_Counter_Domain.Model;

namespace TTR_Counter_Application.Logic_Interfaces;

public interface IPlayerLogic
{
    Task<Player> CreatePlayer(string login);
    Task<Player> SelectColor(Player player, string color);
}