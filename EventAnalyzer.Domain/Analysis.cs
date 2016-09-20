using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EventAnalyzer.Domain
{
    /// <summary>
    /// An analysis of the provided events
    /// </summary>
    public class Analysis
    {
        private IEnumerable<SwitchEvent> _events;

        public Analysis(IEnumerable<SwitchEvent> events)
        {
            Debug.Assert(events != null);
            Debug.Assert(events.Count() > 0);

            _events = events;
        }

        /// <summary>
        /// Total seconds from all of the events
        /// </summary>
        public int TotalSeconds
        {
            get { return _events.Sum(x => x.EventSeconds); }
        }

        /// <summary>
        /// The number of events
        /// </summary>
        public int EventCount
        {
            get { return _events.Count(); }
        }

        /// <summary>
        /// Average of all event seconds rounded to the nearest integer
        /// </summary>
        public int AverageSeconds
        {
            get
            {
                double average = _events.Average(x => x.EventSeconds);
                double roundedAverage = Math.Round(average, MidpointRounding.AwayFromZero);
                return Convert.ToInt32(roundedAverage);
            }
        }

        /// <summary>
        /// An array of all event seconds sorted from smallest to largest
        /// </summary>
        public int[] SortedSeconds
        {
            get
            {
                return _events.OrderBy(x => x.EventSeconds)
                                .Select(x => x.EventSeconds)
                                .ToArray();
            }
        }

        /// <summary>
        /// The Event Type that occurred most often
        /// </summary>
        public SwitchEventType MostCommonEventType
        {
            get
            {
                var eventGroupWithMostEvents = _events.GroupBy(x => x.EventType)
                                                        .OrderByDescending(x => x.Count())
                                                        .First();
                return eventGroupWithMostEvents.Key;
            }
        }
    }
}
