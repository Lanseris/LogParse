using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogParse
{
    class ExcceptionBodyTemplate
    {
        private string _templateName;

        public string TemplateName
        {
            get
            {
                return _templateName;
            }
            set
            {
                _templateName = value;
            }
        }

        private LinkedList<TemplatePart> _templatePartsLinkedList;


        public ExcceptionBodyTemplate(string templateName, ReadingTemplateEnum readingSignatureEnum)
        {
            if (string.IsNullOrEmpty(templateName))
                throw new ArgumentNullException(nameof(templateName));

            _templateName = templateName;

            _readingTemplate = readingSignatureEnum;

            _templatePartsLinkedList = new LinkedList<TemplatePart>();
        }

        public  
    }
}
