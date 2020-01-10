using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public enum ReadingTemplateEnum
    {
        StartEnd = 0,      //Обозначение границ считывания двумя словами
        StartAndNum = 1, //Начальное слово, количество строк после него (НЕ включая строку слова)
        Auto = 2        //Как в xml <...></...> (прикрутить стандартный xml парсер)
    }
}
