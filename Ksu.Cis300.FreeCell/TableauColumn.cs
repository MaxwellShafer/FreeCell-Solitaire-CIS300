/* TableauColumn.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.FreeCell
{
    /// <summary>
    /// A drawing of a column of cards on the tableau.
    /// </summary>
    public partial class TableauColumn : UserControl
    {
        /// <summary>
        /// The vertical offset between cards on the column.
        /// </summary>
        private static readonly int _verticalOffset = CardPainter.CardHeight / 5;

        /// <summary>
        /// The height of this control.
        /// </summary>
        private static readonly int _columnHeight = _verticalOffset * 17 + CardPainter.CardHeight + 1;

        /// <summary>
        /// Gets the cards contained in this column.
        /// </summary>
        public Stack<Card> Column { get; } = new Stack<Card>();
        
        /// <summary>
        /// The number of cards selected on this column.
        /// </summary>
        private int _numberSelected;

        /// <summary>
        /// Gets or set the number of cards selected on this column.
        /// If a negative value or a value greater than the number of cards is assigned,
        /// an ArgumentOutOfRangeException is thrown.
        /// </summary>
        public int NumberSelected
        {
            get
            {
                return _numberSelected;
            }
            set
            {
                if (value < 0 || value > Column.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _numberSelected = value;
            }
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        public TableauColumn()
        {
            InitializeComponent();
            Width = CardPainter.CardWidth + 1;
            Height = _columnHeight;
        }

        /// <summary>
        /// Finds the number of cards on top of the given y-coordinate, provided this
        /// coordinate is on a card; otherwise, returns 0.
        /// </summary>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>The number of cards on top of y, or 0 if y is not on a card.</returns>
        public int NumberAbove(int y)
        {
            if (Column.Count == 0 || 
                y > (Column.Count - 1) * _verticalOffset + CardPainter.CardHeight)
            {
                return 0;
            }
            else if (y >= Column.Count * _verticalOffset)
            {
                return 1;
            }
            return Column.Count - y / _verticalOffset;
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        /// <param name="e">Data concerning the graphics environment.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // This method redefines the OnPaint method defined within the UserControl class,
            // which is the super-type (or parent) of this class. The following line ensures
            // that everything done by the overridden method is done here.
            base.OnPaint(e);

            Graphics g = e.Graphics;
            CardPainter.DrawBox(g);
            int y = 0;
            Card[] a = Column.ToArray();
            for (int i = a.Length - 1; i >= 0; i--)
            {
                CardPainter.DrawCard(a[i], g, y);
                y += _verticalOffset;
            }
            if (_numberSelected > 0)
            {
                int boxHeight = CardPainter.CardHeight + (_numberSelected - 1) * _verticalOffset;
                int boxStart = (Column.Count - _numberSelected) * _verticalOffset;
                CardPainter.DrawHighlight(g, boxStart, boxHeight);
            }
        }

    }
}
