using System;
using System.Collections.Generic;

namespace Bitspco.Framework.Common
{
    public class OperationResult
    {
        public bool Success { get; set; } = true;
        public int Code { get; set; }
        public string Message { get; set; }

        public OperationResult SetSuccess(bool value) { Success = value; return this; }
        public OperationResult SetCode(int value) { Code = value; return this; }
        public OperationResult SetMessage(string value) { Message = value; return this; }

        public static OperationResult Successfull()
        {
            return new OperationResult();
        }
        public static OperationResult Failure(string message = "")
        {
            return new OperationResult() { Success = false, Message = message };
        }
        public OperationResult<T> ToOperationResult<T>(T data)
        {
            return new OperationResult<T>()
            {
                Success = Success,
                Code = Code,
                Message = Message,
                Data = data
            };
        }
        public static implicit operator OperationResult(bool result)
        {
            return (result ? Successfull() : Failure(""));
        }
    }
    public class OperationResult<T>
    {
        private bool _success = true;
        public bool Success { get { return _success && Data != null; } set { _success = value; } }
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public OperationResult<T> SetSuccess(bool value) { Success = value; return this; }
        public OperationResult<T> SetCode(int value) { Code = value; return this; }
        public OperationResult<T> SetMessage(string value) { Message = value; return this; }
        public OperationResult<T> SetData(T value) { Data = value; return this; }
        public static implicit operator OperationResult<T>(T obj)
        {
            return Successfull(obj);
        }
        public static implicit operator OperationResult<T>(OperationResult obj)
        {
            return obj.ToOperationResult(default(T));
        }
        public static OperationResult<T> Successfull(T obj)
        {
            return new OperationResult<T>() { Data = obj };
        }
        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>() { Success = false, Message = message };
        }
        public OperationResult ToOperationResult()
        {
            return new OperationResult()
            {
                Success = Success,
                Code = Code,
                Message = Message
            };
        }
        public OperationResult<TG> ToOperationResult<TG>(Func<OperationResult<T>, TG> func)
        {
            return new OperationResult<TG>()
            {
                Success = Success,
                Code = Code,
                Message = Message,
                Data = func(this)
            };
        }
    }
    public class OperationResultCount : OperationResult
    {
        public int Count { get; set; }
    }
    public class OperationResultCount<T> : OperationResult<T>
    {
        public int Count { get; set; }
    }
}
