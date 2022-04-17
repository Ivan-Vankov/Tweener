using UnityEngine;
using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Vaflov {
    public class SpriteSheetAnimation : TweenBuilder<Sprite, SpriteSheetAnimation> {

        public Sprite[] frames;
        public Action<Sprite> frameSetter;
        public float lastFrameMultiplier = 1;
        public bool stayOnLastFrame = false;

        public SpriteSheetAnimation(Sprite[] frames,
                                    Action<Sprite> frameSetter) {
            this.frames = frames;
            this.frameSetter = frameSetter;
        }

        public SpriteSheetAnimation Reverse() {
            frames = frames.Reverse().ToArray();
            return this;
        }

        public SpriteSheetAnimation LastFrameMultiplier(float lastFrameMultiplier) {
            this.lastFrameMultiplier = lastFrameMultiplier;
            return this;
        }

        public SpriteSheetAnimation StayOnLastFrame(bool stay = true) {
            this.stayOnLastFrame = stay;
            return this;
        }

        public async override UniTask TweenTaskInner(CancellationToken cancellationToken) {
            int frameIndex = 0;
            float frameDuration = duration / (frames.Length - 1 + lastFrameMultiplier);
            var frameDurationTimeSpan = TimeSpan.FromSeconds(frameDuration);
            var lastFrameDurationTimeSpan = TimeSpan.FromSeconds(frameDuration * lastFrameMultiplier);
            int currentRepeatCount = 0;
            if (onValueChangedOnStart) {
                OnValueChangedCallback?.Invoke(frames[frameIndex]);
            }

            while (!cancellationToken.IsCancellationRequested) {
                frameSetter(frames[frameIndex]);
                // TODO: calling onvaluechanged on every sprite set might not be intended behaviour
                // Fix this when you find a use case for it
                OnValueChangedCallback?.Invoke(frames[frameIndex]);
                if (frameIndex == frames.Length - 1) {
                    if (IsContinuous() || ++currentRepeatCount < repeatCount) {
                        frameIndex = 0;
                    }
                    await UniTask.Delay(lastFrameDurationTimeSpan, cancellationToken: cancellationToken)
                                 .SuppressCancellationThrow();
                    if (currentRepeatCount == repeatCount) {
                        break;
                    }
                } else {
                    // TODO: Implement Clamp, Loop and PingPong loop modes for this and Tween.TweenRoutine
                    ++frameIndex;
                    await UniTask.Delay(frameDurationTimeSpan, cancellationToken: cancellationToken)
                                 .SuppressCancellationThrow();
                }
            }
            if (!isForceStopped && !stayOnLastFrame && animatedComponent != null) {
                frameSetter(null);
            }
        }
    }
}