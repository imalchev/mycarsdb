namespace MyCarsDb.Data.Common
{
    public static class DataModelConstants
    {
        public const int USER_EMAIL_MAX_LENGTH = 64;
        public const int USER_NAME_MAX_LENGTH = 64;
        public const int USER_PASSWORD_HASH_MAX_LENGTH = 128;
        public const int USER_PHONE_NUMBER_MAX_LENGTH = 64;
        public const int USER_SECURITY_STAMP_MAX_LENGTH = 128;

        public const int ROLE_NAME_MAX_LENGTH = 32;

        public const int USER_LOGIN_LOGIN_PROVIDER_MAX_LENGTH = 128;
        public const int USER_LOGIN_PROVIDER_KEY_MAX_LENGTH = 128;

        public const int USER_CLAIM_CLAIM_TYPE_MAX_LENGTH = 32;
        public const int USER_CLAIM_CLAIM_VALUE_MAX_LENGTH = 128;

        public const int VEHICLE_MODEL_MODEL_NAME_MAX_LENGTH = 64;

        public const int VEHICLE_MAKE_NAME_MAX_LENGTH = 64;

        public const int VEHICLE_REG_NUMBER_MAX_LENGTH = 15;
        public const int VEHICLE_EXACT_MODEL_MAX_LENGTH = 32;

        public const int FUELING_NOTE_MAX_LENGTH = 512;
    }
}
