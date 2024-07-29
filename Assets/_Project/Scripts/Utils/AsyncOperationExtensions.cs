
using System.Threading.Tasks;
using UnityEngine;

namespace Utils
{
    public static class AsyncOperationExtensions 
    {
        public static Task DoAsTask(this AsyncOperation ao){
            TaskCompletionSource<bool> tcs =    new TaskCompletionSource<bool>();
            ao.completed += _ => tcs.SetResult(true);
            return tcs.Task;
        }
    }
}
