using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogParse
{
    public class ExceptionBodyTemplate
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

        private LinkedList<IReadingTemplatePart> _templatePartsLinkedList;

        public LinkedList<IReadingTemplatePart> TemplatePartsLinkedList => _templatePartsLinkedList;

        public ExceptionBodyTemplate(string templateName)
        {
            if (string.IsNullOrEmpty(templateName))
                throw new ArgumentNullException(nameof(templateName));

            _templateName = templateName;

            _templatePartsLinkedList = new LinkedList<IReadingTemplatePart>();
        }

        public void AddTemplatePart(IReadingTemplatePart templatePart)
        {
            if (templatePart == null)
                throw new ArgumentNullException(nameof(templatePart));

            _templatePartsLinkedList.AddLast(templatePart);
        }

    }
}
