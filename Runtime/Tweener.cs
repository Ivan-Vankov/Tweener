using System;
using UnityEngine;
using UnityEngine.UI;

namespace Vaflov {
    public static partial class Tweener {
        public static SpriteSheetAnimation CreateSpriteSheetAnimation(SpriteRenderer renderer, Sprite[] frames) {
            return new SpriteSheetAnimation(frames, renderer.SetSprite).AnimatedComponent(renderer);
        }

        public static SpriteSheetAnimation CreateSpriteSheetAnimation(Image image, Sprite[] frames) {
            return new SpriteSheetAnimation(frames, image.SetSprite).AnimatedComponent(image);
        }

        public static SpriteSheetAnimation SpriteSheet(SpriteRenderer renderer, Sprite[] frames) {
            return CreateSpriteSheetAnimation(renderer, frames).Start();
        }

        public static SpriteSheetAnimation SpriteSheet(Image image, Sprite[] frames) {
            return CreateSpriteSheetAnimation(image, frames).Start();
        }

        public static Tween<Vector3> CreateScaleTween(Transform transform, Vector3 fromTargetScale, Vector3 toTargetScale) {
            return new Tween<Vector3>(fromTargetScale, toTargetScale, transform.SetLocalScale).AnimatedComponent(transform);
        }

        public static Tween<Vector3> CreateScaleTween(Transform transform, Vector3 toTargetScale) {
            return CreateScaleTween(transform, transform.localScale, toTargetScale);
        }

        public static Tween<Vector3> Scale(Transform transform, Vector3 fromTargetScale, Vector3 toTargetScale) {
            return CreateScaleTween(transform, fromTargetScale, toTargetScale).Start();
        }

        public static Tween<Vector3> Scale(Transform transform, Vector3 toTargetScale) {
            return CreateScaleTween(transform, toTargetScale).Start();
        }

        public static Tween<Vector2> CreateSizeDeltaTween(RectTransform rectTransform,
                                                          Vector2 fromSizeDelta,
                                                          Vector2 toSizeDelta) {
            return new Tween<Vector2>(fromSizeDelta, toSizeDelta, rectTransform.SetSizeDelta).AnimatedComponent(rectTransform);
        }

        public static Tween<Vector2> CreateSizeDeltaTween(RectTransform rectTransform, Vector2 toSizeDelta) {
            return CreateSizeDeltaTween(rectTransform, rectTransform.sizeDelta, toSizeDelta);
        }

        public static Tween<Vector2> SizeDelta(RectTransform rectTransform,
                                               Vector2 fromSizeDelta,
                                               Vector2 toSizeDelta) {
            return CreateSizeDeltaTween(rectTransform, fromSizeDelta, toSizeDelta).Start();
        }

        public static Tween<Vector2> SizeDelta(RectTransform rectTransform, Vector2 toSizeDelta) {
            return CreateSizeDeltaTween(rectTransform, toSizeDelta).Start();
        }

        public static Tween<Color> CreateColorTween(SpriteRenderer renderer, Color fromColor, Color toColor) {
            return new Tween<Color>(fromColor, toColor, renderer.SetColor).AnimatedComponent(renderer);
        }

        public static Tween<Color> CreateColorTween(SpriteRenderer renderer, Color toColor) {
            return CreateColorTween(renderer, renderer.color, toColor);
        }

        public static Tween<Color> CreateColorTween(Graphic graphic, Color fromColor, Color toColor) {
            return new Tween<Color>(fromColor, toColor, graphic.SetColor).AnimatedComponent(graphic);
        }

        public static Tween<Color> CreateColorTween(Graphic graphic, Color toColor) {
            return CreateColorTween(graphic, graphic.color, toColor);
        }

        public static Tween<Color> Color(SpriteRenderer renderer, Color toColor) {
            return CreateColorTween(renderer, toColor).Start();
        }

