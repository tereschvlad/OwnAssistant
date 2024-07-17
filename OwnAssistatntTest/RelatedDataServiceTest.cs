using OwnAssistantCommon.RelatedData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OwnAssistatntTest
{
    public class RelatedDataServiceTest
    {
        [Fact]
        public void Check_HashSum1()
        {
            var testDataModel1 = new TestDataModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Alice Johnson",
                Email = "alice.johnson@example.com",
                Phone = "123-456-7890",
                HashSum = "hashsum1",
                FirstChildList = new List<ChildFirstTestDataModel>
            {
                new ChildFirstTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "TechCorp",
                    WorkPosition = "Developer",
                    StartWork = new DateTime(2018, 1, 15),
                    EndWork = new DateTime(2021, 5, 30),
                    MiddleSalary = 70000
                },
                new ChildFirstTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "Innovatech",
                    WorkPosition = "Senior Developer",
                    StartWork = new DateTime(2021, 6, 1),
                    EndWork = null, // Currently working
                    MiddleSalary = 85000
                }
            },
                SecondChildList = new List<SecondChildTestDataModel>
            {
                new SecondChildTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = "United Kingdom",
                    City = "London",
                    Index = 101
                },
                new SecondChildTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = "United Kingdom",
                    City = "Manchester",
                    Index = 102
                }
            }
            };
            var testDataModel1_1 = new TestDataModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Alice Johnson",
                Email = "alice.johnson@example.com",
                Phone = "123-456-7890",
                HashSum = "hashsum1",
                FirstChildList = new List<ChildFirstTestDataModel>
            {
                new ChildFirstTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "TechCorp",
                    WorkPosition = "Developer",
                    StartWork = new DateTime(2018, 1, 15),
                    EndWork = new DateTime(2021, 5, 30),
                    MiddleSalary = 70000
                },
                new ChildFirstTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "Innovatech",
                    WorkPosition = "Senior Developer",
                    StartWork = new DateTime(2021, 6, 1),
                    EndWork = null, // Currently working
                    MiddleSalary = 85000
                }
            },
                SecondChildList = new List<SecondChildTestDataModel>
            {
                new SecondChildTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = "United Kingdom",
                    City = "London",
                    Index = 101
                },
                new SecondChildTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = "United Kingdom",
                    City = "Manchester",
                    Index = 102
                }
            }
            };

            var testDataModel2 = new TestDataModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Bob Smith",
                Email = "bob.smith@example.com",
                Phone = "098-765-4321",
                HashSum = "hashsum2",
                FirstChildList = new List<ChildFirstTestDataModel>
            {
                new ChildFirstTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "HealthInc",
                    WorkPosition = "Data Analyst",
                    StartWork = new DateTime(2019, 3, 10),
                    EndWork = new DateTime(2022, 8, 15),
                    MiddleSalary = 60000
                },
                new ChildFirstTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "MedTech",
                    WorkPosition = "Senior Data Analyst",
                    StartWork = new DateTime(2022, 9, 1),
                    EndWork = null, // Currently working
                    MiddleSalary = 75000
                }
            },
                SecondChildList = new List<SecondChildTestDataModel>
            {
                new SecondChildTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = "United Kingdom",
                    City = "Birmingham",
                    Index = 201
                },
                new SecondChildTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = "United Kingdom",
                    City = "Liverpool",
                    Index = 202
                }
            }
            };
            var testDataModel2_2 = new TestDataModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Bob Smith",
                Email = "bob.smith@example.com",
                Phone = "098-765-4321",
                HashSum = "hashsum2",
                FirstChildList = new List<ChildFirstTestDataModel>
            {
                new ChildFirstTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "HealthInc",
                    WorkPosition = "Data Analyst",
                    StartWork = new DateTime(2019, 3, 10),
                    EndWork = new DateTime(2022, 8, 15),
                    MiddleSalary = 60000
                },
                new ChildFirstTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    CompanyName = "MedTech",
                    WorkPosition = "Senior Data Analyst",
                    StartWork = new DateTime(2022, 9, 1),
                    EndWork = null, // Currently working
                    MiddleSalary = 75000
                }
            },
                SecondChildList = new List<SecondChildTestDataModel>
            {
                new SecondChildTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = "United Kingdom",
                    City = "Birmingham",
                    Index = 201
                },
                new SecondChildTestDataModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = "United Kingdom",
                    City = "Liverpool",
                    Index = 202
                }
            }
            };

            var t1 = testDataModel1.GetHashSum();
            var t1_1 = testDataModel1_1.GetHashSum();

            var t2 = testDataModel2.GetHashSum();
            var t2_2 = testDataModel2.GetHashSum();

            Assert.Equal(t1, t1_1);
            Assert.Equal(t2, t2_2);
        }
    }
}
