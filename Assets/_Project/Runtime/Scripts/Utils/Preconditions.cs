using System;

namespace Utils{
    public class Preconditions{
        public static T OrNull<T>( T obj) where T :  UnityEngine.Object => obj ? obj : null;
        public static T CheckNotNull<T>(T reference){
            return CheckNotNull(reference, null);
        }
        public static T CheckNotNull<T>(T reference , string message){
            if(reference is UnityEngine.Object obj && OrNull(obj) == null){
                throw new ArgumentNullException(message);
            }
            if(reference is null){
                throw new ArgumentNullException(message);
            }
            return reference;
        }
        public static void CheckState(bool expression){
            CheckState(expression , null);
        }
        public static void CheckState(bool expression, string message, params object[] messageArgs){
            CheckState(expression, string.Format(message, messageArgs));
        }
        public static void CheckState(bool expression, string message){
            if(expression){
                return;
            }
            throw message == null ? new InvalidOperationException() : new InvalidOperationException(message);
        }
    }
}