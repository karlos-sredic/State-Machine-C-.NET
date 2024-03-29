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
    AckReceived,
    Timeout
}

public class StateMachine
{
    private State currentState = State.Idle;

    public async Task ProcessEventAsync(Event ev, string message, Action<string> displayCallback)
    {
        switch (currentState)
        {
            case State.Idle:
                if (ev == Event.ResultSent && message.StartsWith("R| "))
                {
                    currentState = State.Result;
                    displayCallback("<ACK>");
                    await Task.Delay(2000);
                    displayCallback(message.Substring(3)); // Display result after 2 seconds
                }
                else if (ev == Event.QuerySent && message.StartsWith("Q| "))
                {
                    currentState = State.Query;
                    displayCallback("<ACK>");
                }
                else
                {
                    displayCallback("<NAK>");
                }
                break;

            case State.Result:
                // No events expected in Result state
                break;

            case State.Query:
                if (ev == Event.AckReceived)
                {
                    currentState = State.Answer;
                    await Task.Delay(2000); // Generate automated answer after 2 seconds
                    string automatedAnswer = "A| Answer to the Query";
                    displayCallback(automatedAnswer);
                }
                break;

            case State.Answer:
                // No events expected in Answer state
                break;

            case State.Push:
                if (ev == Event.AckReceived && message.StartsWith("P| "))
                {
                    currentState = State.Push;
                    displayCallback(message.Substring(3)); // Display push answer
                }
                else
                {
                    displayCallback("<NAK>");
                }
                break;
        }
    }
}