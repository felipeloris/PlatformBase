namespace Loris.Common.Extensions
{
    public static class AccessPermissionExtension
    {
        public static bool CanCreate(this AccessPermission permission) 
        {
           return (permission & AccessPermission.Create) == AccessPermission.Create;
        }

        public static bool CanRead(this AccessPermission permission)
        {
            return (permission & AccessPermission.Read) == AccessPermission.Read;
        }

        public static bool CanUpdate(this AccessPermission permission)
        {
            return (permission & AccessPermission.Update) == AccessPermission.Update;
        }

        public static bool CanDelete(this AccessPermission permission)
        {
            return (permission & AccessPermission.Delete) == AccessPermission.Delete;
        }

        public static bool CanReport(this AccessPermission permission)
        {
            return (permission & AccessPermission.Report) == AccessPermission.Report;
        }

        public static bool CanAll(this AccessPermission permission)
        {
            return (permission & AccessPermission.All) == AccessPermission.All;
        }
    }
}
