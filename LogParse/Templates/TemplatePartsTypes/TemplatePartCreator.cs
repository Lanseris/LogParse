using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse.Templates.TemplatePartsTypes
{
    public static class TemplatePartCreator
    {
        public static IReadingTemplatePart CreateTemplatePart(string templateName, string startString = null, string endString = null, int numOfStrings = 0)
        {
            if (string.IsNullOrEmpty(templateName))
                throw new ArgumentNullException("Наименование части шаблона не может быть пустым");

            IReadingTemplatePart readingTemplatePart = null;

            ReadingTemplateEnum readingTemplate;

            readingTemplate = chooseReadingPattern(templateName, startString, endString, numOfStrings);

            switch (readingTemplate)
            {
                case ReadingTemplateEnum.StartEnd:
                    readingTemplatePart = new TemplatePartStartEnd(templateName, startString, endString);
                    break;
                case ReadingTemplateEnum.StartAndNum:
                    readingTemplatePart = new TemplatePartStartAndNum(templateName, startString, numOfStrings);
                    break;
                case ReadingTemplateEnum.Auto:
                    readingTemplatePart = new TemplatePartAuto(templateName);
                    break;
                default:
                    break;
            }

            return readingTemplatePart;
        }

        private static ReadingTemplateEnum chooseReadingPattern(string templateName, string startString = null, string endString = null, int numOfStrings = 0)
        {
            return startString != null ?
                endString != null ?
                     ReadingTemplateEnum.StartEnd
                     : ReadingTemplateEnum.StartAndNum
                : ReadingTemplateEnum.Auto;
        }

    }
}
