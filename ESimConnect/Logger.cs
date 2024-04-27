using Microsoft.FlightSimulator.SimConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ESimConnect
{
  internal static class Logger
  {
    public static Action<string>? LogHandler { get; set; } = null;
    public static void Log(string msg, Type? sender = null)
    {
      if (sender != null) msg = $"[{sender.Name}] {msg}";
      LogInternal(msg);
    }
    public static void Log(string msg, object? sender = null)
    {
      if (sender != null) msg = $"[{sender.GetType().Name}] {msg}";
      LogInternal(msg);
    }

    internal static void LogInvokedEvent(object sender, string eventName, object data)
    {
      List<string> tmp = new List<string>()
        .Union(ExpandFields(data))
        .Union(ExpandProperties(data))
        .OrderBy(q => q)
        .ToList();

      string msg = $"{sender.GetType().Name}.{eventName} ({string.Join(",", tmp)}";
      LogInternal(msg);
    }

    private static List<string> ExpandProperties(object obj)
    {
      var ret = obj.GetType().GetProperties()
        .Select(q => $"{q.Name}={q.GetValue(obj)}")
        .ToList();
      return ret;
    }

    private static List<string> ExpandFields(object obj)
    {
      var ret = obj.GetType().GetFields()
        .Select(q => $"{q.Name}={q.GetValue(obj)}")
        .ToList();
      return ret;
    }

    internal static void LogMethodEnd(
      [CallerMemberName] string methodName = "injectedMemberName",
      [CallerFilePath] string sourceFilePath = "injectedFilePath",
      [CallerLineNumber] int sourceLineNumber = 0)
    {
      var fileNameOnly = System.IO.Path.GetFileName(sourceFilePath);
      LogInternal($"{fileNameOnly}({sourceLineNumber}):{methodName}(...) end");
    }

    internal static void LogMethodStart(
      object?[]? parameters = null,
      [CallerMemberName] string methodName = "injectedMemberName",
      [CallerFilePath] string sourceFilePath = "injectedFilePath",
      [CallerLineNumber] int sourceLineNumber = 0
      )
    {
      var paramString = string.Join(",", parameters ?? Array.Empty<object>());
      var fileNameOnly = System.IO.Path.GetFileName(sourceFilePath);
      LogInternal($"{fileNameOnly}({sourceLineNumber}):{methodName}({paramString})");
    }

    //internal static void LogStaticMethodEnd(string methodName, Type sender)
    //{
    //  Log(methodName + "() end", sender);
    //  LogInternal($"{sender.Name}.{methodName}() end");
    //}

    //internal static void LogStaticMethodStart(string methodName, Type sender, params object[] parameters)
    //{
    //  var paramString = string.Join(",", parameters);
    //  LogInternal($"{sender.Name}.{methodName}({paramString})");
    //}

    private static void LogInternal(string msg)
    {
      LogHandler?.Invoke(msg + "\n");
    }

    internal static void LogException(Exception ex,
      [CallerMemberName] string methodName = "injectedMemberName",
      [CallerFilePath] string sourceFilePath = "injectedFilePath",
      [CallerLineNumber] int sourceLineNumber = 0)
    {
      Exception? tmp = ex;
      List<string> lst = new();
      while (tmp != null)
      {
        lst.Add(tmp.Message);
        lst.Add(" /(");
        lst.Add(tmp.StackTrace ?? "");
        lst.Add(")/ ");
        tmp = tmp.InnerException;
      }

      var fileNameOnly = System.IO.Path.GetFileName(sourceFilePath);
      LogInternal($"{fileNameOnly}({sourceLineNumber}):{methodName}(...) throws exception {tmp}");
    }
  }
}
