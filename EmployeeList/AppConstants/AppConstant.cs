namespace EmployeeList.AppConstants;

public class AppConstant
{
    public class Input
    {
        public const char ARGS_SEPARATOR = ':';
    }
    public class ExitCodes
    {
        public const int OK                = 0;
        public const int INVALID_PATH      = 1;
        public const int HAVE_NO_ARGS      = 2;
        public const int INVALID_ARGS      = 3;
        public const int UNKNOWN_COMMAND   = 5;
        public const int ERROR             = 6;
  
        public const int CANNOT_ADD        = 7;
        public const int CANNOT_UPDATE     = 8;
        public const int CANNOT_DELETE     = 9;
        public const int CANNOT_GET        = 10;
        public const int CANNOT_GETALL     = 11;
        public const int NOTHING_TO_CHANGE = 12;
        
    }
}