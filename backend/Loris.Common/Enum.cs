using Loris.Common.Domain;
using System;

namespace Loris.Common
{
    public enum TreatedResultStatus : byte
    {
        Undefined = 0,
        Success = 1,
        Error = 2,
        NotValidate = 3,
        GoTo = 4,
        NoDataFound = 5,
        SuccessWarning = 6,
        CriticalError = 7,
        InternalServerError = 8,
        TimeoutError = 9,
        NotAuthorized = 10,
        BusinessError = 11,
    }

    public enum Languages : byte
    {
        Undefined = 0,
        Portuguese = 1,
        English = 2,
        Spanish = 3
    }

    public enum TimeSpanEnum : byte
    {
        [EnumInfo(dictionary: "lbl_undefined")]
        Undefined = 0,
        [EnumInfo(dictionary: "lbl_days")]
        Days = 1,
        [EnumInfo(dictionary: "lbl_hours")]
        Hours = 2,
        [EnumInfo(dictionary: "lbl_minutes")]
        Minutes = 3,
        [EnumInfo(dictionary: "lbl_secounds")]
        Seconds = 4
    }

    public enum YesNoEnum : byte
    {
        [EnumInfo(dictionary: "lbl_undefined")]
        Undefined = 0,
        [EnumInfo(dictionary: "lbl_no")]
        No = 1,
        [EnumInfo(dictionary: "lbl_yes")]
        Yes = 2
    }

    /// <summary>
    /// Tipo de severidade
    /// </summary>
    public enum SeverityTypeEnum : byte
    {
        [EnumInfo(dictionary: "lbl_undefined")]
        Undefined = 0,
        [EnumInfo(dictionary: "lbl_severe")]
        Severe = 1,
        [EnumInfo(dictionary: "lbl_high")]
        High = 2,
        [EnumInfo(dictionary: "lbl_average")]
        Average = 3,
        [EnumInfo(dictionary: "lbl_low")]
        Low = 4,
        [EnumInfo(dictionary: "lbl_none")]
        None = 5,
    }

    public enum LoginType : byte
    {
        [EnumInfo(dictionary: "lbl_user")]
        User = 0,
        [EnumInfo(dictionary: "lbl_system")]
        System = 1
    }

    public enum LoginStatus : byte
    {
        Undefined = 0,
        NotFound = 1,
        NotLogged = 2,
        Logged = 3,
        Blocked = 4,
        Disabled = 5,
        NotAuthorized = 6,
        ExpiredPassword = 7,
        InvalidPassword = 8,
        ResetPassword = 9,
    }

    public enum ChangePwdStatus : byte
    {
        Undefined = 0,
        InvalidUser = 1,
        BlockedUser = 2,
        InvalidOldPassword = 3,
        InvalidNewPassword = 4,
        InvalidNewPasswordEqualOld = 5,
        InvalidToken = 6,
        GeneratedKey = 7,
        PasswordChanged = 8,
    }

    public enum DataErrorTypeEnum : short
    {
        Undefined = 0,
        IntegrityConstraintViolation,
        RestrictViolation,
        NotNullViolation,
        ForeignKeyViolation,
        UniqueViolation,
        CheckViolation,
        ExclusionViolation
    }

    /// <summary>
    /// Este enumerador funciona como acumulador dentro do campo referente ao tipo
    /// <example>
    ///     var obj = new AccessPermission() { AccessPermission = AccessPermission.Create | AccessPermission.Read
    ///     e para verificar se algum compete:
    /// return (obj.AccessPermission & AccessPermission.Create) == AccessPermission.Create)? "Permite criação" : "NÃO permite criação"
    /// Enums do tipo FlagsAttribute DEVEM ser atribudos como potencia de 2
    /// https://msdn.microsoft.com/pt-br/library/ms182335.aspx
    /// </example>
    /// </summary>
    [Flags]
    public enum AccessPermission : byte
    {
        [EnumInfo(dictionary: "lbl_none")]
        None = 0,
        [EnumInfo(dictionary: "lbl_create")]
        Create = 1,     // 2^0
        [EnumInfo(dictionary: "lbl_read")]
        Read = 2,       // 2^1
        [EnumInfo(dictionary: "lbl_update")]
        Update = 4,     // 2^2
        [EnumInfo(dictionary: "lbl_delete")]
        Delete = 8,     // 2^3
        [EnumInfo(dictionary: "lbl_report")]
        Report = 16,    // 2^4
        [EnumInfo(dictionary: "lbl_all")]
        All = Create | Read | Update | Delete | Report
    }

    public enum ValidationsExceptionType : byte
    {
        Undefined = 0,
        InvalidParameter = 1,
        IdMustBeProvided = 2,
        ViolationUniqueField = 3,
        FailedToExecuteOperation = 4,
    }

    public enum InternalSystem : byte
    {
        Undefined = 0,
        PlatformBase = 1,
        IotSystem = 2,
    }

    public enum CourierAction : byte
    {
        Undefined = 0,
        ToSend = 1,
        Received = 2,
    }

    public enum CourierStatus : byte
    {
        ToProcess = 0,
        ProcessError = 1,
        Finalized = 2,
    }

    public enum CourierSystem : byte
    {
        Undefined = 0,
        Email = 1,
        SMS = 2,
        WhatsApp = 3,
        Telegram = 4,
        FbMessage = 5,
    }

    public enum FileType : byte
    {
        Undefined = 0,
        Image = 1,
        Video = 2,
        Document = 3,
    }
}
