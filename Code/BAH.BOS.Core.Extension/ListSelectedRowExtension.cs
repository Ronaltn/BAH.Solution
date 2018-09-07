using Kingdee.BOS.Core.Metadata;
using Kingdee.BOS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.BOS.Core.List
{
    /// <summary>
    /// 列表选中的行扩展。
    /// </summary>
    public static class ListSelectedRowExtension
    {
        /// <summary>
        /// 设定单据体实体主键。
        /// </summary>
        /// <param name="row">列表选中的行对象。</param>
        /// <param name="entryKey">要赋值的单据体实体主键。</param>
        /// <returns>返回原对象。</returns>
        public static ListSelectedRow SetEntryKey(this ListSelectedRow row,string entryKey)
        {
            row.EntryEntityKey = entryKey;
            return row;
        }//end method

        /// <summary>
        /// 将信息转换成字典形式并赋给FieldValues。
        /// </summary>
        /// <param name="row">列表选中的行对象。</param>
        /// <returns>返回原对象。</returns>
        public static ListSelectedRow ExtraFieldValues(this ListSelectedRow row)
        {
            if (row.FieldValues == null) row.FieldValues = new Dictionary<string, string>();

            row.FieldValues.Add(Appearance.BILL_HEAD, row.PrimaryKeyValue);
            if (!row.EntryEntityKey.IsNullOrEmptyOrWhiteSpace()) row.FieldValues.Add(row.EntryEntityKey, row.EntryPrimaryKeyValue);
            return row;
        }//end method

        /// <summary>
        /// 如果源单数据里有子单据体，需指定父级单据体。
        /// </summary>
        /// <param name="row">列表选中的行对象。</param>
        /// <param name="parentEntryKey">父单据体实体主键。</param>
        /// <param name="parentEntryId">父单据体数据主键。</param>
        /// <returns>返回原对象。</returns>
        public static ListSelectedRow ExtraFieldValues(this ListSelectedRow row, string parentEntryKey, string parentEntryId)
        {
            row.ExtraFieldValues();
            row.FieldValues.Add(parentEntryKey, parentEntryId);
            return row;
        }//end method

    }//end static class
}//end namespace
