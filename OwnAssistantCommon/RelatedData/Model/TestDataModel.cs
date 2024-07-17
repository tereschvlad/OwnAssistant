using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Ardalis.Result;

namespace OwnAssistantCommon.RelatedData.Model
{
    public class TestDataModel : UtilsHashSum
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

    public class ChildFirstTestDataModel : UtilsHashSum
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

    public class SecondChildTestDataModel : UtilsHashSum
    {
        public string Id { get; set; }

        [ReverseHashCustom]
        public string Country { get; set; }

        [ReverseHashCustom]
        public string City { get; set; }

        public int Index { get; set; }
    }

    public class UtilsHashSum
    {
        public string HashSum { get; set; }

        public string GetHashSum()
        {
            string hashDataLine = String.Empty;

            var props = (this).GetType().GetProperties();

            if(props.Any(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ReverseHashCustomAttribute))))
            {
                hashDataLine = props.Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ReverseHashCustomAttribute)))
                              .Select(x => x.GetValue(this).ToString()).Aggregate((x, y) => x + " " + y);
            }

            var textByte = Encoding.Default.GetBytes(hashDataLine);

            var hashByte = MD5.HashData(textByte);

            return Encoding.Default.GetString(hashByte);
        }
    }

    public class ReverseHashCustomAttribute : Attribute
    {

    }
}
