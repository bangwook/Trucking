namespace MetaSheets {
    ///		Meta Sheets Pro
    ///		Copyright © 2017 renderhjs
    ///		Version 2.00.0
    ///		Website www.metasheets.com
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class PluginLong : Plugin {
        public PluginLong() : base(eType._long, "long") {
        }
        override public bool IsValid(string cell) {
            long valInt;
            if (long.TryParse(cell, out valInt))
                return true;
            return false;
        }
        override public string GetValue(string cell, string className, string sheetName, string columnName) {
            long parseTry;
            if(cell.Trim().Length == 0) {
                return "0";
            }else if (long.TryParse(cell, out parseTry)) {
                return parseTry.ToString();
            } else {
                return cell;
            }
        }
        public override bool IsValidType(Type type) {
            return type == typeof(long);
        }
        override public string FormatReload(int index, string cellVar, string className, string sheetName, string columnName) {
            string attribute    = MetaSheets.GetName_Variable(columnName);
            string template = @"
				{0} {1}; _rows[i].{2} = {0}.TryParse( {3}, out {1}) ? {1} : {4};";
            return string.Format(template, "long", "tmp" + index, attribute, cellVar, "0");
        }
    }
}

