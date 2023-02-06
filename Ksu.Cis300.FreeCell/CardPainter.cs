/* CardPainter.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.FreeCell
{
    /// <summary>
    /// Contains constants and static methods/properties for drawing cards.
    /// </summary>
    public static class CardPainter
    {
        /// <summary>
        /// The height of a single card image from the input files.
        /// </summary>
        private const int _cardImageHeight = 333;

        /// <summary>
        /// The width of a single card image from the input files.
        /// </summary>
        private const int _cardImageWidth = 234;

        /// <summary>
        /// The pen used to draw the box where the stock will be placed.
        /// </summary>
        private static Pen _boxPen = new Pen(Color.White);

        /// <summary>
        /// The pen used to highlight selected cards.
        /// </summary>
        private static Pen _highlightPen = new Pen(Color.Magenta, 2);

        /// <summary>
        /// The height of a displayed card drawing.
        /// </summary>
        public static readonly int CardHeight = _cardImageHeight / 3;

        /// <summary>
        /// The width of a displayed card drawing.
        /// </summary>
        public static readonly int CardWidth = _cardImageWidth / 3;

        /// <summary>
        /// Draws the given card on the given graphics context at the given y-coordinate.
        /// </summary>
        /// <param name="c">The card to draw.</param>
        /// <param name="g">The graphics context on which to draw.</param>
        /// <param name="y">The y-coordinate of the upper-left corner.</param>
        public static void DrawCard(Card c, Graphics g, int y)
        {
            g.DrawImage(c.Picture, 0, y, CardWidth, CardHeight);
        }

        /// <summary>
        /// Draws a box the size of a card at the top of the given graphics context.
        /// </summary>
        /// <param name="g">The graphics context on which to draw.</param>
        public static void DrawBox(Graphics g)
        {
            g.DrawRectangle(_boxPen, 0, 0, CardWidth, CardHeight);
        }

        /// <summary>
        /// Draws a highlight box of the given height at the given y-coordinate of the given
        /// graphics context.
        /// </summary>
        /// <param name="g">The graphics context on which to draw.</param>
        /// <param name="y">The y-coordinate of the upper-left corner.</param>
        /// <param name="height">The height of the box.</param>
        public static void DrawHighlight(Graphics g, int y, int height)
        {
            g.DrawRectangle(_highlightPen, 0, y, CardWidth, height);
        }
    }
}
