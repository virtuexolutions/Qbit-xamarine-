using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.APILog
{
    public static class LogManager
    {
        /// <summary>
        /// this method is used to trace error log
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void TraceErrorLog(Exception errorMessage)
        {
            try
            {
                Crashes.TrackError(errorMessage);
            }
            catch (Exception ex)
            {
                TraceErrorLog(ex);
                LogManager.TraceErrorLog(ex);
            }
        }

        public static void TraceLogAndEvents(string eventName,Dictionary<string,string> logs)
        {
            try
            {
                Analytics.TrackEvent(eventName, logs);
            }
            catch (Exception ex)
            {
                TraceErrorLog(ex);
                LogManager.TraceErrorLog(ex);
            }
        }

    }
}
