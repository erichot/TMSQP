using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSQPBusiness.Models
{
    public class BusinessProcessModel
    {
    }





    public class BusinessProcessResult
    {

        public BusinessProcessResult() { }

        public BusinessProcessResult(DataProcessResult _source)
        {
            this.HasError = _source.HasError;
            this.ResultId = _source.ResultId;
            this.ResultNo = _source.ResultNo;
            this.Message = _source.Message;
        }

        public bool HasError { get; set; }

        public string ResultId { get; set; }
        public int ResultNo { get; set; }

        public int ResultValue { get; set; }
        public int ResultValue2 { get; set; }

        public string Message { get; set; }


        private Exception? m_ExceptionInfo;
        public Exception? ExceptionInfo
        {
            get
            {
                return m_ExceptionInfo;
            }
            set
            {
                m_ExceptionInfo = value;
                if (value != null)
                {
                    this.HasError = true;
                    this.Message = value.ComposeExceptionMessage();
                }

            }
        }




        public void SetErrorMessage(string _message)
        {
            this.HasError = true;
            this.Message = _message;
        }

    }





    public class BusinessProcessResult<T> : BusinessProcessResult
    {
        public BusinessProcessResult() { }

        public BusinessProcessResult(DataProcessResult<T> _source)
        {
            this.HasError = _source.HasError;
            this.ResultId = _source.ResultId;
            this.ResultNo = _source.ResultNo;
            this.Message = _source.Message;

            this.ResultInfo = _source.ResultInfo;
        }

        public BusinessProcessResult(BusinessProcessResult _source)
        {
            this.HasError = _source.HasError;
            this.ResultId = _source.ResultId;
            this.ResultNo = _source.ResultNo;
            this.Message = _source.Message;
        }




        public BusinessProcessResult(T _entity)
        {
            this.ResultInfo = _entity;
        }


        public T ResultInfo { get; set; }
    }



}
