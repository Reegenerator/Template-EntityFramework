﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Kodeo.Reegenerator.Generators;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Debugger = System.Diagnostics.Debugger;

namespace RgenLib.Extensions {
    public static class Debug {

        #region Debug helpers
        /// <summary>
        /// Get string representation of Type.Member value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string DebugMember<T>(this IEnumerable<T> list, string memberName) {
            var sb = new StringBuilder();

            foreach (var x in list) {
                var text = Versioned.CallByName(x, memberName, CallType.Get).ToString();
                sb.AppendLine(text);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get string value of specified member of each item in a list as type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<T> DebugMembers<T>(this IEnumerable list, string memberName) {
            var res = from object x in list
                      select Versioned.CallByName(x, memberName, CallType.Get);

            return res.Cast<T>();
        }

        /// <summary>
        /// Get value of specified member of each item in a list as string array
        /// </summary>
        /// <param name="list"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string[] DebugMembers(this IEnumerable list, string memberName) {
            return list.DebugMembers<string>(memberName).ToArray();
        }

        /// <summary>
        /// Like DebugMember, but with two members
        /// </summary>
        /// <param name="list"></param>
        /// <param name="memberName"></param>
        /// <param name="secondMemberName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static object DebugMembers(this IEnumerable list, string memberName, string secondMemberName) {
            var res = from object x in list
                      select new {
                          Member1 = Versioned.CallByName(x, memberName, CallType.Get),
                          Member2 = Versioned.CallByName(x, secondMemberName, CallType.Get)
                      };

            return res.ToArray();

        }

        /// <summary>
        /// Alias for DebugPosition
        /// </summary>
        /// <param name="point"></param>
        /// <param name="charCount"></param>
        /// <param name="listener"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string DP(this EnvDTE.TextPoint point, int charCount = 10, OutputPaneTraceListener listener = null) {
            return DebugPosition(point, charCount, listener);
        }

        /// <summary>
        /// Show the position of a textpoint by printing the surrounding text
        /// </summary>
        /// <param name="point"></param>
        /// <param name="charCount"></param>
        /// <param name="listener"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string DebugPosition(this EnvDTE.TextPoint point, int charCount = 50, OutputPaneTraceListener listener = null) {

            var start = point.CreateEditPoint();
            var text = string.Format("{0}>|<{1}", start.GetText(-charCount), start.GetText(charCount));
            if (listener != null) {
                listener.WriteLine(text);
            }
            return text;

        }

        private static OutputPaneTraceListener _traceListener;
        /// <summary>
        /// Do not use this method ( OutputPaneTraceListener will not ready, and will throw an exception) within constructor, do it in PreRender or Render
        /// </summary>
        /// <param name="listener"></param>
        static public void SetTraceListener(OutputPaneTraceListener listener) {
         
            if (_traceListener != null) {
                if (MessageBox.Show("There's already a TraceListener, replace?",
                        "Set default Reegenerator Template trace listener", MessageBoxButton.YesNo) !=
                    MessageBoxResult.Yes) {
                    return;
                }
            }
            _traceListener = listener;
        }

        private static bool DebugSkipped;
        /// <summary>
        /// Launch debugger or Break if it's already attached
        /// </summary>
        /// <remarks></remarks>
        public static void DebugHere(Exception ex = null) {
            if (ex != null) {
                //for resharper, copy exception
                Clipboard.SetText(ex.ToString());
                if (_traceListener != null) {
                   _traceListener.WriteLine(ex.ToString());
                }
            }
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) {
                //don't break or launch if shift is held down
                return;
            }
            if (Debugger.IsAttached) {
                Debugger.Break();
            }
            else {
                //If debug is cancelled once, stop trying to launch
                if (DebugSkipped) {
                    return;
                }
                var launched = Debugger.Launch();
                if (!launched) {
                    DebugSkipped = true;
                }

            }

        }
        #endregion

    }

}