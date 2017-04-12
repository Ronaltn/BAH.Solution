using Kingdee.BOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAH.BOS.WebAPI.ServiceStub
{
    /// <summary>
    /// 接口方法抽象类。
    /// </summary>
    public abstract class APIMethod<TParameter>
    {
        /// <summary>
        /// 上下文对象。
        /// </summary>
        public Context Context { get; protected set; }

        /// <summary>
        /// 输入参数。
        /// </summary>
        public TParameter Parameter { get; protected set; }

        /// <summary>
        /// 输出结果。
        /// </summary>
        public ServiceResult Result { get; protected set; }

        /// <summary>
        /// 接口方法构造。
        /// </summary>
        /// <param name="ctx">上下文对象。</param>
        /// <param name="parameter">传入参数。</param>
        public APIMethod(Context ctx, TParameter parameter)
        {
            this.Context = ctx;
            this.Parameter = parameter;
            this.Result = new ServiceResult();
        }

        /// <summary>
        /// 执行接口方法。
        /// </summary>
        public virtual void Execute()
        {
            if (!this.Validate()) return;

            try
            {
                this.Implement();
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        /// <summary>
        /// 验证传入参数。
        /// </summary>
        /// <returns>返回验证参数结果，true表示验证通过，false表示验证未通过。</returns>
        public abstract bool Validate();

        /// <summary>
        /// 实现方法。
        /// </summary>
        public abstract void Implement();

        /// <summary>
        /// 处理异常。
        /// </summary>
        /// <param name="ex">异常实例。</param>
        public abstract void HandleException(Exception ex);
    }

    /// <summary>
    /// 接口方法抽象类。
    /// </summary>
    public abstract class APIMethod<TParameter, TResult> : APIMethod<TParameter>
    {
        /// <summary>
        /// 输出结果。
        /// </summary>
        public new ServiceResult<TResult> Result { get; protected set; }

        /// <summary>
        /// 接口方法构造。
        /// </summary>
        /// <param name="ctx">上下文对象。</param>
        /// <param name="parameter">传入参数。</param>
        public APIMethod(Context ctx, TParameter parameter) : base(ctx, parameter)
        {
            this.Context = ctx;
            this.Parameter = parameter;
            this.Result = new ServiceResult<TResult>();
        }
    }
}
