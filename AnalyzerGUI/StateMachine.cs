// Code für die State Machine

using System;
using System.Threading.Tasks;

public enum State
{
    Idle,
    Result,
    Query,
    Answer,
    Push
}

public enum Event
{
    ResultSent,
    QuerySent,
    AckReceived
}

public class StateMachine
{
    private State currentState = State.Idle;

    public async Task ProcessEventAsync(Event ev, string message, Action<string> displayCallback)
    {
        switch (currentState)
        {
            case State.Idle:
                ProcessIdleState(ev, message, displayCallback);
                break;

            case State.Result:
                ProcessResultState(ev, message, displayCallback);
                break;

            case State.Query:
                ProcessQueryState(ev, message, displayCallback);
                break;

            case State.Answer:
                // No events expected in Answer state
                break;

            case State.Push:
                ProcessPushState(ev, message, displayCallback);
                break;
        }
    }

    private void ProcessIdleState(Event ev, string message, Action<string> displayCallback)
    {
        if (ev == Event.ResultSent && message.StartsWith("R| "))
        {
            currentState = State.Result;
            displayCallback("<ACK>");
            displayCallback("Result received from the Analyzer");
        }
        else if (ev == Event.QuerySent && message.StartsWith("Q| "))
        {
            currentState = State.Query;
            displayCallback("<ACK>");
            displayCallback("Query received from the Analyzer");
        }
        else
        {
            displayCallback("<NAK>");
            displayCallback("Message received is not in the correct format");
        }
    }

    private void ProcessResultState(Event ev, string message, Action<string> displayCallback)
    {
        if (ev == Event.ResultSent && message.StartsWith("R| "))
        {
            displayCallback("<ACK>");
            displayCallback("Result received from the Analyzer");
        }
        else
        {
            displayCallback("<NAK>");
            displayCallback("Message received is not in the correct format for Result");
        }
    }

    private void ProcessQueryState(Event ev, string message, Action<string> displayCallback)
    {
        if (ev == Event.QuerySent && message.StartsWith("Q| "))
        {
            displayCallback("<ACK>");
            displayCallback("Query received from the Analyzer");
        }
        else
        {
            displayCallback("<NAK>");
            displayCallback("Message received is not in the correct format for Query");
        }
    }

    private void ProcessPushState(Event ev, string message, Action<string> displayCallback)
    {
        if (ev == Event.AckReceived && message.StartsWith("P| "))
        {
            displayCallback(message.Substring(3)); // Display push answer
        }
        else
        {
            displayCallback("<NAK>");
            displayCallback("Message received is not in the correct format for Push");
        }
    }
}
