using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnAssistantCommon
{
    /// <summary>
    /// Auxiliary class of solution
    /// </summary>
    public static class GeneralUtils
    {
        /// <summary>
        /// Getting description for enum
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum val)
        {
            var info = val.GetType().GetField(val.ToString());

            var descAttr = info.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if(descAttr != null && descAttr.Any())
                return descAttr.FirstOrDefault().Description;

            return val.ToString();
        }
    }
}
