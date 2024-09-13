using OwnAssistantCommon.RelatedData.Model;

namespace OwnAssistantCommon.RelatedData
{
    public static class ListRelatedDataExtensions
    {
        /// <summary>
        /// Get new package of related data. Get enumerable related data with uniq block ident, with refer on previous data block and data type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentData"></param>
        /// <param name="previousData"></param>
        /// <param name="comparisonF"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> GetNewDataPacket<T>(this IEnumerable<T> currentData, IEnumerable<T> previousData, Func<T, T, bool> comparisonF) where T : GeneralRelatedPackageDataModel
        {
            if (currentData == null && previousData == null)
            {
                //Todo: correct error
                throw new ArgumentNullException(nameof(currentData));
            }

            if (currentData == null || !currentData.Any())
            {
                return previousData.Select(x =>
                {
                    x.PreviousUniqBlockIdent = x.UniqBlockIndent;
                    x.UniqBlockIndent = Guid.NewGuid();
                    x.IncrementalDataType = IncrementalDataType.Removed;
                    return x;
                });
            }
            else if (previousData == null || !previousData.Any())
            {
                return currentData.Select(x =>
                {
                    x.PreviousUniqBlockIdent = null;
                    x.UniqBlockIndent = Guid.NewGuid();
                    x.IncrementalDataType = IncrementalDataType.New;
                    return x;
                });
            }
            else
            {
                if(comparisonF == null)
                {
                    //Todo: correct error
                    throw new ArgumentNullException(nameof(comparisonF));
                }

                List<T> newPackage = new List<T>();

                foreach(var currItem in currentData)
                {
                    var prev = previousData.FirstOrDefault(x => comparisonF(x, currItem));

                    if (prev == null)
                    {
                        currItem.PreviousUniqBlockIdent = null;
                        currItem.UniqBlockIndent = Guid.NewGuid();
                        currItem.IncrementalDataType = IncrementalDataType.New;
                    }
                    else
                    {
                        
                        if (currItem.HashSum == prev.HashSum)
                        {
                            currItem.UniqBlockIndent = prev.UniqBlockIndent;
                            currItem.PreviousUniqBlockIdent = prev.UniqBlockIndent;
                            currItem.IncrementalDataType = IncrementalDataType.Repeated;
                        }
                        else
                        {
                            currItem.UniqBlockIndent = Guid.NewGuid();
                            currItem.PreviousUniqBlockIdent = prev.UniqBlockIndent;
                            currItem.IncrementalDataType = IncrementalDataType.Modified;
                        }
                    }

                    newPackage.Add(currItem);
                }

                newPackage.AddRange(previousData.Where(x => !currentData.Any(y => comparisonF(x, y))));
                return newPackage;
            }
        }
    }
}
