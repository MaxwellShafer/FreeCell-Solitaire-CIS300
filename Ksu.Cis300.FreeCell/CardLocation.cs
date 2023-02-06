/* CardLocation.cs
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
    /// A drawing of a location of a single visible card. It may or may not contain a card.
    /// </summary>
    public partial class CardLocation : UserControl
    {
        /// <summary>
        /// Gets or sets the card at this location, where null represents no card.
        /// </summary>
        public Card Card { get; set; }

        /// <summary>
        /// Indicates whether the card at this location is selected.
        /// </summary>
        private bool _isSelected;

        /// <summary>
        /// Gets or sets whether the card at this location is selected.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (value && Card == null)
                {
                    throw new InvalidOperationException();
                }
                _isSelected = value;
            }
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        public CardLocation()
        {
            InitializeComponent();
            Width = CardPainter.CardWidth + 1;
            Height = CardPainter.CardHeight + 1;
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        /// <param name="e">Data about the drawing context.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // This method redefines the OnPaint method defined within the UserControl class,
            // which is the super-type (or parent) of this class. The following line ensures
            // that everything done by the overridden method is done here.
            base.OnPaint(e);

            Graphics g = e.Graphics;
            int x = 0;
            CardPainter.DrawBox(g);
            if (Card != null)
            {
                CardPainter.DrawCard(Card, g, 0);
            }
            if (_isSelected)
            {
                CardPainter.DrawHighlight(g, 0, CardPainter.CardHeight);
            }
        }
    }
}
