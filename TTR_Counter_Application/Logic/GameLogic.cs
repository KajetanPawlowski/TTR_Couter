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
        return Task.FromResult(NextPlayer(game));
    }

    public Task<Game> PickTrainCards(Game game)
    {
        game.Moves.Add(new Move("PickCards", 0, game.CurrentPlayer));
        return Task.FromResult(NextPlayer(game));
    }

    public Task<Game> PlaceTrainStation(Game game)
    {
        if (game.CurrentPlayer.StationCount < 1)
        {
            throw new Exception("You dont have any stations left!");
        }

        game.Moves.Add(new Move("PlaceStation", 0, game.CurrentPlayer,0,1));
        game.Players.FirstOrDefault(p => p.Login.Equals(game.CurrentPlayer.Login))!.StationCount--;
        return Task.FromResult(NextPlayer(game));
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
            case 1:
                moveScore = 1;
                break;
            case 2:
                moveScore = 2;
                break;
            case 3:
                moveScore = 4;
                break;
            case 4:
                moveScore = 7;
                break;
            case 6:
                moveScore = 15;
                break;
            case 8:
                moveScore = 21;
                break;
        }
        game.Moves.Add(new Move("BuildTrains", moveScore, game.CurrentPlayer, trainLength,0));
        game.Players.FirstOrDefault(p => p.Login.Equals(game.CurrentPlayer.Login))!.TrainCount-=trainLength;
        game.Players.FirstOrDefault(p => p.Login.Equals(game.CurrentPlayer.Login))!.Points+=moveScore;
        
        return Task.FromResult(NextPlayer(game));
    }

    public Task<Game> UndoMove(Game game)
    {
        if (game.Moves.Any())
        {
            Move lastMove = game.Moves.Last();
            game.Moves.Remove(lastMove);
            game.Players.FirstOrDefault(p => p.Login.Equals(lastMove.Player.Login))!.TrainCount+=lastMove.TrainsUsed;
            game.Players.FirstOrDefault(p => p.Login.Equals(lastMove.Player.Login))!.Points-=lastMove.MovePoints;
            game.Players.FirstOrDefault(p => p.Login.Equals(lastMove.Player.Login))!.StationCount+=lastMove.StationUsed;
            game.CurrentPlayer = game.Players.FirstOrDefault(p => p.Login.Equals(lastMove.Player.Login))!;
            if (game.GameState.Equals("LastRound"))
            {
                game.MovesLeft++;
            }
            return Task.FromResult(CheckGameStatus(game));
        }

        throw new Exception("Cannot Undo Move");
    }

    private Game CheckGameStatus(Game game)
    {
        if (game.Players.Any(p => p.TrainCount <= 2))
        {
            game.GameState = "LastRound";
        }
        else
        {
            game.GameState = "Play";
        }
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
        
        return game;
    }

    public Game NextPlayer(Game game)
    {
        int index = game.Players.IndexOf(game.CurrentPlayer);
        if (index == game.Players.Count - 1)
        {
            game.CurrentPlayer = game.Players[0];
            return  CheckGameStatus(game);
        }
        else
        {
            game.CurrentPlayer = game.Players[index + 1];
            return CheckGameStatus(game);
        }
    }
}