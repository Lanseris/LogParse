using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public class ExceptionInfo
    {
        public ExceptionBodyTemplate ExcceptionBodyTemplate { get; set; }

        //название части шаблона и её тело
        public Dictionary<string, List<string>> ExceptionPartsReultsDic { get; set; }

        public ExceptionInfo()
        {
            ExceptionPartsReultsDic = new Dictionary<string, List<string>>();
        }

        public void AddRowToTemplatePartBody(string partName, string row)
        {
            if (!ExceptionPartsReultsDic.ContainsKey(partName))
            {
                ExceptionPartsReultsDic.Add(partName, new List<string>());
            }

            ExceptionPartsReultsDic[partName].Add(row);
        }
    }
}
