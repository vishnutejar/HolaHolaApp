namespace HolaHolaApp.models
{
    public class FCMBody
    {
        public string to { get; set; }
        public string priority { get; set; } = "high";

        public FCMNotification notification { get; set; }

        public FCMData data { get; set; }

    }

    public class FCMNotification

    {

        public string body { get; set; }

        public string title { get; set; }

    }

    public class FCMData
    {

        public string key1 { get; set; }

        public string key2 { get; set; }

        public string key3 { get; set; }

        public string key4 { get; set; }

    }

}