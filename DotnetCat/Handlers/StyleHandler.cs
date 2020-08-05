﻿using DotnetCat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetCat.Handlers
{
    /// <summary>
    /// Application output style handler
    /// </summary>
    class StyleHandler
    {
        /// Initialize new StyleHandler
        public StyleHandler()
        {
            this.Statuses = new List<Status>
            {
                new Status("info", "[*]", ConsoleColor.Cyan),
                new Status("out", "[+]", ConsoleColor.Green),
                new Status("warn", "[!]", ConsoleColor.Yellow)
            };
        }

        public List<Status> Statuses { get; set; }

        /// Print a custom status to standard output
        public void Status(string msg, string level = "info")
        {
            if (!this.IsValidLevel(level))
            {
                string errMsg = "Level must be info, out, or warning";
                throw new ArgumentException(errMsg, paramName: "errMsg");
            }

            int index = IndexOfStatus(level);

            Console.ForegroundColor = Statuses[index].Color;
            Console.Write($"{Statuses[index].Symbol} ");
            Console.ResetColor();
            Console.WriteLine(msg);
        }

        /// Determine if specified status level is valid
        private bool IsValidLevel(string level)
        {
            return IndexOfStatus(level) > -1;
        }

        /// Get the index of a status in this.Statuses
        private int IndexOfStatus(string level)
        {
            int index = -1;

            List<int> query = (from stat in Statuses
                               where stat.Level == level.ToLower()
                               select Statuses.IndexOf(stat)).ToList();

            query.ForEach(x => index = x);
            return index;
        }
    }
}