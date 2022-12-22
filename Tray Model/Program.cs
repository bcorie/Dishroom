/*
 * Corie Beale
 * IGME 206
 * E7 and E8
 */

using System.Transactions;
using Tray_Model;


/* * Algorithm Plan * *
 * Create the belt as a Queue of type Tray
 * Create empty Trays to add to the belt
 * Create array of 4 Workers
 * From the highest index to the lowest, the respective worker removes
 *  a number of garbage pieces in coordinance with their efficiency
 *      - removes between 2 and 6 items at a time
 *      - skipping items must be considered
 * Decrease all Worker's efficiency by 5(%)
 * Move the Trays on the belt down by one
 * Create a new, fresh Tray to the end of the belt with an item count between 0 and 5, inclusive
 * Continue looping until the Tray removed has Items remaining (not empty)
 * *
 */

namespace Tray_Model { 

    /// <summary>
    /// Runs the simulation/model. Handles events of the simulation.
    /// </summary>
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("Starting simulation.");  // DEBUG

            // Create belt
            Queue<Tray> belt = new Queue<Tray>();
            Console.WriteLine("Belt created.");     //DEBUG

            // Populate belt with empty trays
            for (int i = 0; i < 4; i++)
            {
                belt.Enqueue(new Tray().Empty());
            }
            Console.WriteLine("Belt populated.");   //DEBUG

            // Create workers
            Worker[] workers = new Worker[4];
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new Worker();
            }
            Console.WriteLine("Workers created.");  //DEBUG

            // Loop until a Tray with Items gets through all workers
            int itemsToRemove;
            int skipCounter1 = 0, skipCounter2 = 0, skipCounter3 = 0;
            bool initialLoad = true;
            do
            {
                if (!initialLoad) { belt.Dequeue(); }
                for (int i = workers.Length - 1; i >= 0; i--)
                {
                    if (i == workers.Length -1)
                    {
                        Console.WriteLine("Belt: 1(" + belt.ElementAt(0).Items + "), 2(" + belt.ElementAt(1).Items + "), 3(" + belt.ElementAt(2).Items
                            + "), 4(" + belt.ElementAt(3).Items + ")");
                    }

                    itemsToRemove = GetRemovableItems(workers[i]); //indecies of the belt and array are inverses
                    Console.WriteLine("Items to remove for Worker #" + (i + 1) +" from Tray #" + (belt.Count - i) +": " + itemsToRemove); // DEBUG

                    if (belt.ElementAt(belt.Count - 1 - i).Items == 0)
                        continue;

                    // count occurences to potentially skip
                    switch (i)
                    {
                        case 1:                     // skip 1/4 half the time                                                                                         
                            skipCounter1++;
                            if (skipCounter1 == 2)
                            {
                                skipCounter1 = 0;
                                itemsToRemove /= 4;
                                Console.WriteLine("Worker #1 skipped some items.");    //DEBUG
                            }
                            break;

                        case 2:                     // skip 1/3 half the time
                            skipCounter2++;
                            if (skipCounter2 == 2)
                            {
                                skipCounter2 = 0;
                                itemsToRemove /= 3;
                                Console.WriteLine("Worker #2 skipped some items.");    //DEBIG
                            }
                            break;

                        case 3:                     // skip 1/2 half the time
                            skipCounter3++;
                            if (skipCounter3 == 2)
                            {
                                skipCounter3 = 0;
                                itemsToRemove /= 4;
                                Console.WriteLine("Worker #3 skipped some items.");    //DEBUG
                            }
                            break;
                    }
                    // remove items
                    belt.ElementAt(belt.Count - 1 - i).RemoveItems(itemsToRemove);
                    Console.WriteLine(itemsToRemove + " items removed on Tray " + (i + 1));     //DEBUG
                    
                }

                // Decrease each Worker's efficiency by 5%
                foreach (Worker worker in workers)
                {
                    worker.DecreaseEfficiency(5);
                }
                Console.WriteLine("Workers efficiencies decreased.");       //DEBUG

                belt.Enqueue(new Tray());
                initialLoad = false;
            } while (belt.Peek().Items == 0);

            Console.WriteLine("Simulation ended.");     //DEBUG
        }

        /// <summary>
        /// Find how many items a worker can remove.
        /// </summary>
        /// <param name="w">the worker.</param>
        /// <returns>how many items the worker w can remove.</returns>
        static int GetRemovableItems(Worker w)
        {
            int eff = w.Efficiency;
            // 2 to 6 items are removable, total of 5 partitions
            if (eff > 80) { return 6; } // 100-81%  (1)
            if (eff > 60) { return 5; } // 80-61%   (2)
            if (eff > 40) { return 4; } // 60-41%   (3)
            if (eff > 20) { return 3; } // 40-21%   (4)
            return 2;                   // 0-20%    (5)
        }
    }
}