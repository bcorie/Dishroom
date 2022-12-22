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
    internal class Worker
    {
        /*
         * Constructor creates a worker with a random efficiency between 85 and 100
         * Add a method to decrease the efficiency by a given valule
         */

        private int efficiency;
        private Random rand = new Random();

        /// <summary>
        /// Create a Worker object with an efficiency between 85 and 100.
        /// </summary>
        public Worker()
        {
            efficiency = rand.Next(85, 101);
        }

        public int Efficiency
        {
            get { return efficiency; }
        }

        /// <summary>
        /// Decreases the worker's efficiency.
        /// </summary>
        /// <param name="efficiency">the efficiency to decrease by, as a percent value.</param>
        public void DecreaseEfficiency(int efficiency)
        {
            this.efficiency -= efficiency;
        }

    }
}
