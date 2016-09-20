using EventAnalyzer.Domain;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace EventAnalyzer.Tests
{
    public class when_analyzing_a_list_of_switch_events
    {
        private Analysis _target;

        public when_analyzing_a_list_of_switch_events()
        {
            var events = new List<SwitchEvent>
            {
                new SwitchEvent { EventSeconds = 25, EventType = SwitchEventType.Off},
                new SwitchEvent { EventSeconds = 40, EventType = SwitchEventType.On },
                new SwitchEvent { EventSeconds = 3, EventType = SwitchEventType.Off }
            };

            _target = new Analysis(events);
        }

        [Fact]
        public void calculates_total_seconds()
        {
            _target.TotalSeconds.Should().Be(68);
        }

        [Fact]
        public void calculates_event_count()
        {
            _target.EventCount.Should().Be(3);
        }

        [Fact]
        public void calculates_average_seconds()
        {
            _target.AverageSeconds.Should().Be(23);
        }

        [Fact]
        public void calculates_sorted_seconds()
        {
            _target.SortedSeconds.Should().BeInAscendingOrder();
        }

        [Fact]
        public void calculates_most_common_event_type()
        {
            _target.MostCommonEventType.Should().Be(SwitchEventType.Off);
        }
    }
}
