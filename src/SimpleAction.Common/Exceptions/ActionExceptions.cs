using System;

namespace SimpleAction.Common.Exceptions {
    public class ActionException : Exception {
        public ActionException () { }

        public ActionException (string code) {
            this.Code = code;

        }
        public string Code { get; }

         public ActionException (string message, params object[] args): this(string.Empty, message, args){ } 
         public ActionException (string code,string message, params object[] args): this (null, code, message, args){ } 

         public ActionException (Exception innerException,string message, params object[] args): this (innerException, string.Empty, message, args){ } 

         public ActionException (Exception innerException, string code,string message, params object[] args): base (string.Format(message, args), innerException){ 
             Code = code;
         } 

    }
}