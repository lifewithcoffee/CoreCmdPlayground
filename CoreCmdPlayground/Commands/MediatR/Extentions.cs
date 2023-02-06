using CoreCmdPlayground.Commands.MediatR.PipelineBehaviors;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CoreCmdPlayground.Commands.MediatR
{
    static class Extentions
    {
        static public void AddMediatRDemo(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            /** [MCN] IPipelineBehavior<> will be invoked by their registration order
             * 
             * Note that IRequestPreProcessor<> and IRequestPostProcessor<> must be registered to
             * IPipelineBehavior<> using RequestPreProcessorBehavior<> & RequestPostProcessorBehavior<>.
             * 
             * Statement like the following ways won't work:
             * 
             *     services.AddScoped(typeof(IRequestPreProcessor<>), typeof(GenericRequestPreProcessor<>));
             *     services.AddTransient<IRequestPreProcessor, PingPipelinePreProcess<Ping>>();
             *     services.AddScoped(typeof(IRequestPreProcessor<>), typeof(PingPipelinePreProcess<>));
             */
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));   // for IRequestPreProcessor
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));  // for IRequestPostProcessor

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior2<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IPipelineBehavior<Ping, string>, PingPipelineBehavior>();


            // add validators
            services.AddScoped<IValidator<Ping>, PingRequestValidator>();
        }
    }
}
