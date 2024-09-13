using System.Security.Cryptography;
using System.Text;

namespace OwnAssistantCommon.RelatedData.Model
{
    /// <summary>
    /// General model for related packages data. 
    /// </summary>
    public class GeneralRelatedPackageDataModel
    {
        private string hashSum;
        public string HashSum
        {
            get
            {
                if (String.IsNullOrEmpty(hashSum))
                {
                    hashSum = GetHashSum();
                }

                return hashSum;
            }

        }

        public Guid UniqBlockIndent { get; set; }

        public Guid? PreviousUniqBlockIdent { get; set; }

        public IncrementalDataType IncrementalDataType { get; set; }

        public string GetHashSum()
        {
            string hashDataLine = String.Empty;

            var props = (this).GetType().GetProperties();

            if (props.Any(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ReverseHashCustomAttribute))))
            {
                hashDataLine = props.Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(ReverseHashCustomAttribute)))
                              .Select(x => x.GetValue(this).ToString()).Aggregate((x, y) => x + " " + y);
            }

            var textByte = Encoding.Default.GetBytes(hashDataLine);

            var hashByte = MD5.HashData(textByte);

            return Encoding.Default.GetString(hashByte);
        }
    }

    /// <summary>
    /// Type of related data
    /// </summary>
    public enum IncrementalDataType
    {
        None = 0,
        New = 1,
        Modified = 2,
        Removed = 3,
        Repeated = 4
    }

    public class ReverseHashCustomAttribute : Attribute
    {

    }
}
