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
                context.Roles.AddRange(new List<RoleDbModel>()
                {
                    new RoleDbModel()
                    {
                        Id = new Guid("8a047c7c-573a-4393-9fc1-cee297b7dbb1"),
                        Name = "User"
                    },
                    new RoleDbModel()
                    {
                        Id = new Guid("ea303818-50de-49a5-a899-d0d9829f20a2"),
                        Name = "Admin"
                    }
                });

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(new List<UserDbModel>()
                {
                    new UserDbModel()
                    {
                        Id = new Guid("dda5b335-580f-414c-b8a2-80f3523950e6"),
                        Login = "john_doe",
                        Password = "securePassword123", // Note: In practice, use secure password hashing
                        Email = "john.doe@example.com",
                        CrtDate = DateTime.Now,
                        RoleId = new Guid("ea303818-50de-49a5-a899-d0d9829f20a2")
                    },
                    new UserDbModel()
                    {
                        Id = new Guid("52fefe38-b35a-4540-a85d-56294ac86fc0"),
                        Login = "alice_smith",
                        Password = "strongPass456",
                        Email = "alice.smith@example.com",
                        CrtDate = DateTime.Now,
                        RoleId = new Guid("ea303818-50de-49a5-a899-d0d9829f20a2"),
                    },
                    new UserDbModel()
                    {
                        Id = new Guid("dd1afab8-f852-435a-9653-6546559f8c39"),
                        Login = "bob_jones",
                        Password = "safeAndSound789",
                        Email = "bob.jones@example.com",
                        CrtDate = DateTime.Now,
                        RoleId = new Guid("8a047c7c-573a-4393-9fc1-cee297b7dbb1")
                    },
                    new UserDbModel()
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

            if (!context.MainInfoTasks.Any())
            {



                var user1 = context.Users.FirstOrDefault(x => x.Id == new Guid("dda5b335-580f-414c-b8a2-80f3523950e6"));
                var user2 = context.Users.FirstOrDefault(x => x.Id == new Guid("52fefe38-b35a-4540-a85d-56294ac86fc0"));
                var user3 = context.Users.FirstOrDefault(x => x.Id == new Guid("dd1afab8-f852-435a-9653-6546559f8c39"));
                var user4 = context.Users.FirstOrDefault(x => x.Id == new Guid("18d4f302-9131-4e8e-a6d7-d2d72c5a4000"));

                var main1 = Guid.NewGuid();
                var main2 = Guid.NewGuid();
                var main3 = Guid.NewGuid();
                var main4 = Guid.NewGuid();
                var main5 = Guid.NewGuid();
                var main6 = Guid.NewGuid();

                context.MainInfoTasks.AddRange(new List<CustomerTaskMainInfoDbModel>()
                {
                    new CustomerTaskMainInfoDbModel()
                    {
                        Id = main1,
                        Title = "Task 1",
                        Text = "Description for Task 1",
                        CrtDate = DateTime.Now,
                        CreatorUser = user1,
                        PerformingUser = user2,
                        CustomerTaskDateInfos = new List<CustomerTaskDateInfoDbModel>()
                        {
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main1,
                                TaskDate = DateTime.Now.AddDays(1)
                            },
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main1,
                                TaskDate = DateTime.Now.AddDays(2)
                            }
                        },
                        CustomerTaskCheckpointInfos = new List<CustomerTaskCheckpointInfoDbModel>()
                        {
                            new CustomerTaskCheckpointInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main1,
                                Lat = 2,
                                Long = 3
                            }
                        }
                    },
                    new CustomerTaskMainInfoDbModel()
                    {
                        Id = main2,
                        Title = "Task 2",
                        Text = "Description for Task 2",
                        CrtDate = DateTime.Now,
                        CreatorUser = user2,
                        PerformingUser = user4,
                        CustomerTaskDateInfos = new List<CustomerTaskDateInfoDbModel>()
                        {
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main2,
                                TaskDate = DateTime.Now.AddDays(1)
                            }
                        },
                        CustomerTaskCheckpointInfos = new List<CustomerTaskCheckpointInfoDbModel>()
                        {
                            new CustomerTaskCheckpointInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main2,
                                Lat = 2,
                                Long = 3
                            }
                        }
                    },
                    new CustomerTaskMainInfoDbModel()
                    {
                        Id = main3,
                        Title = "Task 3",
                        Text = "Description for Task 3",
                        CrtDate = DateTime.Now,
                        CreatorUser = user3,
                        PerformingUser = user4,
                        CustomerTaskDateInfos = new List<CustomerTaskDateInfoDbModel>()
                        {
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main3,
                                TaskDate = DateTime.Now.AddDays(1)
                            },
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main3,
                                TaskDate = DateTime.Now.AddDays(3)
                            },
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main3,
                                TaskDate = DateTime.Now.AddDays(5)
                            }
                        },
                        CustomerTaskCheckpointInfos = new List<CustomerTaskCheckpointInfoDbModel>()
                        {
                            new CustomerTaskCheckpointInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main3,
                                Lat = 2,
                                Long = 3
                            }
                        }
                    },
                    new CustomerTaskMainInfoDbModel()
                    {
                        Id = main4,
                        Title = "Task 4",
                        Text = "Description for Task 4",
                        CrtDate = DateTime.Now,
                        CreatorUser = user4,
                        PerformingUser = user3,
                        CustomerTaskDateInfos = new List<CustomerTaskDateInfoDbModel>()
                        {
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main4,
                                TaskDate = DateTime.Now.AddDays(3)
                            }
                        },
                        CustomerTaskCheckpointInfos = new List<CustomerTaskCheckpointInfoDbModel>()
                        {
                            new CustomerTaskCheckpointInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main4,
                                Lat = 2,
                                Long = 3
                            }
                        }
                    },
                    new CustomerTaskMainInfoDbModel()
                    {
                        Id = main5,
                        Title = "Task 5",
                        Text = "Description for Task 5",
                        CrtDate = DateTime.Now,
                        CreatorUser = user1,
                        PerformingUser = user2,
                        CustomerTaskDateInfos = new List<CustomerTaskDateInfoDbModel>()
                        {
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main5,
                                TaskDate = DateTime.Now.AddDays(10)
                            }
                        },
                        CustomerTaskCheckpointInfos = new List<CustomerTaskCheckpointInfoDbModel>()
                        {
                            new CustomerTaskCheckpointInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main5,
                                Lat = 2,
                                Long = 3
                            }
                        }
                    },
                    new CustomerTaskMainInfoDbModel()
                    {
                        Id = main6,
                        Title = "Task 6",
                        Text = "Description for Task 6",
                        CrtDate = DateTime.Now,
                        CreatorUser = user2,
                        PerformingUser = user1,
                        CustomerTaskDateInfos = new List<CustomerTaskDateInfoDbModel>()
                        {
                            new CustomerTaskDateInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main6,
                                TaskDate = DateTime.Now.AddDays(5)
                            }
                        },
                        CustomerTaskCheckpointInfos = new List<CustomerTaskCheckpointInfoDbModel>()
                        {
                            new CustomerTaskCheckpointInfoDbModel()
                            {
                                Id = Guid.NewGuid(),
                                CustomerTaskMainId = main6,
                                Lat = 2,
                                Long = 3
                            }
                        }
                    }
                });

                context.SaveChanges();
            }
        }
    }
}
