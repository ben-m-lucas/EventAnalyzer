using EventAnalyzer.Domain;
using NDesk.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EventAnalyzer.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            bool runTest = false;
            bool showHelp = false;
            string inputFile = null;
            var optionSet = new OptionSet
            {
                {"i|input=", "the {FILE} in JSON format to use for input.", v => inputFile = v },
                {"t|test", "test switch event analysis against test data set", v => runTest = true },
                {"h|help", "show this message and exit", v => showHelp = v != null  }
            };

            if (args.Length == 0)
            {
                ShowErrorMessage();
                return;
            }

            try
            {
                List<string> extra = optionSet.Parse(args);
                if (showHelp)
                {
                    ShowHelp(optionSet);
                }
                else if (runTest)
                {
                    TestAnalysis();
                }
                else if (!string.IsNullOrEmpty(inputFile))
                {
                    AnalyzeEvents(inputFile);
                    return;
                }
            }
            catch (OptionException e)
            {
                ShowErrorMessage(e);
            }
        }

        private static void AnalyzeEvents(string inputFile)
        {
            try
            {
                using (StreamReader reader = File.OpenText(inputFile))
                {
                    string json = reader.ReadToEnd();
                    var events = JsonConvert.DeserializeObject<IEnumerable<SwitchEvent>>(json, new StringEnumConverter());

                    AnalyzeEvents(events);
                }
            }
            catch (Exception e)
            {
                ShowErrorMessage(e);
            }
        }

        private static void AnalyzeEvents(IEnumerable<SwitchEvent> events)
        {
            var analysis = new Analysis(events);
            var result = JsonConvert.SerializeObject(analysis, new StringEnumConverter());
            Console.WriteLine(result);

            Console.ReadKey();
        }

        private static void TestAnalysis()
        {
            var events = new List<SwitchEvent>
            {
                new SwitchEvent { EventSeconds = 25, EventType = SwitchEventType.Off},
                new SwitchEvent { EventSeconds = 40, EventType = SwitchEventType.On },
                new SwitchEvent { EventSeconds = 3, EventType = SwitchEventType.Off }
            };
            AnalyzeEvents(events);
        }

        private static void ShowHelp(OptionSet optionSet)
        {
            Console.WriteLine("Usage: EventAnalyzer [OPTIONS]");
            Console.WriteLine("Analyze a list of switch events.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            optionSet.WriteOptionDescriptions(Console.Out);

            Console.ReadKey();
        }

        private static void ShowErrorMessage(Exception e = null)
        {
            Console.Write("EventAnalyzer: ");
            if (e != null)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Try 'EventAnalyzer --help' for more information.");
            Console.ReadKey();
        }
    }
}
