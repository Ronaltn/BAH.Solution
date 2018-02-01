using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub.MC.Dto
{
    /// <summary>
    /// 数据中心输出结果。
    /// </summary>
    [JsonObject]
    public class DataCenterInfoOutput
    {
        /// <summary>
        /// 主键。
        /// </summary>
        [JsonProperty]
        public string Id { get; set; }

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
    }
}
