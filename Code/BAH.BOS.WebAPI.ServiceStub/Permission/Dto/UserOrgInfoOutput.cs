using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub.Permission.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [JsonObject]
    public class UserOrgInfoOutput
    {
        /// <summary>
        /// 主键。
        /// </summary>
        [JsonProperty]
        public long Id { get; set; }

        /// <summary>
        /// 编码。
        /// </summary>
        [JsonProperty]
        public string Number { get; set; }


        /// <summary>
        /// 名称。
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

    }//end class
}//end namespace
