namespace MetaSheets {
	///		Meta Sheets Pro
	///		Copyright © 2017 renderhjs
	///		Version 2.00.0
	///		Website www.metasheets.com
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	public class PluginLongArray : Plugin {
		public PluginLongArray() : base(eType._longArray, "long[]", true) {
		}
		public override bool IsValid(string cell) {
			//IS INT ARRAY
			if (cell.Contains(",")) {
				string[] array = cell.Split(',');
				int countInteger = 0;
				long result;
				foreach (string s in array) {
					if (s.Contains(".") == false && long.TryParse(s, out result)) {
						countInteger++;
					}
				}
				if (countInteger == array.Length) {
					return true;
				}
			}
			return false;
		}
		public override string GetValue(string cell, string className, string sheetName, string columnName) {
			cell = Trim(cell);
			if (cell == "") {
				return "new long[]{}";
			} else {
				List<string> values = new List<string>();
				foreach (string s in cell.Split(',')) {
					long parsed;
					if (s.Trim().Length == 0) {
						values.Add("0");
					} else if (long.TryParse(s, out parsed)) {
						values.Add(parsed.ToString());
					} else {
						values.Add(s.Trim());
					}
				}
				return "new long[]{" + string.Join(",", values.ToArray()) + "}";
			}
		}
		public override bool IsValidType(Type type) {
			return type == typeof(long[]);
		}
		public override string FormatReload(int index, string cellVar, string className, string sheetName, string columnName) {
			string attribute    = MetaSheets.GetName_Variable(columnName);
			string tempVar = "tmp" + index;
			return StringUtilities.Format(@"
				{0} = CleanString({0});	
				string[] {1} = {0}.Split(',');
				_rows[i].{2} =  ({0} == """") ? new long[]{} : Array.ConvertAll<string, long>({1}, ParseLong);",
				cellVar,
				tempVar,
				attribute
			);
		}
	}
}

