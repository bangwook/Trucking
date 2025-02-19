namespace Trucking.Common
{
    public class Trucking
    {
        public const int VERSION_CODE = 1083;

        public static string GetVersionString()
        {
            int value1000 = VERSION_CODE / 1000;
            int value100 = VERSION_CODE % 1000 / 100;
            int value10 = VERSION_CODE % 100;
            //int value1 = VERSION_CODE % 10;

            return value1000 + "." + value100 + "." + value10;
        }
    }
}