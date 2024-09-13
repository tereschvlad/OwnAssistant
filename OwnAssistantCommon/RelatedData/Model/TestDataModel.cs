using System.Text;
using System.Security.Cryptography;

namespace OwnAssistantCommon.RelatedData.Model
{
    public class TestDataModel : GeneralRelatedPackageDataModel
    {
        public string Id { get; set; }

        [ReverseHashCustom]
        public string Name { get; set; }

        public string Email { get; set; }

        [ReverseHashCustom]
        public string Phone { get; set; }

        public List<ChildFirstTestDataModel> FirstChildList { get; set; }

        public List<SecondChildTestDataModel> SecondChildList { get; set; }

    }

    public class ChildFirstTestDataModel : GeneralRelatedPackageDataModel
    {
        public string Id { get; set; }

        [ReverseHashCustom]
        public string CompanyName { get; set; }

        [ReverseHashCustomAttribute]
        public string WorkPosition { get; set; }

        public DateTime? StartWork { get; set; }

        public DateTime? EndWork { get; set; }

        public double? MiddleSalary { get; set; }
    }

    public class SecondChildTestDataModel : GeneralRelatedPackageDataModel
    {
        public string Id { get; set; }

        [ReverseHashCustom]
        public string Country { get; set; }

        [ReverseHashCustom]
        public string City { get; set; }

        public int Index { get; set; }
    }
}
