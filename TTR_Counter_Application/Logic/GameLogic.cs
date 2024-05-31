using System.Collections;
using TTR_Counter_Application.Logic_Interfaces;
using TTR_Counter_Domain.Model;

namespace TTR_Counter_Application.Logic;

public class GameLogic : IGameLogic
{
    public Task<Game> StartGame(List<Player> players)
    {
        if (players.Count < 2)
        {
            throw new Exception("You need to have friends!");
        }
        Random random = new Random();
        return Task.FromResult(new Game(random.Next(10000, 100000),players));
    }

    public Task<Game> TakeConnections(Game game)
    {
        game.Moves.Add(new Move("Connections", 0, game.CurrentPlayer));
        return Task.FromResult(CheckGameStatus(game));
    }

    public Task<Game> PickTrainCards(Game game)
    {
        game.Moves.Add(new Move("PickCards", 0, game.CurrentPlayer));
        return Task.FromResult(CheckGameStatus(game));
    }

    public Task<Game> PlaceTrainStation(Game game)
    {
        if (game.CurrentPlayer.StationCount < 1)
        {
            throw new Exception("You dont have any stations left!");
        }

        game.Moves.Add(new Move("PlaceStation", 0, game.CurrentPlayer));
        game.Players.FirstOrDefault(p => p.Login.Equals(game.CurrentPlayer.Login))!.StationCount--;
        return Task.FromResult(CheckGameStatus(game));
    }

    public Task<Game> PlaceTrains(Game game, int trainLength)
    {
        if (game.CurrentPlayer.TrainCount < trainLength)
        {
            throw new Exception("You dont have enough trains!");
        }

        int moveScore = 0;
        switch (trainLength)
        {
            case 2:
                moveScore = 2;
                break;
            case 3:
                moveScore = 4;
                break;
            case 4:
                moveScore = 7;
                break;
            case 5:
                moveScore = 10;
                break;
            case 6:
                moveScore = 15;
                break;
        }
        game.Moves.Add(new Move("BuildTrains", moveScore, game.CurrentPlayer));
        game.Players.FirstOrDefault(p => p.Login.Equals(game.CurrentPlayer.Login))!.TrainCount-=trainLength;
        game.Players.FirstOrDefault(p => p.Login.Equals(game.CurrentPlayer.Login))!.Points+=moveScore;

        if (game.Players.FirstOrDefault(p => p.Login.Equals(game.CurrentPlayer.Login))!.TrainCount <= 2)
        {
            game.GameState = "LastRound";
        }
        return Task.FromResult(CheckGameStatus(game));
    }

    private Game CheckGameStatus(Game game)
    {
        switch (game.GameState)
        {
            case "Play":
                break;
            case "LastRound":
                if (game.MovesLeft == 0)
                {
                    game.GameState = "Finished";
                }
                game.MovesLeft--;
                break;
            case "Finished":
                throw new Exception("Game Has Finished!");
        }

        game.CurrentPlayer = NextPlayer(game);
        return game;
    }

    public Player NextPlayer(Game game)
    {
        int index = game.Players.IndexOf(game.CurrentPlayer);
        if (index == game.Players.Count - 1)
        {
            return game.Players[0];
        }
        else
        {
            return game.Players[index + 1];
        }
    }
}