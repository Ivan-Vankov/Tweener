using UnityEngine;

namespace Vaflov {
    public enum EaseType {
        LINEAR,
        IN_SLOW,
        IN_MEDIUM,
        IN_FAST,
        IN_FASTEST,
        IN_OVERSHOOT,
        IN_ELASTIC,
        IN_BOUNCE,
        OUT_SLOW,
        OUT_MEDIUM,
        OUT_FAST,
        OUT_FASTEST,
        OUT_OVERSHOOT,
        OUT_ELASTIC,
        OUT_BOUNCE,
        IN_OUT_SLOW,
        IN_OUT_MEDIUM,
        IN_OUT_FAST,
        IN_OUT_FASTEST,
        IN_OUT_OVERSHOOT,
        IN_OUT_ELASTIC,
        IN_OUT_BOUNCE,
        BELL_SLOW,
        BELL_MEDIUM,
        BELL_FAST,
        BELL_FASTEST,
        BELL_OVERSHOOT,
        BELL_ELASTIC,
        BELL_BOUNCE,
    }

    public class EaseCurve : AnimationCurve {
        public float durationMultiplier;

        public EaseCurve(AnimationCurve animationCurve,
                         float durationMultiplier = 1,
                         WrapMode postWrapMode = WrapMode.Default,
                         WrapMode preWrapMode = WrapMode.Default) : base(animationCurve.keys) {
            this.durationMultiplier = durationMultiplier;
            this.postWrapMode = postWrapMode == WrapMode.Default ? animationCurve.postWrapMode : postWrapMode;
            this.preWrapMode = preWrapMode == WrapMode.Default ? animationCurve.preWrapMode : preWrapMode;
        }

        public EaseCurve(float durationMultiplier = 1,
                         WrapMode postWrapMode = WrapMode.Default,
                         WrapMode preWrapMode = WrapMode.Default,
                         params Keyframe[] keys) : base(keys) {
            this.durationMultiplier = durationMultiplier;
            this.postWrapMode = postWrapMode;
            this.preWrapMode = preWrapMode;
        }

        public static EaseCurve GetEaseCurve(EaseType easeType) {
            return EaseCurvesExport.easeCurves[easeType];
        }
    }
}