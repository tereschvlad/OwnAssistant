using Microsoft.EntityFrameworkCore;
using OwnAssistantCommon.Models;

namespace OwnAssistant.Utils
{
    //TODO: Change to await method
    public class DbDataFilling
    {
        public static void FillData(IApplicationBuilder appBuilder)
        {
            var context = appBuilder.ApplicationServices.CreateScope()
                                    .ServiceProvider.GetService<DataContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(new List<RoleModel>()
                {
                    new RoleModel()
                    {
                        Id = new Guid("8a047c7c-573a-4393-9fc1-cee297b7dbb1"),
                        Name = "User"
                    },
                    new RoleModel()
                    {
                        Id = new Guid("ea303818-50de-49a5-a899-d0d9829f20a2"),
                        Name = "Admin"
                    }
                });

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(new List<UserModel>()
                {
                    new UserModel()
                    {
                        Id = new Guid("dda5b335-580f-414c-b8a2-80f3523950e6"),
                        Login = "john_doe",
                        Password = "securePassword123", // Note: In practice, use secure password hashing
                        Email = "john.doe@example.com",
                        CrtDate = DateTime.Now,
                        RoleId = new Guid("ea303818-50de-49a5-a899-d0d9829f20a2")
                    },
                    new UserModel()
                    {
                        Id = new Guid("52fefe38-b35a-4540-a85d-56294ac86fc0"),
                        Login = "alice_smith",
                        Password = "strongPass456",
                        Email = "alice.smith@example.com",
                        CrtDate = DateTime.Now,
                        RoleId = new Guid("ea303818-50de-49a5-a899-d0d9829f20a2"),
                    },
                    new UserModel()
                    {
                        Id = new Guid("dd1afab8-f852-435a-9653-6546559f8c39"),
                        Login = "bob_jones",
                        Password = "safeAndSound789",
                        Email = "bob.jones@example.com",
                        CrtDate = DateTime.Now,
                        RoleId = new Guid("8a047c7c-573a-4393-9fc1-cee297b7dbb1")
                    },
                    new UserModel()
                    {
                        Id = new Guid("18d4f302-9131-4e8e-a6d7-d2d72c5a4000"),
                        Login = "emma_watson",
                        Password = "password1234",
                        Email = "emma.watson@example.com",
                        CrtDate = DateTime.Now,
                        RoleId = new Guid("8a047c7c-573a-4393-9fc1-cee297b7dbb1")
                    }
                });

                context.SaveChanges();
            }

            if(!context.Tasks.Any())
            {

                

                var user1 = context.Users.FirstOrDefault(x => x.Id == new Guid("dda5b335-580f-414c-b8a2-80f3523950e6"));
                var user2 = context.Users.FirstOrDefault(x => x.Id == new Guid("52fefe38-b35a-4540-a85d-56294ac86fc0"));
                var user3 = context.Users.FirstOrDefault(x => x.Id == new Guid("dd1afab8-f852-435a-9653-6546559f8c39"));
                var user4 = context.Users.FirstOrDefault(x => x.Id == new Guid("18d4f302-9131-4e8e-a6d7-d2d72c5a4000"));

                context.Tasks.AddRange(new List<CustomerTaskModel>()
                {
                    new CustomerTaskModel() 
                    {
                        Id = Guid.NewGuid(),
                        Title = "Task 1",
                        Text = "Description for Task 1",
                        CrtDate = DateTime.Now,
                        TaskDate = DateTime.Now.AddDays(2),
                        CloseDate = null,
                        CreatorUser = user1,
                        PerformingUsers = new List<UserModel>
                        {
                             user2, 
                        }
                    },
                    new CustomerTaskModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Task 2",
                        Text = "Description for Task 2",
                        CrtDate = DateTime.Now,
                        TaskDate = DateTime.Now.AddDays(5),
                        CloseDate = null,
                        CreatorUser = user2,
                        PerformingUsers = new List<UserModel>
                        {
                           user1,
                           user4,
                        }
                    },
                    new CustomerTaskModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Task 3",
                        Text = "Description for Task 3",
                        CrtDate = DateTime.Now,
                        TaskDate = DateTime.Now.AddDays(3),
                        CloseDate = null,
                        CreatorUser = user3,
                        PerformingUsers = new List<UserModel>
                        {
                            user2,
                            user4,
                        }
                    },
                    new CustomerTaskModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Task 4",
                        Text = "Description for Task 4",
                        CrtDate = DateTime.Now,
                        TaskDate = DateTime.Now.AddDays(1),
                        CloseDate = null,
                        CreatorUser = user4,
                        PerformingUsers = new List<UserModel>
                        {
                            user1,
                            user3,
                        }
                    },
                    new CustomerTaskModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Task 5",
                        Text = "Description for Task 5",
                        CrtDate = DateTime.Now,
                        TaskDate = DateTime.Now.AddDays(4),
                        CloseDate = null,
                        CreatorUser = user1,
                        PerformingUsers = new List<UserModel>
                        {
                            user2,
                            user3,
                            user4,
                        }
                    },
                    new CustomerTaskModel()
                    {
                        Id = Guid.NewGuid(),
                        Title = "Task 6",
                        Text = "Description for Task 6",
                        CrtDate = DateTime.Now,
                        TaskDate = DateTime.Now.AddDays(7),
                        CloseDate = null,
                        CreatorUser = user2,
                        PerformingUsers = new List<UserModel>
                        {
                            user1,
                            user3,
                            user4,
                        }
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
