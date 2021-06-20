namespace CarShop.Data
{
    public class DataConstants
    {
        public const int DefaultMinLength = 5;
        public const int DefaultMaxLength = 20;

        public const int PlateNumberMaxLength = 8;

        public const int DescriptionMinLength = 5;

        public const int UserMinUsername = 4;

        public const string UserTypeClient = "Client";
        public const string UserTypeMechanic = "Mechanic";

        public const int CarYearMinValue = 1900;
        public const int CarYearMaxValue = 2100;
        public const string CarPlateNumberRegularExpression = @"[A-Z]{2}[0-9]{4}[A-Z]{2}";
    }
}
