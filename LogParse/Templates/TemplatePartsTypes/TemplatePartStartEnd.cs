using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse.Templates
{
    class TemplatePartStartEnd : TemplatePart
    {
        public string StartString { get; set; }

        public string EndString { get; set; }

        private bool _reading;


        public TemplatePartStartEnd(string partName,Action endOfTemplate, string startString, string endString):
            base(partName, endOfTemplate)
        {
            if (startString == null)
                throw new ArgumentNullException("Один из параметров поиска не задан");

            _readingTemplate = ReadingTemplateEnum.StartEnd;
            
            StartString = startString;
            EndString = string.IsNullOrWhiteSpace(endString)?"": endString;
            _reading = false;
        }

        //кажись верно
        public override void LineProcessing(string fileRow)
        {
            if (_reading)
            {
                if (!string.IsNullOrWhiteSpace(EndString))
                {
                    TemplateBody.AddLast(fileRow);

                    if (fileRow.Contains(EndString))
                    {
                        InvokeEndOfTemplate();
                        _reading = false;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(fileRow))
                    {
                        TemplateBody.AddLast(fileRow);
                    }
                    else
                    {
                        InvokeEndOfTemplate();
                        _reading = false;
                    }
                }
            }
            else
            {
                if (fileRow.Contains(StartString))
                {
                    TemplateBody.AddLast(fileRow);
                    _reading = true;
                }
            }
            
        }
    }
}
