/*
 * Corie Beale
 * IGME 206
 * E7 and E8
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tray_Model
{
    internal class Tray
    {
        /*
         * * Algorithm plan * *
         * Add property Items that the Tray holds - must be a value between 0 and 5
         *      - can get values, not set
         * Add remove Item method to clean Tray
         */

        private Random rand = new Random();

        /// <summary>
        /// Create a Tray object with a random number of items between 0 and 5, inclusive.
        /// </summary>
        public Tray()
        {
            Items = rand.Next(0, 6);
        }

        /// <summary>
        /// The number of items on the tray.
        /// </summary>
        public int Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Removes an item from the tray.
        /// </summary>
        /// <returns>true if successful, false if nothing was removed.</returns>
        public bool RemoveItems(int items)
        {
            if (Items == 0) { return false; }
            if (items > Items) { Items = 0; return true; }
            Items -= items;
            return true;
        }

        /// <summary>
        /// Empties the tray to have no items.
        /// </summary>
        public Tray Empty()
        {
            Items = 0;
            return this;
        }
    }
}
