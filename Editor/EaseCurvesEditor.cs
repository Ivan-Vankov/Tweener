using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Linq;

namespace Vaflov {
    public class EaseCurvesEditorWindow : EditorWindow {
        public Dictionary<EaseType, AnimationCurve> curves = new Dictionary<EaseType, AnimationCurve>();

        [MenuItem("Window/Tweener/Ease Curves Editor")]
        public static void ShowWindow() {
            GetWindow<EaseCurvesEditorWindow>("Tweener Ease Curves Editor").Show();
        }

        public void OnGUI() {
            foreach (var easeType in (EaseType[])Enum.GetValues(typeof(EaseType))) {
                var easeTypeName = easeType.ToString();
                if (easeType.ToString().StartsWith("BELL")) {
                    continue;
                }
                var editedCurve = curves.ContainsKey(easeType)
                    ? curves[easeType]
                    : EaseCurvesExport.easeCurves[easeType];
                var nameSplit = easeTypeName.Split('_');
                var curveName = string.Join(" ", nameSplit.Select(str => str.Substring(0, 1).ToUpper()
                                                               + (str.Length > 1 ? str.Substring(1).ToLower() : "")));
                curves[easeType] = EditorGUILayout.CurveField(curveName, editedCurve);
            }
            if (GUILayout.Button("Export Curves")) {
                ExportCurves();
            }
        }

        public void ExportCurves() {
            StringBuilder textCurveBuilder = new StringBuilder();
            var className = "EaseCurvesExport";
            textCurveBuilder
                .AppendLine("////////////////////////////////////////////////////////////////////")
                .AppendLine("/////////////////// AUTOMATICALLY GENERATED FILE ///////////////////")
                .AppendLine("////////////////////////////////////////////////////////////////////")
                .AppendLine()
                .AppendLine("using System;")
                .AppendLine("using System.Collections.Generic;")
                .AppendLine("using UnityEngine;")
                .AppendLine()
                .AppendLine("namespace Vaflov {")
                .AppendLine($"\tpublic static class {className} {{")
                .AppendLine("\t\tpublic static readonly Dictionary<EaseType, EaseCurve> easeCurves;")
                .AppendLine()
                .AppendLine($"\t\tstatic {className}() {{")
                .AppendLine("\t\t\teaseCurves = new Dictionary<EaseType, EaseCurve>(Enum.GetValues(typeof(EaseType)).Length) {");

            foreach (var easeType in (EaseType[])Enum.GetValues(typeof(EaseType))) {
                var easeString = easeType.ToString();
                EaseCurve easeCurve = null;
                if (easeType.ToString().StartsWith("BELL")) {
                    if (!Enum.TryParse("IN_OUT" + easeString.Substring(4), out EaseType inOutEaseType)) {
                        continue;
                    }
                    easeCurve = new EaseCurve(curves[inOutEaseType], 2, WrapMode.PingPong);
                } else {
                    easeCurve = new EaseCurve(curves[easeType]);
                }

                textCurveBuilder
                    .Append("\t\t\t\t{ EaseType.")
                    .Append(easeType.ToString())
                    .Append(", new EaseCurve(");

                static string AppendF(float f) => f + (f % 1 == 0 ? "" : "f");
                Keyframe a = new Keyframe {
                    inTangent = 1,
                    weightedMode = WeightedMode.None,
                };
                if (easeCurve != null) {
                    textCurveBuilder.Append(AppendF(easeCurve.durationMultiplier)).Append(", ");
                    textCurveBuilder.Append("WrapMode.").Append(easeCurve.postWrapMode).Append(", ");
                    textCurveBuilder.Append("WrapMode.").Append(easeCurve.preWrapMode).Append(", ");

                    for (int i = 0; i < easeCurve.keys.Length; i++) {
                        var key = easeCurve.keys[i];
                        textCurveBuilder.Append("new Keyframe { ")
                            .Append("time = ").Append(AppendF(key.time)).Append(", ")
                            .Append("value = ").Append(AppendF(key.value));
                        bool setTangents = false;
                        if (key.inTangent != 0 || key.outTangent != 0) {
                            textCurveBuilder
                                .Append(", inTangent = ").Append(AppendF(key.inTangent))
                                .Append(", outTangent = ").Append(AppendF(key.outTangent));
                            setTangents = true;
                        }
                        if (key.inWeight != 0 || key.outWeight != 0) {
                            if (!setTangents) {
                                textCurveBuilder
                                    .Append(", inTangent = ").Append(AppendF(key.inTangent))
                                    .Append(", outTangent = ").Append(AppendF(key.outTangent));
                            }
                            textCurveBuilder
                                .Append(", inWeight = ").Append(AppendF(key.inWeight))
                                .Append(", outWeight = ").Append(AppendF(key.outWeight));
                        }
                        textCurveBuilder.Append(", weightedMode = WeightedMode.").Append(key.weightedMode).Append(" }");

                        if (i < easeCurve.keys.Length - 1) {
                            textCurveBuilder.Append(", ");
                        }
                    }
                }
                textCurveBuilder.Append(") },").AppendLine();
            }
            textCurveBuilder
                .AppendLine("\t\t\t};")
                .AppendLine("\t\t}")
                .AppendLine("\t}")
                .AppendLine("}");

            var currentFilePath = Path.GetFullPath(new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName());
            var separator = Path.DirectorySeparatorChar;
            var withoutClassName = currentFilePath.Substring(0, currentFilePath.LastIndexOf(separator));
            var withoutEditor = currentFilePath.Substring(0, withoutClassName.LastIndexOf(separator));
            var exportFilePath = withoutEditor + $"{separator}Export{separator}{className}.cs";

            using var file = new StreamWriter(exportFilePath, append: false);
            file.Write(textCurveBuilder);
            file.Flush();
            Debug.Log($"Exported Tweener ease curves to: {exportFilePath}");
            textCurveBuilder.Clear();
        }
    }
}