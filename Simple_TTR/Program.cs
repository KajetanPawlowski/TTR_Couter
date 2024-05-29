// Create the player logic and game logic instances

using TTR_Counter_Application.Logic;
using TTR_Counter_Application.Logic_Interfaces;
using TTR_Counter_Domain.Model;

IPlayerLogic playerLogic = new PlayerLogic();
        IGameLogic gameLogic = new GameLogic();

        // Create a list to hold the players
        List<Player> players = new List<Player>();
    
int currentPlayerIndex = 0;
Console.WriteLine("How Many Players:");
int playersCount = int.Parse(Console.ReadLine());

        // Create five players
        for (int i = 1; i <= playersCount; i++)
        {
            string login = $"Player{i}";
            Player player = await playerLogic.CreatePlayer(login);
            players.Add(player);
        }

        try
        {
            // Start the game
            Game game = await gameLogic.StartGame(players);
            Console.WriteLine($"Game {game.GameId} started with {game.Players.Count} players.");
            
            // Loop until the game state is "Finished"
            while (game.GameState != "Finished")
            {
                int playerNumber = currentPlayerIndex + 1;
                // Prompt the user to select a move
                Console.WriteLine($"Player {playerNumber} Select a move:");
                Console.WriteLine("1. Take Connections");
                Console.WriteLine("2. Pick Train Cards");
                Console.WriteLine("3. Place Train Station");
                Console.WriteLine("4. Place Trains");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                // Perform the selected move
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Taking Connections...");
                        await gameLogic.TakeConnections(game, game.Players[currentPlayerIndex]); // For simplicity, always use the first player
                        NextPlayerIndex(currentPlayerIndex);
                        break;
                    case 2:
                        Console.WriteLine("Picking Train Cards...");
                        await gameLogic.PickTrainCards(game, game.Players[currentPlayerIndex]); // For simplicity, always use the first player
                        NextPlayerIndex(currentPlayerIndex);
                        break;
                    case 3:
                        Console.WriteLine("Placing Train Station...");
                        await gameLogic.PlaceTrainStation(game, game.Players[currentPlayerIndex]); // For simplicity, always use the first player
                        NextPlayerIndex(currentPlayerIndex);
                        break;
                    case 4:
                        Console.WriteLine("Placing Trains...");
                        Console.Write("Enter the length of the train: ");
                        int trainLength = int.Parse(Console.ReadLine());
                        await gameLogic.PlaceTrains(game, game.Players[currentPlayerIndex], trainLength); // For simplicity, always use the first player
                        NextPlayerIndex(currentPlayerIndex);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
                Console.WriteLine($"\nUpdated Game State: {game.GameState}");
                Console.WriteLine("Players:");
                foreach (var player in game.Players)
                {
                    Console.WriteLine($"Player: {player.Login}, Points: {player.Points}, Stations: {player.StationCount}, Trains: {player.TrainCount}");
                }
            }
            // Display game status after the move
            
            Console.WriteLine($"Game ID: {game.GameId}");
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

void NextPlayerIndex(int currentIndex)
{
    if (currentIndex >= players.Count - 1)
    {
        currentPlayerIndex = 0;
    }
    else
    {
        currentPlayerIndex++;
    }
}