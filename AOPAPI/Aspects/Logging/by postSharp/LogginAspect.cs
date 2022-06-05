using AOPAPI.Aspects.Utitiles;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Extensibility;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Unity;

namespace AOPAPI.Aspects.Logging.by_postSharp
{
    [PSerializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetExternalMemberAttributes = MulticastAttributes.Public)]
    public class LoggingAspect : OnMethodBoundaryAspect, IInstanceScopedAspect
    {
        [IntroduceMember(Visibility = PostSharp.Reflection.Visibility.Public, OverrideAction = MemberOverrideAction.Ignore)]
        [CopyCustomAttributes(typeof(DependencyAttribute))]
        [Dependency]
        public ILogger Logger { get; set; }

        [ImportMember("Logger", IsRequired = true)]
        public Property<ILogger> LoggerProperty;

        [IntroduceMember(Visibility = PostSharp.Reflection.Visibility.Public, OverrideAction = MemberOverrideAction.Ignore)]
        public Stopwatch watch { get; set; }




        public override void OnEntry(MethodExecutionArgs args)
        {
            watch = new Stopwatch();
            watch.Start();
            var arg = JsonConvert.SerializeObject(args.Arguments, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            LoggerProperty.Get().LogDebug($"input Params {arg}");
            base.OnEntry(args);
        }

        public override void OnException(MethodExecutionArgs args)
        {

            LoggerProperty.Get().LogError(args.Exception);
            args.FlowBehavior = FlowBehavior.Return;
            args.ReturnValue = null;
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            watch.Stop();
            LoggerProperty.Get().LogDebug($"Exection Time={  watch.Elapsed:mm\\:ss\\.fff}");
            base.OnExit(args);
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            var res = JsonConvert.SerializeObject(args.ReturnValue, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
           
            LoggerProperty.Get().LogDebug($"result ={res}");

            base.OnSuccess(args);
        }

        public object CreateInstance(AdviceArgs adviceArgs)
        {
            return this.MemberwiseClone();
        }

        public void RuntimeInitializeInstance()
        {
        }
    }
}