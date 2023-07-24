using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Vaflov {
    public enum TweenLoopMode {
        CLAMP,
        LOOP,
        PING_PONG,
    }

    public interface ITween {
        public void Stop();
        public void ForceStop();
    }

    public abstract class TweenBuilder<T, TDerived> : CustomYieldInstruction, ITween where TDerived : TweenBuilder<T, TDerived> {
        public float delay = 0;
        public float duration = 1;
        public int repeatCount = 1;
        public TweenLoopMode loopMode = TweenLoopMode.CLAMP;
        public bool onValueChangedOnStart = false;
        public Action<T> OnValueChangedCallback;
        public Component animatedComponent;
        public UniTask task;
        public CancellationTokenSource cts;
        public CancellationToken objLifetimeCancellationToken = CancellationToken.None;
        public CancellationToken externalCancellationToken = CancellationToken.None;
        public Action OnCompleteCallback;
        public delegate void OnCompleteAction(bool isStopped);
        public OnCompleteAction OnCompleteAndIsStoppedCallback;
        public Action OnStoppedCallback;
        public bool isStopped = false;
        public bool isForceStopped = false;
        public bool syncWithObject = true;
        public bool IsRunning { get; protected set; } = true;

        public override bool keepWaiting => IsRunning;

        public TDerived Delay(float delay) {
            this.delay = delay;
            return (TDerived)this;
        }

        public TDerived Duration(float duration) {
#if UNITY_EDITOR
            Debug.Assert(duration >= 0, "Can't have a Tween with a <0 duration.");
#endif
            this.duration = duration;
            return (TDerived)this;
        }

        public TDerived Repeat(int repeatCount) {
            this.repeatCount = repeatCount;
            return (TDerived)this;
        }

        public TDerived Continuous(bool continuous = true) {
            if (continuous) {
                this.repeatCount = -1;
            }
            return (TDerived)this;
        }

        public TDerived LoopMode(TweenLoopMode loopMode) {
            this.loopMode = loopMode;
            return (TDerived)this;
        }

        public TDerived OnValueChangedOnStart() {
            this.onValueChangedOnStart = true;
            return (TDerived)this;
        }

        public TDerived OnValueChanged(Action<T> callback) {
            this.OnValueChangedCallback += callback;
            return (TDerived)this;
        }

        public TDerived OnComplete(Action callback) {
            this.OnCompleteCallback += callback;
            return (TDerived)this;
        }

        public TDerived OnComplete(OnCompleteAction callback) {
            this.OnCompleteAndIsStoppedCallback += callback;
            return (TDerived)this;
        }

        public TDerived OnStop(Action callback) {
            this.OnStoppedCallback += callback;
            return (TDerived)this;
        }

        public TDerived SyncWithObject(bool syncWithObject = true) {
            this.syncWithObject = syncWithObject;
            return (TDerived)this;
        }

        public TDerived AnimatedComponent(Component animatedComponent) {
            this.animatedComponent = animatedComponent;
            return (TDerived)this;
        }

        public void Stop() {
            isStopped = true;
            if (cts != null) {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
            if (!IsRunning) {
                OnCompleteCallback?.Invoke();
                OnCompleteAndIsStoppedCallback?.Invoke(isStopped: true);
                OnStoppedCallback?.Invoke();
            }
        }

        public void ForceStop() {
            isForceStopped = true;
            if (cts != null) {
                cts.Cancel();
                cts.Dispose();
                cts = null;
            }
        }

        public TDerived WithCancellation(CancellationToken cancellationToken) {
            this.externalCancellationToken = cancellationToken;
            return (TDerived)this;
        }

        public bool IsContinuous() => repeatCount == -1;

        public TDerived Start() {
            task = TweenTask();
            return (TDerived)this;
        }

        public async UniTask TweenTask() {
            TweenTracker.AddTween(this);
            cts = new CancellationTokenSource();
            isStopped = false;
            isForceStopped = false;
            IsRunning = true;
            await UniTask.NextFrame(cts.Token).SuppressCancellationThrow();
            if (isForceStopped)
                return;

            var extraTokens = externalCancellationToken != CancellationToken.None;
            if (syncWithObject && animatedComponent) {
                objLifetimeCancellationToken = animatedComponent.GetCancellationTokenOnDestroy();
                extraTokens = true;
            }
            if (extraTokens) {
                cts?.Dispose();
                cts = CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken, objLifetimeCancellationToken);
            }

            if (!cts.IsCancellationRequested) {
                if (delay > 0) {
                    await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cts.Token)
                                 .SuppressCancellationThrow();
                }
                if (!isForceStopped && !cts.IsCancellationRequested) {
                    await DoTweenTask(cts.Token);
                }
            }
            if (!isForceStopped) {
                OnCompleteCallback?.Invoke();
                OnCompleteAndIsStoppedCallback?.Invoke(isStopped);
                if (isStopped) {
                    OnStoppedCallback?.Invoke();
                }
            }
            IsRunning = false;
            TweenTracker.RemoveTweenCTS(this);
        }

        public abstract UniTask DoTweenTask(CancellationToken cancellationToken);

        public static implicit operator UniTask(TweenBuilder<T, TDerived> tween) => tween.task;
    }
}