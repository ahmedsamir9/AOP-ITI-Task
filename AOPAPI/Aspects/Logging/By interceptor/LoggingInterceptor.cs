using AOPAPI.Aspects.Utitiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace AOPAPI.Aspects.Logging.By_interceptor
{
    public class LoggingInterceptor : IInterceptionBehavior
    {
        private readonly ILogger _logger;

        public LoggingInterceptor(ILogger logger)
        {
            _logger = logger;
        }
        public bool WillExecute => true;

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // before exec next
            var watch = new Stopwatch();
            watch.Start();
            var args = JsonConvert.SerializeObject(input.Arguments, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            _logger.LogDebug($"the params is {args}");
            var result = getNext()(input, getNext);
            var res = JsonConvert.SerializeObject(result.ReturnValue, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            _logger.LogDebug($"the result is {res}");
             watch.Stop();
            _logger.LogDebug($"Exection Time={watch.Elapsed:mm\\:ss\\.fff}");
            // after exec next
            if (result.Exception != null)
            {
                _logger.LogError(result.Exception);
                var defaultValue = GetDefaultValue(input.MethodBase);
                return input.CreateMethodReturn(defaultValue);
            };
            return result;
        }

        private object GetDefaultValue(MethodBase methodBase)
        {
            var methodInfo = methodBase as MethodInfo;
            var returnType = methodInfo.ReturnType;
            if (!returnType.IsValueType)
                return null;
            if (returnType == typeof(int))
                return default(int);
            if (returnType == typeof(bool))
                return default(bool);
            return default;
        }
    }
}