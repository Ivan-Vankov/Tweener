using System.Threading;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace Vaflov {
    public class Tween<T> : TweenBuilder<T, Tween<T>> {

        public EaseCurve easeCurve = EaseCurve.GetEaseCurve(EaseType.LINEAR);
        public T from;
        public T to;
        public Action<T> setter;
        public bool resetOnComplete = true;

        public Tween(T from, T to, Action<T> setter) {
            this.from = from;
            this.to = to;
            this.setter = setter;
        }

        public static object Lerp(object from, object to, float t) {
#if UNITY_EDITOR
            Debug.Assert(from.GetType() == to.GetType(), "<from> and <to> should be the same type");
#endif
            switch (from) {
                case float fromV: return Mathf.LerpUnclamped(fromV, (float)to, t);
                case int fromV: return (int)Mathf.LerpUnclamped(fromV, (int)to, t);
                case Vector2 fromV: return Vector2.LerpUnclamped(fromV, (Vector2)to, t);
                case Vector3 fromV: return Vector3.LerpUnclamped(fromV, (Vector3)to, t);
                case Vector4 fromV: return Vector4.LerpUnclamped(fromV, (Vector4)to, t);
                case Color fromV: return Color.LerpUnclamped(fromV, (Color)to, t);
                case Color32 fromV: return Color32.LerpUnclamped(fromV, (Color32)to, t);
                case Quaternion fromV: return Quaternion.LerpUnclamped(fromV, (Quaternion)to, t);
                default: Debug.LogError("Using unsupported tween type."); return default(T);
            }
        }

        public Tween<T> ResetOnComplete(bool resetOnComplete = true) {
            this.resetOnComplete = resetOnComplete;
            return this;
        }

        public Tween<T> Ease(AnimationCurve easeCurve) {
            this.easeCurve = new EaseCurve(easeCurve);
            return this;
        }

        public Tween<T> Ease(EaseType easeType) {
            this.easeCurve = EaseCurve.GetEaseCurve(easeType);
            return this;
        }

        public Tween<T> EaseLinear() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.LINEAR);
            return this;
        }

        public Tween<T> EaseInSlow() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_SLOW);
            return this;
        }

        public Tween<T> EaseInMedium() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_MEDIUM);
            return this;
        }

        public Tween<T> EaseInFast() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_FAST);
            return this;
        }

        public Tween<T> EaseInFastest() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_FASTEST);
            return this;
        }

        public Tween<T> EaseInOvershoot() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_OVERSHOOT);
            return this;
        }

        public Tween<T> EaseInElastic() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_ELASTIC);
            return this;
        }

        public Tween<T> EaseInBounce() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_BOUNCE);
            return this;
        }

        public Tween<T> EaseOutSlow() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.OUT_SLOW);
            return this;
        }

        public Tween<T> EaseOutMedium() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.OUT_MEDIUM);
            return this;
        }

        public Tween<T> EaseOutFast() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.OUT_FAST);
            return this;
        }

        public Tween<T> EaseOutFastest() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.OUT_FASTEST);
            return this;
        }

        public Tween<T> EaseOutOvershoot() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.OUT_OVERSHOOT);
            return this;
        }

        public Tween<T> EaseOutElastic() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.OUT_ELASTIC);
            return this;
        }

        public Tween<T> EaseOutBounce() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.OUT_BOUNCE);
            return this;
        }

        public Tween<T> EaseInOutSlow() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_OUT_SLOW);
            return this;
        }

        public Tween<T> EaseInOutMedium() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_OUT_MEDIUM);
            return this;
        }

        public Tween<T> EaseInOutFast() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_OUT_FAST);
            return this;
        }

        public Tween<T> EaseInOutFastest() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_OUT_FASTEST);
            return this;
        }

        public Tween<T> EaseInOutOvershoot() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_OUT_OVERSHOOT);
            return this;
        }

        public Tween<T> EaseInOutElastic() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_OUT_ELASTIC);
            return this;
        }

        public Tween<T> EaseInOutBounce() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.IN_OUT_BOUNCE);
            return this;
        }

        public Tween<T> EaseBellSlow() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.BELL_SLOW);
            return this;
        }

        public Tween<T> EaseBellMedium() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.BELL_MEDIUM);
            return this;
        }

        public Tween<T> EaseBellFast() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.BELL_FAST);
            return this;
        }

        public Tween<T> EaseBellFastest() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.BELL_FASTEST);
            return this;
        }

        public Tween<T> EaseBellOvershoot() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.BELL_OVERSHOOT);
            return this;
        }

        public Tween<T> EaseBellElastic() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.BELL_ELASTIC);
            return this;
        }

        public Tween<T> EaseBellBounce() {
            this.easeCurve = EaseCurve.GetEaseCurve(EaseType.BELL_BOUNCE);
            return this;
        }

        public async override UniTask TweenTaskInner(CancellationToken cancellationToken) {
            float start = Time.time;
            float end = start + duration;
            float sampleTime = 0;
            int iteration = 1;
            T currentValue = from;
            if (onValueChangedOnStart) {
                OnValueChangedCallback?.Invoke(currentValue);
            }

            while (Time.time < end && !cancellationToken.IsCancellationRequested) {
                if (iteration == 1 || loopMode == TweenLoopMode.LOOP) {
                    sampleTime = easeCurve.durationMultiplier * (Time.time - start) / duration;
                } else if (iteration > 1) {
                    if (loopMode == TweenLoopMode.CLAMP) {
                        sampleTime = easeCurve.durationMultiplier;
                    } else if (loopMode == TweenLoopMode.PING_PONG) {
                        if (iteration % 2 == 1) {
                            sampleTime = easeCurve.durationMultiplier * (Time.time - start) / duration;
                        } else {
                            sampleTime = easeCurve.durationMultiplier * (duration - Time.time + start) / duration;
                        }
                    }
                }
                T newValue = (T)Lerp(from, to, easeCurve.Evaluate(sampleTime));
                setter(newValue);
                if (!currentValue.Equals(newValue)) {
                    OnValueChangedCallback?.Invoke(newValue);
                }
                currentValue = newValue;
                await UniTask.NextFrame(cancellationToken).SuppressCancellationThrow();
                if (IsContinuous() && Time.time >= end) {
                    start = Time.time;
                    end = start + duration;
                    ++iteration;
                }
            }
            if (!isForceStopped && resetOnComplete && animatedComponent != null) {
                // TODO: Debug loop modes and implement them for SpriteSheetAnimation
                if (loopMode == TweenLoopMode.CLAMP || loopMode == TweenLoopMode.PING_PONG) {
                    setter((T)Lerp(from, to, easeCurve.Evaluate(easeCurve.durationMultiplier)));
                } else {
                    setter((T)Lerp(from, to, easeCurve.Evaluate(0)));
                }
            }
        }
    }
}