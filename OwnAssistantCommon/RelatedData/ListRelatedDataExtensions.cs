using Microsoft.CodeAnalysis.CSharp.Syntax;
using OwnAssistantCommon.RelatedData.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnAssistantCommon.RelatedData
{
    public static class ListRelatedDataExtensions
    {
        public static void DefineBlockIndentRelatedData<T>(this IEnumerable<T> currentData, IEnumerable<T> previousData, Func<T, bool> comparison) where T : IncrementalDataUtilsModel 
        {
            if (currentData.Any())
            {
                foreach (var item in currentData)
                {
                    var prev = previousData.FirstOrDefault(comparison);

                    if(prev != null)
                    {
                        item.PreviousUniqBlockIdent = prev.UniqBlockIndent;

                        if (item.HashSum == prev.HashSum)
                        {
                            item.UniqBlockIndent = prev.UniqBlockIndent;
                            item.IncrementalDataType = IncrementalDataType.Repeated;
                        }
                        else
                        {
                            item.UniqBlockIndent = Guid.NewGuid();
                            item.IncrementalDataType = IncrementalDataType.Modified;
                        }   
                    }
                    else
                    {
                        item.UniqBlockIndent = Guid.NewGuid();
                        item.IncrementalDataType = IncrementalDataType.New;
                    }
                }
            }
        }

        public static IEnumerable<T> GetRemovedRelatedData<T>(this IEnumerable<T> currentData, IEnumerable<T> previousData, IEqualityComparer<T> equality) where T : IncrementalDataUtilsModel
        {
            var removed = previousData.Except(currentData, equality);

            foreach(var item in removed)
            {
                item.PreviousUniqBlockIdent = item.UniqBlockIndent;
                item.UniqBlockIndent = Guid.NewGuid();
                item.IncrementalDataType = IncrementalDataType.Removed;
            }

            return removed;
        }
    }

    public class EqualityComparerAbsentRelatedData : IEqualityComparer<TestDataModel>
    {
        public bool Equals(TestDataModel x, TestDataModel y)
        {
            return x.PreviousUniqBlockIdent == y.UniqBlockIndent && x.HashSum == y.HashSum;
        }

        public int GetHashCode(TestDataModel obj)
        {
            return obj.GetHashCode();
        }
    }

}
