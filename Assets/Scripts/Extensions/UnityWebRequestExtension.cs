using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public static class UnityWebRequestExtension
{
    public static TaskAwaiter<UnityWebRequest.Result> GetAwaiter(this UnityWebRequestAsyncOperation requestOperation)
    {
        TaskCompletionSource<UnityWebRequest.Result> tsc = new TaskCompletionSource<UnityWebRequest.Result>();
        requestOperation.completed += asyncOperation => tsc.TrySetResult(requestOperation.webRequest.result);
        if (requestOperation.isDone) tsc.TrySetResult(requestOperation.webRequest.result);
        else
        {
            Popup.Instance.RequestLoading(requestOperation.progress);
        }
        return tsc.Task.GetAwaiter();
    }
}