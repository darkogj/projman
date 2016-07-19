namespace ProjectManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6dbc6a62-0873-4b32-98ed-04768e8bb8b0', N'Admin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'bd9dcdfd-f103-4cd1-a2a9-d198e94cf28d', N'User')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DisplayName], [IsActive], [DateCreated]) VALUES (N'2acbab31-91a7-4bb7-9bb3-aed6a8aedc30', N'hajan@haselt.com', 0, N'AJJuGleWvJJaRxMZiQJCZp+jCDBLduvKtxcwD5Uxz57PjMukr4zdobhRvcI40kRvVQ==', N'3f7b32b7-2893-44a4-8831-b65b22cf1f1c', NULL, 0, 0, NULL, 1, 0, N'hajan@haselt.com', N'Hajan Selmani', 'True', '2010-01-01T00:00:00')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [DisplayName], [IsActive], [DateCreated]) VALUES (N'50cc3df0-0311-4afe-a77e-4159f7949ffb', N'darko.twist@gmail.com', 0, N'AF1lQRDaN4bic+QknQwP8TU03pVsULiHsv20mdtuwNR68PHzxVu5eo7bR/rX66w+Fw==', N'61817737-c0aa-4842-884e-832ce21fd506', NULL, 0, 0, NULL, 1, 0, N'darko.twist@gmail.com', N'Darko Marko', 'True', '2010-01-01T00:00:00')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2acbab31-91a7-4bb7-9bb3-aed6a8aedc30', N'6dbc6a62-0873-4b32-98ed-04768e8bb8b0')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'50cc3df0-0311-4afe-a77e-4159f7949ffb', N'bd9dcdfd-f103-4cd1-a2a9-d198e94cf28d')

");
        }

        public override void Down()
        {
        }
    }
}

