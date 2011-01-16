using System;
namespace SystemEx.Html
{
    public class LocalStorage
    {
        public static string GetItem(string key)
        {
            try
            {
                return (string)Script.Literal("window.localStorage.getItem({0})", key);
            }
            catch (Exception e) { throw new Exception("IOException:" + e); }
        }

        public static string Key(int index)
        {
            try
            {
                return (string)Script.Literal("window.localStorage.key({0})", index);
            }
            catch (Exception e) { throw new Exception("IOException:" + e); }
        }

        public static int Length
        {
            get
            {
                try
                {
                    return (int)Script.Literal("window.localStorage.length");
                }
                catch (Exception e) { throw new Exception("IOException:" + e); }
            }
        }

        public static void RemoveItem(string key)
        {
            try
            {
                Script.Literal("window.localStorage.removeItem({0})", key);
            }
            catch (Exception e) { throw new Exception("IOException:" + e); }
        }

        public static void SetItem(string key, string value)
        {
            try
            {
                Script.Literal("window.localStorage.setItem({0}, {1})", key, value);
            }
            catch (Exception e) { throw new Exception("IOException:" + e); }
        }
    }
}
