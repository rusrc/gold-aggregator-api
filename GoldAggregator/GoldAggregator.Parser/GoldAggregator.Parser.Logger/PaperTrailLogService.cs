using log4net;
using log4net.Config;

using System;
using System.Xml;

namespace GoldAggregator.Parser.Logger
{
    // https://docs.microsoft.com/en-us/dotnet/core/extensions/custom-logging-provider
    public class PaperTrailLogService : ILogService
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(PaperTrailLogService));
        public PaperTrailLogService()
        {
            var xml = @"<log4net>
                        <appender name=""PapertrailRemoteSyslogAppender"" type=""log4net.Appender.RemoteSyslogAppender"">
                          <facility value=""Local6"" />
                          <identity value=""%date{yyyy-MM-ddTHH:mm:ss.ffffffzzz} %P{log4net:HostName} GoldAggregator.Parser.Api"" />
                          <layout type=""log4net.Layout.PatternLayout"" value=""%level - %message%newline"" />
                          <remoteAddress value=""logs2.papertrailapp.com"" />
                          <remotePort value=""18548"" />
                        </appender>

                        <appender name=""AnsiColorTerminalAppender"" type=""log4net.Appender.AnsiColorTerminalAppender"">
                            <mapping>
                            <level value=""INFO"" />
                            <forecolor value=""Green"" />
                            </mapping>
                            <mapping>
                            <level value=""ERROR"" />
                            <forecolor value=""Red"" />
                            </mapping>
                            <mapping>
                            <level value=""DEBUG"" />
                            <forecolor value=""Yellow"" />
                            </mapping>
                            <layout type=""log4net.Layout.PatternLayout"">
                            <conversionpattern value=""%date [%thread] %-5level - %message%newline"" />
                            </layout>
                        </appender>
                        <root>
                          <level value=""ALL"" />
                          <appender-ref ref=""AnsiColorTerminalAppender"" />
                          <appender-ref ref=""PapertrailRemoteSyslogAppender"" />

                        </root>
                      </log4net>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var repo = LogManager.CreateRepository(
               System.Reflection.Assembly.GetEntryAssembly(),
               typeof(log4net.Repository.Hierarchy.Hierarchy)
            );

            XmlConfigurator.Configure(repo, doc.DocumentElement);
        }

        public void Info(string msg)
        {
            _log.Info(msg);
        }

        public void Info(string msg, Exception ex)
        {
            _log.Info(msg, ex);
        }

        public void Debug(string msg)
        {
            _log.Debug(msg);
        }

        public void Debug(string msg, Exception ex)
        {
            _log.Debug(msg, ex);
        }

        public void Warn(string msg)
        {
            _log.Warn(msg);
        }

        public void Warn(string msg, Exception ex)
        {
            _log.Warn(msg, ex);
        }

        public void Error(string msg)
        {
            _log.Error(msg);
        }

        public void Error(string msg, Exception ex)
        {
            _log.Error(msg, ex);
        }
    }
}
