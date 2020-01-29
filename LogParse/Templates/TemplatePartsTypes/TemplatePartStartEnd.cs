using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse.Templates
{
    class TemplatePartStartEnd : TemplatePart
    {
        public string StartString { get; set; }

        public string EndString { get; set; }


        public TemplatePartStartEnd(string partName,Action endOfTemplate, string startString, string endString):
            base(partName, endOfTemplate)
        {
            if (startString == null)
                throw new ArgumentNullException("Один из параметров поиска не задан");

            _readingTemplate = ReadingTemplateEnum.StartEnd;
            
            StartString = startString;
            EndString = string.IsNullOrWhiteSpace(endString)?"": endString;
        }

        //Метод определения начала и конца Части шаблона
        public override void LineProcessing(string fileRow, ref ExceptionInfo exceptionInfo)
        {
            if (CheckConditionMatch(fileRow))
            {
                _reading = !_reading;
                if (!string.IsNullOrWhiteSpace(fileRow))
                {
                    exceptionInfo.AddRowToTemplatePartBody(_partName,fileRow);
                }
            }
            else
            {
                if (_reading)
                {
                    exceptionInfo.AddRowToTemplatePartBody(_partName, fileRow);
                }
            }
        }

        /// <summary>
        /// Проверка совпадения читаемой строки, параметра _reading и значений из Части шаблона (start,end)
        /// </summary>
        /// <param name="stringForCheck">строка в которой ищется совпадение</param>
        /// <returns></returns>
        public bool CheckConditionMatch(string stringForCheck)
        {
            //TODO Варианты с началом на "" не допустимы, подумать над этим ещё
            if (_reading)
            {
                if (!string.IsNullOrWhiteSpace(EndString))
                {
                    if (stringForCheck.Contains(EndString))
                    {
                        return true;
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(stringForCheck))
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (stringForCheck.Contains(StartString))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
