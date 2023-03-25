using System.Collections.Generic;
using UnityEngine;

namespace Vaflov {
    public class TweenTracker : MonoBehaviour {
        public HashSet<ITween> tweens = new HashSet<ITween>();

        public static TweenTracker _instance;
        public static TweenTracker Instance { 
            get {
                #if UNITY_EDITOR
                if (_instance == null && Application.isPlaying) {
                    var tweenTrackerObj = new GameObject("Tween Tracker");
                    DontDestroyOnLoad(tweenTrackerObj);
                    _instance = tweenTrackerObj.AddComponent<TweenTracker>();
                }
                return _instance;
                #else
                return null;
                #endif
            }
        }

        public static void AddTween(ITween tween) {
            #if UNITY_EDITOR
            var instance = Instance;
            if (instance)
                instance.tweens.Add(tween);
            #endif
        }

        public static void RemoveTweenCTS(ITween tween) {
            #if UNITY_EDITOR
            var instance = Instance;
            if (instance)
                instance.tweens.Remove(tween);
            #endif
        }

        private void OnDestroy() {
            foreach (var tween in tweens) {
                tween.ForceStop();
            }
            tweens.Clear();
        }
    }
}
