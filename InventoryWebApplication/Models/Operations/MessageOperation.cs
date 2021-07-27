namespace InventoryWebApplication.Models.Operations
{
    public class MessageOperation
    {
        public static MessageOperation Empty => new();
        
        public bool IsEmpty => Message.Equals(string.Empty);
        public string Message { get; }
        public MessageSeverity Severity { get; }
        
        public MessageOperation()
        {
            Message = "";
        }
        
        public MessageOperation(string message)
        {
            Message = message;
            Severity = MessageSeverity.warning;
        }
        
        public MessageOperation(string message, MessageSeverity severity)
        {
            Message = message;
            Severity = severity;
        }
    }
}