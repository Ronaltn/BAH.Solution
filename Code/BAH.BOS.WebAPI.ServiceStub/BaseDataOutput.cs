using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub
{
    /// <summary>
    /// 基础资料输出对象。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [JsonObject]
    public class BaseDataOutput<T>
    {
        /// <summary>
        /// 主键。
        /// </summary>
        [JsonProperty]
        public virtual T Id { get; set; }

        /// <summary>
        /// 编码。
        /// </summary>
        [JsonProperty]
        public virtual string Number { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [JsonProperty]
        public virtual string Name { get; set; }
    }
}
