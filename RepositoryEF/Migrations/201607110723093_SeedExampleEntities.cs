namespace RepositoryEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedExampleEntities : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[Customers] ([Name], [Company], [Email], [IsActive], [DateCreated]) VALUES (N'George Brown', N'Microsoft', N'gbrown@microsoft.com', 1, N'2016-07-5 05:06:25')
INSERT INTO [dbo].[Customers] ([Name], [Company], [Email], [IsActive], [DateCreated]) VALUES (N'Dennis Ritchie', N'Google', N'd.ritchie@google.com', 1, N'2016-07-5 05:06:43')
INSERT INTO [dbo].[Projects] ([Name], [CustomerId], [IsActive], [DateCreated]) VALUES (N'Asp.Net Core 12', 1, 1, N'2016-07-5 05:07:13')
INSERT INTO [dbo].[Projects] ([Name], [CustomerId], [IsActive], [DateCreated]) VALUES (N'Angular 2', 2, 1, N'2016-07-5 05:13:13')
INSERT INTO [dbo].[Tasks] ([Name], [Description], [EstimatedHours], [StartDateTime], [EndDateTime], [Type], [Status], [ProjectId], [UserId]) VALUES (N'Create XML helpers', N'HTML helpers', 10, N'2016-05-15 00:00:00', N'2016-09-07 00:00:00', 0, 0, 1, N'50cc3df0-0311-4afe-a77e-4159f7949ffb')
INSERT INTO [dbo].[Tasks] ( [Name], [Description], [EstimatedHours], [StartDateTime], [EndDateTime], [Type], [Status], [ProjectId], [UserId]) VALUES (N'Fix connection string issue', N'Connection string issue in Web.Config causing the app to crash', 43, N'2016-07-05 00:00:00', N'2016-10-07 00:00:00', 1, 0, 1, N'2acbab31-91a7-4bb7-9bb3-aed6a8aedc30')
INSERT INTO [dbo].[Tasks] ([Name], [Description], [EstimatedHours], [StartDateTime], [EndDateTime], [Type], [Status], [ProjectId], [UserId]) VALUES (N'Fix databinding issue', N'Databinding issue regarding angular 2', 43, N'2016-04-05 00:00:00', N'2016-07-06 00:00:00', 1, 0, 2, N'2acbab31-91a7-4bb7-9bb3-aed6a8aedc30')
INSERT INTO [dbo].[Tasks] ([Name], [Description], [EstimatedHours], [StartDateTime], [EndDateTime], [Type], [Status], [ProjectId], [UserId]) VALUES (N'Add 3-way data binding', N'3 way data binding', 33, N'2016-07-03 00:00:00', N'2017-01-19 00:00:00', 2, 1, 2, N'50cc3df0-0311-4afe-a77e-4159f7949ffb')
INSERT INTO [dbo].[Tasks] ([Name], [Description], [EstimatedHours], [StartDateTime], [EndDateTime], [Type], [Status], [ProjectId], [UserId]) VALUES (N'Clarify the term ""Core""', N'Some people don''t understand it', 10, N'2016-07-04 00:00:00', N'2016-07-08 00:00:00', 3, 0, 1, N'2acbab31-91a7-4bb7-9bb3-aed6a8aedc30')
INSERT INTO [dbo].[TaskComments] ([Comment], [UserId], [TaskId]) VALUES (N'Good job so far!', N'2acbab31-91a7-4bb7-9bb3-aed6a8aedc30', 1)
INSERT INTO [dbo].[TaskComments] ([Comment], [UserId], [TaskId]) VALUES (N'Thanks man!', N'2acbab31-91a7-4bb7-9bb3-aed6a8aedc30', 1)


");
        }
        
        public override void Down()
        {
        }
    }
}
