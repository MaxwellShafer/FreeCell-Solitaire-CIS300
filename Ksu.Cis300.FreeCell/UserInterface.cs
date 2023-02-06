/* UserInterface.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.FreeCell
{
    /// <summary>
    /// A GUI for a program to play FreeCell Solitaire.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// The minimum gap between the free cells and the home cells.
        /// </summary>
        private const int _minimumTopGap = 10;

        /// <summary>
        /// The default padding between controls.
        /// </summary>
        private const int _defaultPadding = 3;

        /// <summary>
        /// The the vertical padding around the top panel and the tableau panel.
        /// </summary>
        private const int _mainPadding = 5;

        /// <summary>
        /// The game logic controller.
        /// </summary>
        private Game _game = new Game();

        /// <summary>
        /// Constructs the GUI.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
            InitializeBoard();
        }

        /// <summary>
        /// Initializes the free cell panel.
        /// </summary>
        private void InitializeFreeCellPanel()
        {
            uxFreeCellPanel.Margin = new Padding(0, 0, 0, 0);
            foreach (CardLocation loc in _game.FreeCells)
            {
                loc.Margin = new Padding(_defaultPadding);
                uxFreeCellPanel.Controls.Add(loc);
                loc.Click += new EventHandler(FreeCellClick);
            }
        }

        /// <summary>
        /// Initializes the home panel.
        /// </summary>
        private void InitializeHomePanel()
        {
            foreach (CardLocation loc in _game.HomeCells)
            {
                loc.Margin = new Padding(_defaultPadding);
                uxHomePanel.Controls.Add(loc);
                loc.Click += new EventHandler(HomeCellClick);
            }
        }

        /// <summary>
        /// Initializes the tableau panel so that its width is at least the given minimum.
        /// </summary>
        /// <param name="minWidth">The minimum width of the tableau panel.</param>
        /// <returns>The left margin of the panel.</returns>
        private int InitializeTableauPanel(int minWidth)
        {
            int usedSpace = _game.TableauColumns.Length * _game.TableauColumns[0].Width;
            int gaps = (minWidth - usedSpace) / _game.TableauColumns.Length + 1;
            foreach (TableauColumn col in _game.TableauColumns)
            {
                col.Margin = new Padding(gaps, 0, 0, 0);
                uxTableauPanel.Controls.Add(col);
                col.MouseClick += new MouseEventHandler(TableauColumnMouseClick);
            }
            return gaps;
        }

        /// <summary>
        /// Initializes the board controls.
        /// </summary>
        private void InitializeBoard()
        {
            InitializeFreeCellPanel();
            InitializeHomePanel();
            int tableauLeftMargin = InitializeTableauPanel(uxFreeCellPanel.Width + uxHomePanel.Width + _minimumTopGap);

            // Set the margins of the panels so that they line up correctly.
            uxTopPanel.Margin = new Padding(tableauLeftMargin - _defaultPadding, _defaultPadding, _defaultPadding, _mainPadding);
            uxTableauPanel.Margin = new Padding(0, _mainPadding, 0, 0);
            int topGap = uxTableauPanel.Width - uxFreeCellPanel.Width - uxHomePanel.Width - _defaultPadding + 1;
            uxHomePanel.Margin = new Padding(topGap, 0, 0, 0);
        }

        /// <summary>
        /// Handles a Click event on the NumericUpDown.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxNewGame_Click(object sender, EventArgs e)
        {
            _game.StartNewGame((int)uxSeed.Value);
        }

        /// <summary>
        /// Handles a Click event on one of the free cells.
        /// </summary>
        /// <param name="sender">The free cell that was clicked.</param>
        /// <param name="e">Information about the event.</param>
        private void FreeCellClick(object sender, EventArgs e)
        {
            _game.ClickFreeCell((CardLocation)sender);
        }

        /// <summary>
        /// Handles a Click event on one of the home cells.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeCellClick(object sender, EventArgs e)
        {
            _game.ClickHomeCell((CardLocation)sender);
        }

        /// <summary>
        /// Handles a MouseClick event on one of the tableau columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TableauColumnMouseClick(object sender, MouseEventArgs e)
        {
            TableauColumn col = (TableauColumn)sender;

            // We only handle clicks that are on a card or an empty column.
            // e.Y gives the vertical distance from the top of the control to the click location.
            int n = col.NumberAbove(e.Y);
            if (n > 0 || (n == 0 && e.Y < CardPainter.CardHeight))
            {
                _game.ClickColumn(col, n);
            }
        }

        /// <summary>
        /// Handles a Click event on the "Move All Home" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxMoveAllHome_Click(object sender, EventArgs e)
        {
            _game.MoveAll();
        }
    }
}
