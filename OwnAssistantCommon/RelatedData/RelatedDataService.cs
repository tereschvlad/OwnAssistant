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
    public class RelatedDataService
    {
        //public IEnumerable<TestDataModel> GetNewRelatedData(IEnumerable<TestDataModel> currentData, IEnumerable<TestDataModel> previousData)
        //{
        //    var t1 = currentData.ExceptBy(previousData.Select(x => x.UniqBlockIndent),);

        //    var t2 = currentData.Where(x => !previousData.Any(y => y.UniqBlockIndent == x.PreviousUniqBlockIdent));

        //    return currentData.Where(x => !previousData.Any(y => y.UniqBlockIndent == x.PreviousUniqBlockIdent));
        //}

        //public IEnumerable<TestDataModel> GetModifiedRelatedData(IEnumerable<TestDataModel> currentData, IEnumerable<TestDataModel> previousData)
        //{
        //    var t1 = currentData.Intersect(previousData, new EqualityComparerAbsentRelatedData());

        //    return currentData.Where(x => previousData.Any(y => y.UniqBlockIndent == x.PreviousUniqBlockIdent && y.HashSum != x.HashSum));
        //}

        //public IEnumerable<TestDataModel> GetRemovedRelatedData(IEnumerable<TestDataModel> currentData, IEnumerable<TestDataModel> previousData)
        //{
        //    return previousData.Where()
        //}
    }

    public static class ListRelatedDataExtensions
    {
        public static void DefineBlockIndentRelatedDataType<T>(this IEnumerable<T> currentData, IEnumerable<T> previousData, Func<T, bool> comparison) where T : IncrementalDataUtilsModel 
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
                            item.UniqBlockIndent = prev.UniqBlockIndent;
                        else
                            item.UniqBlockIndent = Guid.NewGuid();
                    }
                    else
                        item.UniqBlockIndent = Guid.NewGuid();
                }
            }
            
        }

        public static IEnumerable<TestDataModel> GetAbsentRelatedData(this IEnumerable<TestDataModel> currentData, IEnumerable<TestDataModel> previousData, IEqualityComparer<TestDataModel> equality)
        {
            return currentData.Except(previousData, equality);
        }

        public static IEnumerable<TestDataModel> GetModifiedRelatedData(this IEnumerable<TestDataModel> currentData, IEnumerable<TestDataModel> previousData, Func<TestDataModel, bool> comparison)
        {
            List<TestDataModel> resList = new List<TestDataModel>();

            foreach(var item in currentData)
            {
                var prev = previousData.FirstOrDefault(comparison);
                
                if(prev != null)
                {
                    item.PreviousUniqBlockIdent = prev.UniqBlockIndent;
                    resList.Add(item);
                }
            }
            return resList;
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