        public static Tween<Color> Color(Graphic graphic, Color toColor) {
            return CreateColorTween(graphic, toColor).Start();
        }

        public static Tween<Color> Color(SpriteRenderer renderer, Color fromColor, Color toColor) {
            return CreateColorTween(renderer, fromColor, toColor).Start();
        }

        public static Tween<Color> Color(Graphic graphic, Color fromColor, Color toColor) {
            return CreateColorTween(graphic, fromColor, toColor).Start();
        }

        public static Tween<float> CreateAlphaTween(SpriteRenderer renderer, float fromAlpha, float toAlpha) {
            return new Tween<float>(fromAlpha, toAlpha, renderer.SetAlpha).AnimatedComponent(renderer);
        }

        public static Tween<float> CreateAlphaTween(SpriteRenderer renderer, float toAlpha) {
            return CreateAlphaTween(renderer, renderer.color.a, toAlpha);
        }
        public static Tween<float> CreateAlphaTween(Graphic graphic, float fromAlpha, float toAlpha) {
            return new Tween<float>(fromAlpha, toAlpha, graphic.SetAlpha).AnimatedComponent(graphic);
        }

        public static Tween<float> CreateAlphaTween(Graphic graphic, float toAlpha) {
            return CreateAlphaTween(graphic, graphic.color.a, toAlpha);
        }

        public static Tween<float> Alpha(SpriteRenderer renderer, float toAlpha) {
            return CreateAlphaTween(renderer, toAlpha).Start();
        }

        public static Tween<float> Alpha(Graphic graphic, float toAlpha) {
            return CreateAlphaTween(graphic, toAlpha).Start();
        }

        public static Tween<float> Alpha(SpriteRenderer renderer, float fromAlpha, float toAlpha) {
            return CreateAlphaTween(renderer, fromAlpha, toAlpha).Start();
        }

        public static Tween<float> Alpha(Graphic graphic, float fromAlpha, float toAlpha) {
            return CreateAlphaTween(graphic, fromAlpha, toAlpha).Start();
        }

        public static Tween<Vector3> CreateMoveTween(Transform toMove, Vector3 fromPosition, Vector3 toPosition) {
            return new Tween<Vector3>(fromPosition, toPosition, toMove.SetPosition).AnimatedComponent(toMove);
        }

        public static Tween<Vector3> CreateMoveTween(Transform toMove, Vector3 toPosition) {
            return CreateMoveTween(toMove, toMove.position, toPosition);
        }

        public static Tween<Vector3> Move(Transform toMove, Vector3 fromPosition, Vector3 toPosition) {
            return CreateMoveTween(toMove, fromPosition, toPosition).Start();
        }

        public static Tween<Vector3> Move(Transform toMove, Vector3 toPosition) {
            return CreateMoveTween(toMove, toPosition).Start();
        }

        public static Tween<float> CreateRotateTween(Transform toRotate, Vector3 fromEulerAngles, Vector3 toEulerAngles, Space space = Space.Self) {
            Action<float> setter;
            if (space == Space.World)
                setter = pct => {
                    toRotate.eulerAngles = fromEulerAngles;
                    var targetEulerAngles = Vector3.LerpUnclamped(fromEulerAngles, toEulerAngles, pct);
                    toRotate.Rotate(targetEulerAngles, space);
                };
            else
                setter = pct => {
                    toRotate.localEulerAngles = fromEulerAngles;
                    var targetEulerAngles = Vector3.LerpUnclamped(fromEulerAngles, toEulerAngles, pct);
                    toRotate.Rotate(targetEulerAngles, space);
                };
            return new Tween<float>(0, 1, setter).AnimatedComponent(toRotate);
        }

        public static Tween<float> CreateRotateTween(Transform toRotate, Vector3 toEulerAngles, Space space = Space.Self) {
            return CreateRotateTween(toRotate,
                space == Space.World ? toRotate.eulerAngles : toRotate.localEulerAngles,
                toEulerAngles,
                space);
        }

