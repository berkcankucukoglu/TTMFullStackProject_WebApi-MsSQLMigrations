internal class Program
{
    private static void Main(string[] args)
    {
    }
}
/*
(!)NOTE = Warning: In order to use the program and migration packs, it is essential that you have a local SQL Server instance installed on your machine. 
Additionally, the migration pack requires the use of PowerShell for its execution. If you do not have a local SQL Server instance or PowerShell installed, 
please make sure to do so before attempting to use the migration pack. Failure to meet these requirements may result in errors or data loss during the migration process.
*/

/*
DB Name: TTM

(!)In Domain Layer
#User Entity
-Id
-FirstName
-LastName
-Email
-Password

#TaskCategory Entity
-Id
-Name
-Description

#Project Entity
-Id
-Name
-Description
-CreatedDate
-LastUpdateDate
-StartDate
-EndDate
-Status
-UserId
-CategoryId

#Task Entity -> Duty&Duties
-Id
-Name
-Description
-CreatedDate
-LastUpdateDate
-StartDate
-EndDate
-Status
-UserId
-Hours
-ProjectId

(!)DataAccess Layer
    -TiynTaskManagerContext (DbContext)
    -Users (DbSet)
    -Projects (DbSet)
    -Tasks (DbSet)
    -Categories (DbSet)
*/