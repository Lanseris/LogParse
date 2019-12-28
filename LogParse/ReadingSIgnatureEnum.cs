using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    enum ReadingSIgnatureEnum
    {
        StartEnd = 0,      //Начало и конец - одно слово
        StartEndDiff = 1, //Начало -  одно слово, конец - другое
        StartAndNum = 2, //Начальное слово, количество строк после него (НЕ включая строку слова)
        Auto = 3        //Как в xml <...></...>
    }
}
