namespace BusinessObjects.Models;

public static class AppRoles
{
    public const int Staff = 1;
    public const int Lecturer = 2;
    public const string Admin = "Admin";
    public const string StaffName = "Staff";
    public const string LecturerName = "Lecturer";

    public static string ToName(int role) => role switch
    {
        Staff => StaffName,
        Lecturer => LecturerName,
        _ => "Unknown"
    };
}
