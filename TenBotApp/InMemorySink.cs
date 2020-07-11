using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Serilog.Formatting.Compact;

namespace TenBotApp
{
    public class InMemorySink : ILogEventSink
    {
        private readonly ITextFormatter
            _textFormatter =
                new CompactJsonFormatter(); //new MessageTemplateTextFormatter("{Timestamp} [{Level}] {Message}{Exception}");

        public ConcurrentQueue<string> Events { get; } = new ConcurrentQueue<string>();

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
            var renderSpace = new StringWriter();
            _textFormatter.Format(logEvent, renderSpace);
            Events.Enqueue(renderSpace.ToString());
        }
    }
}