using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public class ExceptionInfoControll
    {
        private List<ExceptionInfo> _exceptionInfoList;

        public List<ExceptionInfo> ExceptionInfoList => _exceptionInfoList;

        public ExceptionInfoControll(List<ExceptionInfo> exceptionInfoList)
        {
            if (exceptionInfoList == null)
                throw new ArgumentNullException(nameof(exceptionInfoList));

            _exceptionInfoList = exceptionInfoList;
        }

        public void AddExceptionInfo(ExceptionInfo exceptionInfo)
        {
            if (exceptionInfo == null)
                throw new ArgumentNullException(nameof(exceptionInfo));

            _exceptionInfoList.Add(exceptionInfo);
        }

        #region  не должно быть здесь (наверное) (+ не актуально после добавление шаблонов)
        public void SimplyPrintExceptionInfoList()
        {
            foreach (var ex in _exceptionInfoList)
            {
                ExceptionInfoPrint(ex);
            }
        }

        private void ExceptionInfoPrint(ExceptionInfo exceptionInfo)
        {
            Console.WriteLine();
            Console.WriteLine("<<<<<<" + exceptionInfo.exType + ">>>>>>");
            Console.WriteLine();
            foreach (var bodyString in exceptionInfo.exBody)
            {
                Console.WriteLine(bodyString);
            }
        } 
        #endregion
    }
}
