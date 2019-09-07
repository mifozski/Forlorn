using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Forlorn.ConditionSystem
{
	public struct TriggerData
	{
		public string id;
		public string text;
		public string condition;
		public string reaction;
		public string cues;
	}

	public static class TriggerDataReader
	{
		public static void Read(out List<TriggerData> data)
		{
			string pattern = ";(?=(([^\"]*\"){2})*[^\"]*$)";

			data = new List<TriggerData>();

			var fileData = System.IO.File.ReadAllText("Assets/Resources/TriggerData.csv");

			var lines = fileData.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
			bool headerSkipped = false;
			foreach (string line in lines)
			{
				if (!headerSkipped)
				{
					headerSkipped = true;
					continue;
				}
				var trimmedLine = line.Trim();
				string[] tokens = System.Text.RegularExpressions.Regex.Split(
					trimmedLine, pattern, RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture
				);

				var triggerData = new TriggerData();

				triggerData.id = tokens[0];
				triggerData.text = tokens[1].Trim('"');
				triggerData.condition = tokens[2];
				triggerData.reaction = tokens[3];
				triggerData.cues = tokens[4];

				data.Add(triggerData);
			}
		}
	}
}