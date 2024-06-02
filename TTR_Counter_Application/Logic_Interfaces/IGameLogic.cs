using TTR_Counter_Domain.Model;

namespace TTR_Counter_Application.Logic_Interfaces;

public interface IGameLogic
{
    Task<Game> StartGame(List<Player> players);
    
    Task<Game> TakeConnections(Game game);
    Task<Game> PickTrainCards(Game game);
    Task<Game> PlaceTrainStation(Game game);
    Task<Game> PlaceTrains(Game game, int trainLength);
    Task<Game> UndoMove(Game game);
    Game NextPlayer(Game game);
}