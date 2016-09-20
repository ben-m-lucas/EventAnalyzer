using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAnalyzer.Domain
{
    /// <summary>
    /// An analysis of the provided events
    /// </summary>
    public class Analysis
    {
        public Analysis(IEnumerable<SwitchEvent> events)
        {
        }

        /// <summary>
        /// Total seconds from all of the events
        /// </summary>
        public int TotalSeconds
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The number of events
        /// </summary>
        public int EventCount
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Average of all event seconds rounded to the nearest integer
        /// </summary>
        public int AverageSeconds
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// An array of all event seconds sorted from smallest to largest
        /// </summary>
        public int[] SortedSeconds
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// The Event Type that occurred most often
        /// </summary>
        public SwitchEventType MostCommonEventType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
