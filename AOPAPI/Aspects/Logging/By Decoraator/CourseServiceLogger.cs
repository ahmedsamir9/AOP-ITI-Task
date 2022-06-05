using AOPAPI.Aspects.Decorator;
using AOPAPI.Aspects.Utitiles;
using AOPAPI.BLL;
using AOPAPI.DAL;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AOPAPI.Aspects.Logging.By_Decoraator
{
    public class CourseServiceLogger : CourseBaseDecorator
    {
        private readonly ILogger _logger;
        public CourseServiceLogger(ICourseService courseService , ILogger logger) : base(courseService)
        {
            _logger = logger;
        }
        public override Course GetById(int id)
        {
            var watch = new Stopwatch();
            watch.Start();
            try
            {
                _logger.LogDebug($"the params is {id}");
                var result = base.GetById(id);
                _logger.LogDebug($"the result is {result}");
                watch.Stop();
                _logger.LogDebug($"Exection Time={watch.Elapsed:mm\\:ss\\.fff}");
                return result;
            }
            catch (Exception ex) { 
                _logger.LogError(ex);
                return default;
            }
           
        }
    }
}