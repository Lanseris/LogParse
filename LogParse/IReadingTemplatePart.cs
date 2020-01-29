using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public interface IReadingTemplatePart
    {
        public ReadingTemplateEnum ReadingTemplateType { get; }

        public void LineProcessing(string fileRow, ref ExceptionInfo exceptionInfo );

        public bool CheckConditionMatch(string stringForCheck);

        public bool IsReading { get; }

        public event Action EndOfTemplate;

        public string PartName { get; set; }

    }
}
