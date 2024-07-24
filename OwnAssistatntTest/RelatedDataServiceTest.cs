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

        [Fact]
        public void Comparison_HashSum1()
        {

            #region Initialisation

            #region Creating instances of ChildFirstTestDataModel

            var firstChild1 = new ChildFirstTestDataModel
            {
                Id = "1",
                CompanyName = "Company A",
                WorkPosition = "Manager",
                StartWork = new DateTime(2010, 1, 1),
                EndWork = new DateTime(2015, 1, 1),
                MiddleSalary = 50000,
                UniqBlockIndent = Guid.NewGuid(),
            };

            var firstChild2 = new ChildFirstTestDataModel
            {
                Id = "2",
                CompanyName = "Company B",
                WorkPosition = "Engineer",
                StartWork = new DateTime(2011, 1, 1),
                EndWork = new DateTime(2016, 1, 1),
                MiddleSalary = 60000,
                UniqBlockIndent = Guid.NewGuid(),
            };

            var firstChild3 = new ChildFirstTestDataModel
            {
                Id = "3",
                CompanyName = "Company C",
                WorkPosition = "Analyst",
                StartWork = new DateTime(2012, 1, 1),
                EndWork = new DateTime(2017, 1, 1),
                MiddleSalary = 55000,
                UniqBlockIndent = Guid.NewGuid(),
            };

            var firstChild4 = new ChildFirstTestDataModel
            {
                Id = "4",
                CompanyName = "Company D",
                WorkPosition = "Developer",
                StartWork = new DateTime(2013, 1, 1),
                EndWork = new DateTime(2018, 1, 1),
                MiddleSalary = 65000,
                UniqBlockIndent = Guid.NewGuid(),
            };

            var firstChild5 = new ChildFirstTestDataModel
            {
                Id = "5",
                CompanyName = "Company E",
                WorkPosition = "Consultant",
                StartWork = new DateTime(2014, 1, 1),
                EndWork = new DateTime(2019, 1, 1),
                MiddleSalary = 70000,
                UniqBlockIndent = Guid.NewGuid(),
            };

            #endregion

            #region Creating instances of SecondChildTestDataModel

            var secondChild1 = new SecondChildTestDataModel
            {
                Id = "1",
                Country = "USA",
                City = "New York",
                Index = 10001
            };

            var secondChild2 = new SecondChildTestDataModel
            {
                Id = "2",
                Country = "UK",
                City = "London",
                Index = 20001
            };

            var secondChild3 = new SecondChildTestDataModel
            {
                Id = "3",
                Country = "Canada",
                City = "Toronto",
                Index = 30001
            };

            var secondChild4 = new SecondChildTestDataModel
            {
                Id = "4",
                Country = "Australia",
                City = "Sydney",
                Index = 40001
            };

            var secondChild5 = new SecondChildTestDataModel
            {
                Id = "5",
                Country = "Germany",
                City = "Berlin",
                Index = 50001
            };

            #endregion


            #region Creating instances of TestDataModel for the first list

            var testDataModel1 = new TestDataModel
            {
                Id = "1",
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123-456-7890",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild1 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild1 },
                UniqBlockIndent = Guid.NewGuid(),
            };

            var testDataModel2 = new TestDataModel
            {
                Id = "2",
                Name = "Jane Smith",
                Email = "jane.smith@example.com",
                Phone = "098-765-4321",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild2 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild2 },
                UniqBlockIndent = Guid.NewGuid(),
            };

            var testDataModel3 = new TestDataModel
            {
                Id = "3",
                Name = "Alice Johnson",
                Email = "alice.johnson@example.com",
                Phone = "234-567-8901",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild3 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild3 },
                UniqBlockIndent = Guid.NewGuid(),
            };

            var testDataModel4 = new TestDataModel
            {
                Id = "4",
                Name = "Bob Brown",
                Email = "bob.brown@example.com",
                Phone = "345-678-9012",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild4 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild4 },
                UniqBlockIndent = Guid.NewGuid(),
            };

            var testDataModel5 = new TestDataModel
            {
                Id = "5",
                Name = "Charlie White",
                Email = "charlie.white@example.com",
                Phone = "456-789-0123",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild5 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild5 },
                UniqBlockIndent = Guid.NewGuid(),
            };

            #endregion

            #region Creating instances of TestDataModel for the second list

            var testDataModel6 = new TestDataModel
            {
                Id = "6",
                Name = "Bob Brown",
                Email = "bob.brown@example.com",
                Phone = "345-678-9012",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild4 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild4 },
            };

            var testDataModel7 = new TestDataModel
            {
                Id = "7",
                Name = "Eva Brown",
                Email = "eva.brown@example.com",
                Phone = "678-901-2345",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild3, firstChild4 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild3, secondChild4 }
            };

            var testDataModel8 = new TestDataModel
            {
                Id = "8",
                Name = "Alice Johnson",
                Email = "alice.johnson@example.com",
                Phone = "234-567-8901",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild3 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild3 },
            };

            var testDataModel9 = new TestDataModel
            {
                Id = "9",
                Name = "Grace White",
                Email = "grace.white@example.com",
                Phone = "890-123-4567",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild4, firstChild5 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild4, secondChild5 }
            };

            var testDataModel10 = new TestDataModel
            {
                Id = "10",
                Name = "Hannah Grey",
                Email = "hannah.grey@example.com",
                Phone = "901-234-5678",
                FirstChildList = new List<ChildFirstTestDataModel> { firstChild1, firstChild3 },
                SecondChildList = new List<SecondChildTestDataModel> { secondChild1, secondChild3 }
            };

            #endregion

            #endregion

            // Creating the first list of TestDataModel instances
            var testDataList1 = new List<TestDataModel> { testDataModel1, testDataModel2, testDataModel3, testDataModel4, testDataModel5 };

            // Creating the second list of TestDataModel instances
            var testDataList2 = new List<TestDataModel> { testDataModel6, testDataModel7, testDataModel8, testDataModel9, testDataModel10 };

        }
    }
}
