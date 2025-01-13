using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Unity1week202412
{
    public static class UserInput
    {
        public static async UniTask WaitAnyInput(CancellationToken cancellation)
        {
            await UniTask.WaitUntil(() => Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2),
                cancellationToken: cancellation);
        }
        
        public static Observable<Unit> GetKeyDownAsObservable(KeyCode keyCode)
        {
            return Observable.EveryUpdate().Where(_ => Input.GetKeyDown(keyCode));
        }
    }
}