        public static Tween<float> CreateRotateTween(Transform toRotate, float fromX, float fromY, float fromZ, float toX, float toY, float toZ, Space space = Space.Self) {
            return CreateRotateTween(toRotate, new Vector3(fromX, fromY, fromZ), new Vector3(toX, toY, toZ), space);
        }

        public static Tween<float> CreateRotateTween(Transform toRotate, float toX, float toY, float toZ, Space space = Space.Self) {
            return CreateRotateTween(toRotate, new Vector3(toX, toY, toZ), space);
        }

        public static Tween<float> CreateRotateTween(Transform toRotate, Vector3 axis, float angle, Space space = Space.Self) {
            var initialEulerAngles = space == Space.World ? toRotate.eulerAngles : toRotate.localEulerAngles;
            Action<float> setter;
            if (space == Space.World)
                setter = pct => {
                    toRotate.eulerAngles = initialEulerAngles;
                    toRotate.Rotate(axis, pct * angle);
                };
            else
                setter = pct => {
                    toRotate.localEulerAngles = initialEulerAngles;
                    toRotate.Rotate(axis, pct * angle);
                };
            return new Tween<float>(0, 1, setter).AnimatedComponent(toRotate);
        }

        public static Tween<float> Rotate(Transform toRotate, Vector3 fromEulerAngles, Vector3 toEulerAngles, Space space = Space.Self) {
            return CreateRotateTween(toRotate, fromEulerAngles, toEulerAngles, space).Start();
        }

        public static Tween<float> Rotate(Transform toRotate, Vector3 toEulerAngles, Space space = Space.Self) {
            return CreateRotateTween(toRotate, toEulerAngles, space).Start();
        }

        public static Tween<float> Rotate(Transform toRotate, float fromX, float fromY, float fromZ, float toX, float toY, float toZ, Space space = Space.Self) {
            return CreateRotateTween(toRotate, fromX, fromY, fromZ, toX, toY, toZ, space).Start();
        }

        public static Tween<float> Rotate(Transform toRotate, float toX, float toY, float toZ, Space space = Space.Self) {
            return CreateRotateTween(toRotate, toX, toY, toZ, space).Start();
        }

        public static Tween<float> Rotate(Transform toRotate, Vector3 axis, float angle, Space space = Space.Self) {
            return CreateRotateTween(toRotate, axis, angle, space).Start();
        }
    }

    public static class SpriteRendererEntender {
        public static void SetSprite(this SpriteRenderer self, Sprite sprite) {
            if (self)
                self.sprite = sprite;
        }

        public static void SetColor(this SpriteRenderer self, Color color) {
            if (self)
                self.color = color;
        }

        public static void SetAlpha(this SpriteRenderer self, float alpha) {
            if (self) {
                var newColor = self.color;
                newColor.a = alpha;
                self.color = newColor;
            }
        }
    }

    public static class ImageEntender {
        public static void SetSprite(this Image self, Sprite sprite) {
            if (self)
                self.sprite = sprite;
        }
    }

    public static class GraphicEntender {
        public static void SetColor(this Graphic self, Color color) {
            if (self)
                self.color = color;
        }

        public static void SetAlpha(this Graphic self, float alpha) {
            if (self) {
                var newColor = self.color;
                newColor.a = alpha;
                self.color = newColor;
            }
        }
    }

    public static class TransformExtender {
        public static void SetPosition(this Transform self, Vector3 position) {
            if (self)
                self.position = position;
        }

        public static void SetLocalScale(this Transform self, Vector3 localScale) {
            if (self)
                self.localScale = localScale;
        }
    }

    public static class RectTransformExtended {
        public static void SetSizeDelta(this RectTransform self, Vector2 sizeDelta) {
            if (self)
                self.sizeDelta = sizeDelta;
        }
    }

}