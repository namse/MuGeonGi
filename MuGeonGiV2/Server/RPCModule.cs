using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MuGeonGiV2.Core;
using Nancy;
using static System.Char;

namespace MuGeonGiV2.Server
{
    public class RpcModule : NancyModule
    {
        public RpcModule() : base("/{Type}/{Uuid}")
        {
            Post["/{MethodName}"] = parameters =>
            {
                object caller;
                
                if (parameters.Type == "jack")
                {
                    if (!Jack.TryGet(parameters.Uuid, out Jack jack))
                    {
                        return new NotFoundResponse();
                    }
                    caller = jack;
                }
                else
                {
                    if (!Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                    {
                        return new NotFoundResponse();
                    }
                    caller = instrument;
                }

                var type = caller.GetType();
                MethodInfo method = type.GetMethod(parameters.MethodName);
                method.Invoke(caller, null);
                return new Response();
            };
            Post["/{Property}/{Value}"] = parameters =>
            {
                if (!Storable.TryGet(parameters.Uuid, out Storable storable))
                {
                    return new NotFoundResponse();
                }
                // TODO : Check type

                var type = storable.GetType();
                string property = parameters.Property;
                var methodName = $"Set{ToUpper(property[0])}{property.Substring(1)}";

                var method = type.GetMethod(methodName);
                if (method != null)
                {
                    var param = Convert.ChangeType(parameters.Value, method.GetParameters()[0].ParameterType);

                    var parameterArray = new[]
                    {
                        param,
                    };
                    method.Invoke(storable, parameterArray);
                    return new Response();
                }
                var fieldName = $"{ToUpper(property[0])}{property.Substring(1)}";
                var propertyInfo = type.GetProperty(fieldName);
                if (propertyInfo != null)
                {
                    var fieldType = propertyInfo.PropertyType;
                    var value = Convert.ChangeType(parameters.Value, fieldType);
                    propertyInfo.SetValue(storable, value);
                    return new Response();
                }

                return new NotFoundResponse();
            };
        }
    }
}
