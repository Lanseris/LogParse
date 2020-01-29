using System;
using System.Collections.Generic;
using System.Text;

namespace LogParse
{
    public class ExceptionInfoControll
    {
        private Dictionary<string, List<ExceptionInfo>> _exceptionInfoDic;

        public Dictionary<string, List<ExceptionInfo>> ExceptionInfoDic => _exceptionInfoDic;

        public ExceptionInfoControll()
        {
            _exceptionInfoDic = new Dictionary<string, List<ExceptionInfo>>();
        }

        #region пока не нужный конструктор

        /// <summary>
        ///
        /// </summary>
        /// <param name=""></param>
        public ExceptionInfoControll(Dictionary<string, List<ExceptionInfo>> exceptionInfoDic)
        {
            if (exceptionInfoDic == null)
                throw new ArgumentNullException(nameof(exceptionInfoDic));

            _exceptionInfoDic = exceptionInfoDic;
        } 
        #endregion

        public void AddExceptionInfo(ExceptionInfo exceptionInfo)
        {
            if (exceptionInfo == null)
                throw new ArgumentNullException(nameof(exceptionInfo));

            string templateName = exceptionInfo.ExcceptionBodyTemplate.TemplateName;

            if (!ExceptionInfoDic.ContainsKey(templateName))
            {
                ExceptionInfoDic.Add(templateName, new List<ExceptionInfo>());
            }
            ExceptionInfoDic[templateName].Add(exceptionInfo);
        }


        ///починить TODO
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
