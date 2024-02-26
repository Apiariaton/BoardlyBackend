using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSharpBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    BoardGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoardGameName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardGameDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardGamePrice = table.Column<float>(type: "real", nullable: false),
                    BoardGameBuyUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardGameGenre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.BoardGameId);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    playerAttacksPerRound = table.Column<float>(type: "real", nullable: false),
                    opponentAttacksPerRound = table.Column<float>(type: "real", nullable: false),
                    matchDurationInSeconds = table.Column<int>(type: "int", nullable: false),
                    playerWonThisMatch = table.Column<bool>(type: "bit", nullable: false),
                    chosenBoardGame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chosenBoardGameGenre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                });

            migrationBuilder.InsertData(
                table: "BoardGames",
                columns: new[] { "BoardGameId", "BoardGameBuyUrl", "BoardGameDescription", "BoardGameGenre", "BoardGameName", "BoardGamePrice" },
                values: new object[,]
                {
                    { new Guid("153c612a-4192-4b05-9d1b-565f1323aaa5"), "https://www.amazon.co.uk/Peradix-Folding-Storage-Portable-Travel/dp/B07RHL9FKQ/ref=sr_1_6?crid=3BWJXI3GNTMDS&dib=eyJ2IjoiMSJ9.MGCUlR5fTs5U5nrj7VMorlCiPGUuk2UaAujZd8Dy4XhzD0LSrMKfwK26Z8cjbTTroItr5Vu_CbgoieYhjHDBL2LXoHp8-RHp0Yo73ApejIRnh3dEIwXG3HeSFwz1CDICT46x9RvRdvlLM6fnka9rcf9Kd46C0jQcdJeKVGuelX5_eG5vPeedRBgfmjf34H5YbW5hIBF5cuJQigl29_Vs2DeA9H783q60ko26SYjx_7V0a46QLGi3lA08AOzhw3e7ehJ9n8Gp2IonhU79M6LCkaXM_CzGGGwieXJNPJWOfkE.jrO6bHB8m2Fxt4dqFUQtJP46lwT4Isn0of3XJWrw02M&dib_tag=se&keywords=Chess&qid=1708273703&sprefix=chess%2Caps%2C162&sr=8-6", "An ancient strategy game played on an 8x8 grid where players aim to capture the opponent's king.", "Strategy", "Chess", 10.98f },
                    { new Guid("7532dec8-9115-4d21-bae3-fa1db4858fa7"), "https://www.amazon.co.uk/Plan-Games-PBG40020-Azul-Board/dp/B077MZ2MPW/ref=sr_1_2?crid=2232U5CQV57HJ&dib=eyJ2IjoiMSJ9.29hanTRnrSsAxFKnlHMwQD7gHkv-65-Dwl6nwxjXSaRhXOAHvULC5175EXy-dmUIXnoCQfc6Gg4O-W-acdEisYpNLB9tbanOhes31QRxDKlrALwgKC-oqDste7RejxZN3kqy8MWfLMZIEZjtSsUlMI89tJYBkzrBnfDOiINyuXv1U9q2iCbdSkYB0WUUErT7KeGZVpgYrNn-FZ3cozHEZ4RR2nuooKgU5gFxgN9k9EZ7SONsvVK9F5e_4YvvseyZ3UK92mxAOLmD-aiaumaR3FQj3b_jIxVIoSAMLewsCUI.WX5WllJZqtpPyEh2nMs_ej6XRkNixdque5yApD2f2wI&dib_tag=se&keywords=Azul&qid=1708273804&sprefix=azul%2Caps%2C203&sr=8-2", "A tile-placement game where players compete to create the most beautiful mosaic.", "Abstract", "Azul", 38f },
                    { new Guid("76eaeee7-82e4-4269-aaa1-aee419ca334b"), "https://www.amazon.co.uk/Scrabble-Orginal-Y9592-Board-Game/dp/B00DE6FZCK/ref=sr_1_5?crid=2AIYMIRTL7SMF&dib=eyJ2IjoiMSJ9.GeNOi-M2ii8ZlK38V-0nRINxQsGfWZR6DcRLqIgQGy5SS3tE9jxZWVcMvw7XKZpXHlpmw857zzDCQOlxOtfTi7uDNbm8qTvVbWcMdLsuhqBQ2RWInM9aUaqeFqqml7LKN3hKvGx8bs0-Fmrr4CP6aG0RdJHiSaqcgcZq2GsR5qr1HbHFvrht3GtyTVKuGV2eLssiZQ3VqmM3zcMxK3Oc8oUhrquYFpmoS_n9x3OsS1PJHFNFGJNKDBveW6SxOki3vdw4F2lFqPOAdYFoRtbKsp3AvpOXezYsELMdeduLENo.ArahGw7mcVOggloG9zA0lL9LluIJU6GyuJl00xJp-wk&dib_tag=se&keywords=Scrabble&qid=1708273746&sprefix=scrabbl%2Caps%2C158&sr=8-5", "A word game where players form words crossword-style on a game board using letter tiles.", "Word", "Scrabble", 13.99f },
                    { new Guid("afdbe0a4-9f3c-48ff-aaee-fb2e206e48ab"), "https://www.example.com/codenames", "A social word game where players give one-word clues to guess words related to their team's color.", "Party", "Codenames", 34.65f },
                    { new Guid("b8cbbc78-98e2-4d31-9ebb-2016ac7c5431"), "https://www.amazon.co.uk/Hasbro-B7404-Gaming-Risk-Game/dp/B01BKBXQ58/ref=sr_1_5_mod_primary_new?crid=3VRUWI3ONX9V0&dib=eyJ2IjoiMSJ9.Q3iJBFVmOUW6WZSVOmzzsMfXO2cuPiUGyBFA8QKEEZPoo3MenxGxwRsIYP70w8l6P4LNaij_HkiirBXOjS12JxbQDpZ6Cayiw0nHh7_Hc3afQw9BT80lFvkGRo81fxb1L87KS19gNauZ-bnFhIhs-bkschxXyO7DCXSvPbp3HkKPevQklVvsc2-4ImiPGpUuqHo9Wasarzl2A91kx8og020iA14LL-BAaCI9FyduBtLU__LZyhvlrmwHf9gGTk4yIVR72SfN21j8SH1UKWGVJwkZuwzJe4r7OvM3mZm_gzs.StMepnGUTtFgtnlXI6mmosvvn3rKVutyVblIYw3GLso&dib_tag=se&keywords=Risk&qid=1708273773&sbo=RZvfv%2F%2FHxDF%2BO5021pAnSA%3D%3D&sprefix=risk%2Caps%2C174&sr=8-5", "A classic strategy game of global domination where players aim to conquer territories.", "Strategy", "Risk", 30f },
                    { new Guid("c44c6a3a-5949-4646-ab24-bada68c0b48f"), "https://www.amazon.co.uk/Days-Wonder-DOW7202-Ticket-Europe/dp/B000809OAO/ref=sr_1_5?crid=2WCWLAZEAZF7Z&dib=eyJ2IjoiMSJ9.4zDgNd6hTt5_2qcez047EQtMDvQeNsPNZ7uAVqupAPtoQE7TaMTJZHeuOIw5zcozB79otlLERcXAbIaRE778Ed6IeOcFLyCalB0SXzzbWZZrUjlrW8mUF5Gw0_Tn40XlUw3fmZdGIdifmmFHhuB3Oo6wcQ03InnOX5Mk9sNhC-qh71JwfYunzQosddMIUXoUx22bVvSX21VbG76XiCh8zFUFc-tYUkuPl26n1KhYiLjCuCsVnyJX1LkHfVFAZSXHRMHzxBClyFpsoLnDIYud6tPWdkbicfqlWI7Ba021CMI.vTcOPhtzT_EzDLpR5btzNP2YVpzTj33SlcvwWnVYfeo&dib_tag=se&keywords=ticket+to+ride&qid=1708273643&sprefix=t%2Caps%2C509&sr=8-5", "A railroad-themed board game where players build train routes across North America.", "Family", "Ticket to Ride", 39.3f },
                    { new Guid("e36cb2a2-ebb3-4abc-9ddb-1827b6babe23"), "https://www.amazon.co.uk/Catan-Studios-Players-Minutes-Playing/dp/B00U26V4VQ/ref=sr_1_5_mod_primary_new?crid=1C2I9L23IID20&dib=eyJ2IjoiMSJ9.KWhUK3nPlqHdLpYUqf_VrI6Q45WjWGwA4E1KmYgr4lrmrq6RTFslUGsMgB_KmdedtqFsuidSVYetyH4V0W3GfTZNpiH6JAdPEwJmED7kjTJ0r8LFOmDvx_dFhSJ9bhvf-qoHj8WLNOUFbIjWDsQDkNypcPsFxm6tNJ3jEdAygZ7P6V-ZGtNi1TLd3qaytAI9yRwpwMP_GiKx8Jlhs97nm4PsXq6_NoXyou36qPmxgZ-0owOo7jem6kmKZ1K8VTYssuIzvpITG4aVP1VBAujG-Xt9X_P2beBaJzffLVdJQoQ.A-IlRjjYrYLOTBDNdgAGDrCcC8xKUrudtKs3ULgaf5s&dib_tag=se&keywords=catan&qid=1708273421&sbo=RZvfv%2F%2FHxDF%2BO5021pAnSA%3D%3D&sprefix=cast%2Caps%2C197&sr=8-5", "A classic strategy game where players collect resources and build settlements.", "Strategy", "Settlers of Catan", 34f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "Matches");
        }
    }
}
