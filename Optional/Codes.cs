

namespace Optionals
{
    public static class Codes
    {
        public static Code Success = new(CodesNumbers.Success, "Successful operation");
        public static Code NotFound = new(CodesNumbers.NotFound, "Item not found");

        public static Code DbError(string message) => new(CodesNumbers.DbError, message);
    }
}