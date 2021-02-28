namespace Optionals
{
    public class Code
    {
        public Code(CodesNumbers codesNumbers, string codeMessage)
        {
            CodesNumbers = codesNumbers;
            CodeMessage = codeMessage;
        }

        public CodesNumbers CodesNumbers { get; init; }
        public string CodeMessage { get; init; }
    }
    
    public enum CodesNumbers
    {
        Success,
        NotFound,
        DbError
    }
}