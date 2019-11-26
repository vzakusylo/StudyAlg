using System;

namespace Usavc.Microservices.Common.Types
{
    public class UsavcException : Exception
    {
        public string Code { get; }

        public UsavcException()
        {
        }

        public UsavcException(string code)
        {
            Code = code;
        }

        public UsavcException(string message, params object[] args) 
            : this(string.Empty, message, args)
        {
        }

        public UsavcException(string code, string message, params object[] args) 
            : this(null, code, message, args)
        {
        }

        public UsavcException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public UsavcException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        
    }
}