/* Game.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Ksu.Cis300.FreeCell
{
    /// <summary>
    /// Conrols the game logic for a FreeCell game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The number of free cells.
        /// </summary>
        private const int _freeCellCount = 4;

        /// <summary>
        /// The number of home cells.
        /// </summary>
        private const int _homeCellCount = 4;

        /// <summary>
        /// The number of tableau columns.
        /// </summary>
        private const int _tableauColumnCount = 8;

        /// <summary>
        /// Gets the free cells.
        /// </summary>
        public CardLocation[] FreeCells { get; } = new CardLocation[_freeCellCount];

        /// <summary>
        /// Gets the home cells.
        /// </summary>
        public CardLocation[] HomeCells { get; } = new CardLocation[_homeCellCount];

        /// <summary>
        /// Gets the tableau columns.
        /// </summary>
        public TableauColumn[] TableauColumns { get; } = new TableauColumn[_tableauColumnCount];

        /// <summary>
        /// Constructs a Game.
        /// </summary>
        public Game() 
        { 
            for (int i = 0; i < FreeCells.Length; i++)
            {
                FreeCells[i] = new CardLocation();
            }
            for (int i = 0; i < HomeCells.Length; i++)
            {
                HomeCells[i] = new CardLocation();
            }
            for (int i = 0; i < TableauColumns.Length; i++)
            {
                TableauColumns[i] = new TableauColumn();
            }
        }

        /// <summary>
        /// Starts a new game using the given seed.
        /// </summary>
        /// <param name="seed">The seed to use to shuffle the cards.</param>
        public void StartNewGame(int seed)
        {

        }

        /// <summary>
        /// Reacts to the user's click on a free cell.
        /// </summary>
        /// <param name="loc">The chosen free cell.</param>
        public void ClickFreeCell(CardLocation loc)
        {

        }

        /// <summary>
        /// Reacts to the user's click on a tableau column.
        /// </summary>
        /// <param name="col">The column clicked.</param>
        /// <param name="n">The number of cards chosen.</param>
        public void ClickColumn(TableauColumn col, int n)
        {

        }

        /// <summary>
        /// Reacts to the user's clicking a home cell.
        /// </summary>
        /// <param name="loc">The home cell that was clicked.</param>
        public void ClickHomeCell(CardLocation loc)
        {

        }

        /// <summary>
        /// Moves all possible cards to home cells.
        /// </summary>
        public void MoveAll()
        {

        }
    }
}
