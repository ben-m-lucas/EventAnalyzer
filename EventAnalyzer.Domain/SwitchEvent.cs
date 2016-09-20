namespace EventAnalyzer.Domain
{
    public class SwitchEvent
    {
        public int EventSeconds { get; set; }
        public SwitchEventType EventType { get; set; }
    }
}
