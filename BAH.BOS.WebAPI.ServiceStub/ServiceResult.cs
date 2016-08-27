using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub
{
    /// <summary>
    /// 服务结果。
    /// </summary>
    [JsonObject]
    public class ServiceResult
    {
        /// <summary>
        /// 结果代码。
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }//end property

        /// <summary>
        /// 结果消息。
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }//end property
    }//end class

    /// <summary>
    /// 服务结果，定义Body为泛型。
    /// </summary>
    /// <typeparam name="T">泛型定义。</typeparam>
    [JsonObject]
    public class ServiceResult<T> : ServiceResult
    {
        /// <summary>
        /// 主体对象，类型由泛型定义。
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }//end property
    }//end class

}//end namespace
