using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ads.Persistence
{
  public  class LoggerHelper
  {

    public static void LogInformation(string message, int? id = null)
    {
      Log.Logger.Information($"{message} - Id: {id}");
    }

    public static void LogWarning(string message, int? id = null)
    {
      Log.Logger.Warning($"{message} - Id: {id}");
    }

    public static void LogError(Exception ex, string message, int? id = null)
    {
      Log.Logger.Error(ex, $"{message} - Id: {id}");
    }
  }
}
