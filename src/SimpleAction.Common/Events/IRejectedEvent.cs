namespace SimpleAction.Common.Events
{
    public interface IRejectedEvent:IEvent
    {
         string Reason  {get;}
         //This could be enum for tanslation, string used for simplicity
         string code {get;}
    }
}