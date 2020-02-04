namespace Drawer3D.Model.Extensions
{
    public static class NumberExtensions
    {
        public static double ToMilli(this int number)
        {
            return number / (double) 1000;
        }

        public static double ToMilli(this double number)
        {
            return number / 1000;
        }
    }
}