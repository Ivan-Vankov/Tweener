using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Vaflov {
    public class TweenTracker : MonoBehaviour {
        public HashSet<ITween> tweens = new HashSet<ITween>();

        public static TweenTracker _instance;
        public static TweenTracker Instance { 
            get {
                if (_instance == null) {
                    var tweenTrackerObj = new GameObject("Tween Tracker");
                    DontDestroyOnLoad(tweenTrackerObj);
                    _instance = tweenTrackerObj.AddComponent<TweenTracker>();
                }
                return _instance;
            }
        }

        public static void AddTween(ITween tween) {
            Instance.tweens.Add(tween);
        }

        public static void RemoveTweenCTS(ITween tween) {
            Instance.tweens.Remove(tween);
        }

        private void OnDestroy() {
            var tweens = Instance.tweens;
            foreach (var tween in tweens) {
                tween.ForceStop();
            }
            tweens.Clear();
        }
    }
}
