using TTR_Counter_Domain.Model;

namespace TTR_Counter_Application.Logic_Interfaces;

public interface IGameLogic
{
    Task<Game> StartGame(List<Player> players);
    
    Task<Game> TakeConnections(Game game, Player player);
    Task<Game> PickTrainCards(Game game, Player player);
    Task<Game> PlaceTrainStation(Game game, Player player);
    Task<Game> PlaceTrains(Game game, Player player, int trainLength);
}