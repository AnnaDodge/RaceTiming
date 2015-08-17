﻿
using System;
using System.Collections.Generic;
using System.IO;
using RedRat.RaceTiming.Data;
using RedRat.RaceTiming.Data.Model;

namespace RedRat.RaceTiming.Core
{
    /// <summary>
    /// This is the main application controller. The UI is a thin wrapper round this.
    /// </summary>
    public class AppController
    {
        private DbService db;
        private readonly ClockTime clockTime = new ClockTime();
        private readonly ResultsQueue resultQueue;

		public AppController(DbService db)
        {
            this.db = db;
            resultQueue = new ResultsQueue( this );
        }

        public bool IsDbOpen
        {
            get { return db.IsDbOpen; }
        }

        public ClockTime ClockTime
        {
            get { return clockTime; }
        }

        public void CreateNewRace( Race race, string dbFilename )
        {
            if ( db.IsDbOpen )
            {
                db.Close();
            }
            // If we are overwriting, then delete the current file.
            if ( File.Exists( dbFilename ) )
            {
                File.Delete( dbFilename );
            }
            db.Open( dbFilename );
            db.AddRace( race );
        }

        public void OpenRace( string dbFilename )
        {
            if ( db.IsDbOpen )
            {
                db.Close();
            }
            db.Open( dbFilename );
        }

        /// <summary>
        /// Gets the current race.
        /// </summary>
        public Race CurrentRace
        {
            get
            {
                var races = db.GetRaces();
				// If we don't yet have a race, create a new one.
				if ( races.Count == 0 )
				{
					var race = new Race { Name = "-- New Race --" };
					db.AddRace(race);
                    return race;
				}

                // We should only have one race - current logic imposes this.
                // DB should in principle support having multiple races.
                if ( races.Count > 1 )
                {
                    throw new Exception( string.Format( "Should have only one race object. Currently have {0}.", races.Count ) );
                }
                return races[0];
            }
        }

        public void UpdateCurrentRace( Race newRaceDetails )
        {
			db.UpdateRace(CurrentRace.Oid, newRaceDetails);
        }

        public IList<Runner> GetRunners()
        {
            return db.GetRunners();
        }

        /// <summary>
        /// Loads a CSV file containing race entrant information.
        /// </summary>
        public void LoadCsvFile( string filename )
        {
            // ToDo: Long term, make this more configurable. At the moment we're only interested
            //       in Runner's World files.

            // ToDo - AD:
            // 1. Open and read the file.
            // 2. Create runner objects
            // 3. Check that they don't already exist in the DB (use firstname, lastname and DoB)
            // 4. If they don't exist, then add them.
            var runner = new Runner
            {
                FirstName = "Chris", 
                LastName = "Dodge",
                Gender = Runner.GenderEnum.Male,
                DateOfBirth = DateTime.Parse( "25/01/1965" ),
                Club = "Saffron Striders",
                Address = "Somewhere",
            };

            db.AddRunner( runner );
        }

        /// <summary>
        /// Adds a new race result time.
        /// </summary>
        public void AddTime(bool female)
        {
            resultQueue.Enqueue( new ResultsQueue.ResultSlot {datetime = DateTime.Now, female = female} );
        }
    }
}
