
using System;
using System.Diagnostics.Tracing;
using System.Text;

namespace dotnetTracing
{
sealed class CustomEventSourceListener : EventListener{
    
    private readonly string _eventSourceName;
    private readonly StringBuilder _mesage = new StringBuilder();
    
    public CustomEventSourceListener(string name)
    {
        _eventSourceName = name;
    }
        protected override   void OnEventSourceCreated(EventSource eventSource){
        base.OnEventSourceCreated(eventSource);
        if(eventSource.Name == _eventSourceName){
            EnableEvents(eventSource,EventLevel.LogAlways,EventKeywords.All);
        }

        //Console.WriteLine($"New event source: {eventSource.Name}");


    }

    protected override  void OnEventWritten(EventWrittenEventArgs eventData){
        base.OnEventWritten(eventData);
        string message;
        lock(_mesage){
            _mesage.Append("<- Event ");
             _mesage.Append(eventData.EventSource.Name);
            _mesage.Append(" - ");
            _mesage.Append(eventData.EventName);
            _mesage.Append(" : ");
            _mesage.AppendJoin(',', eventData.Payload);
            _mesage.AppendLine(" ->");
            message = _mesage.ToString();
            _mesage.Clear();

        }
        Console.WriteLine(message);

    }

}

}

