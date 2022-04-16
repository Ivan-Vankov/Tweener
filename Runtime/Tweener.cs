using UnityEngine;
using UnityEngine.UI;

namespace Vaflov {
    public static class Tweener {

        public static SpriteSheetAnimation SpriteSheet(SpriteRenderer renderer, Sprite[] frames) {
            return new SpriteSheetAnimation(frames, frame => {
                if (renderer) {
                    renderer.sprite = frame;
                }
            }).Start(renderer);
        }

        public static SpriteSheetAnimation SpriteSheet(Image image, Sprite[] frames) {
            return new SpriteSheetAnimation(frames, frame => {
                if (image) {
                    image.sprite = frame;
                }
            }).Start(image);
        }

        public static Tween<Vector3> Scale(Transform transform, Vector3 fromTargetScale, Vector3 toTargetScale) {
            return new Tween<Vector3>(fromTargetScale, toTargetScale, v => {
                if (transform) {
                    transform.localScale = v;
                }
            }).Start(transform);
        }

        public static Tween<Vector3> Scale(Transform transform, Vector3 toTargetScale) {
            return Scale(transform, transform.localScale, toTargetScale);
        }

        public static Tween<Vector2> SizeDelta(RectTransform rectTransform,
                                               Vector2 fromSizeDelta,
                                               Vector2 toSizeDelta) {
            return new Tween<Vector2>(fromSizeDelta,
                                      toSizeDelta,
                                      v => {
                                          if (rectTransform) {
                                              rectTransform.sizeDelta = v;
                                          }
                                      }).Start(rectTransform);
        }

        public static Tween<Vector2> SizeDelta(RectTransform rectTransform, Vector2 toSizeDelta) {
            return SizeDelta(rectTransform, rectTransform.sizeDelta, toSizeDelta);
        }

        // TODO: Make this work for a Renderer too
        public static Tween<Color> Color(SpriteRenderer renderer, Color toColor) {
            return Color(renderer, renderer.color, toColor);
        }

        public static Tween<Color> Color(Graphic graphic, Color toColor) {
            return Color(graphic, graphic.color, toColor);
        }

        public static Tween<Color> Color(SpriteRenderer renderer, Color fromColor, Color toColor) {
            return new Tween<Color>(fromColor, toColor, color => {
                if (renderer) {
                    renderer.color = color;
                }
            }).Start(renderer);
        }

        public static Tween<Color> Color(Graphic graphic, Color fromColor, Color toColor) {
            return new Tween<Color>(fromColor, toColor, color => {
                if (graphic) {
                    graphic.color = color;
                }
            }).Start(graphic);
        }

        public static Tween<float> Alpha(SpriteRenderer renderer, float toAlpha) {
            return Alpha(renderer, renderer.color.a, toAlpha);
        }

        public static Tween<float> Alpha(Graphic graphic, float toAlpha) {
            return Alpha(graphic, graphic.color.a, toAlpha);
        }

        public static Tween<float> Alpha(SpriteRenderer renderer, float fromAlpha, float toAlpha) {
            return new Tween<float>(fromAlpha, toAlpha, alpha => {
                if (renderer) {
                    var newColor = renderer.color;
                    newColor.a = alpha;
                    renderer.color = newColor;
                }
            }).Start(renderer);
        }

        public static Tween<float> Alpha(Graphic graphic, float fromAlpha, float toAlpha) {
            return new Tween<float>(fromAlpha, toAlpha, alpha => {
                if (graphic) {
                    var newColor = graphic.color;
                    newColor.a = alpha;
                    graphic.color = newColor;
                }
            }).Start(graphic);
        }

        public static Tween<Vector3> Move(Transform toMove, Vector3 fromPosition, Vector3 toPosition) {
            return new Tween<Vector3>(fromPosition, toPosition, pos => {
                if (toMove) {
                    toMove.position = pos;
                }
            }).Start(toMove);
        }

        public static Tween<Vector3> Move(Transform toMove, Vector3 toPosition) {
            return Move(toMove, toMove.position, toPosition);
        }
    }
}