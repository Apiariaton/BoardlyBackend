using Microsoft.EntityFrameworkCore;
using CSharpBackend.API.Controllers;
using CSharpBackend.API.Models.Domain;

namespace CSharpBackend.API.Data 
{

    public class CSharpBackendDBContext : DbContext
    {


        public CSharpBackendDBContext(DbContextOptions<CSharpBackendDBContext> dbContextOptions) : base(dbContextOptions)
        {



        }

        public DbSet<BoardGame> BoardGames {get;set;}


        public DbSet<PlayerVsPlayerDuel> PlayerVsPlayerDuels {get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var boardGamesToInitialiseDataset = new List<BoardGame>()
            {

            new BoardGame
            {
                BoardGameId = Guid.Parse("e36cb2a2-ebb3-4abc-9ddb-1827b6babe23"),
                BoardGameName = "Settlers of Catan",
                BoardGameDescription = "A classic strategy game where players collect resources and build settlements.",
                BoardGameBuyUrl = "https://www.google.com/search?q=settlers+of+catan",
                BoardGameGenre = "Strategy"
            },
            new BoardGame
            {
                BoardGameId = Guid.Parse("c44c6a3a-5949-4646-ab24-bada68c0b48f"),
                BoardGameName = "Ticket to Ride",
                BoardGameDescription = "A railroad-themed board game where players build train routes across North America.",
                BoardGameBuyUrl = "https://www.google.com/search?q=Ticket+to+Ride",
                BoardGameGenre = "Family"
            },
            new BoardGame
            {
                BoardGameId = Guid.Parse("153c612a-4192-4b05-9d1b-565f1323aaa5"),
                BoardGameName = "Chess",
                BoardGameDescription = "An ancient strategy game played on an 8x8 grid where players aim to capture the opponent's king.",
                BoardGameBuyUrl = "https://www.google.com/search?q=Chess+board+game",
                BoardGameGenre = "Strategy"
            },
            new BoardGame
            {
                BoardGameId = Guid.Parse("76eaeee7-82e4-4269-aaa1-aee419ca334b"),
                BoardGameName = "Scrabble",
                BoardGameDescription = "A word game where players form words crossword-style on a game board using letter tiles.",
                BoardGameBuyUrl = "https://www.google.com/search?q=Scrabble",
                BoardGameGenre = "Word"
            },
            new BoardGame
            {
                BoardGameId = Guid.Parse("b8cbbc78-98e2-4d31-9ebb-2016ac7c5431"),
                BoardGameName = "Risk",
                BoardGameDescription = "A classic strategy game of global domination where players aim to conquer territories.",
                BoardGameBuyUrl = "https://www.google.com/search?q=Risk+board+game",
                BoardGameGenre = "Strategy"
            },
            new BoardGame
            {
                BoardGameId = Guid.Parse("afdbe0a4-9f3c-48ff-aaee-fb2e206e48ab"),
                BoardGameName = "Codenames",
                BoardGameDescription = "A social word game where players give one-word clues to guess words related to their team's color.",
                BoardGameBuyUrl = "https://www.google.com/search?q=Codenames+board+game",
                BoardGameGenre = "Party"
            },
            new BoardGame
            {
                BoardGameId = Guid.Parse("7532dec8-9115-4d21-bae3-fa1db4858fa7"),
                BoardGameName = "Azul",
                BoardGameDescription = "A tile-placement game where players compete to create the most beautiful mosaic.",
                BoardGameBuyUrl = "https://www.google.com/search?q=Azul+Board_game",
                BoardGameGenre = "Abstract"
            },
        };


            modelBuilder.Entity<BoardGame>().HasData(boardGamesToInitialiseDataset);

        }

    }




}