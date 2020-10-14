using System;
using System.Collections.Generic;
using System.Reflection;

namespace main
{
    public class FakerImpl : IFaker
    {
        public IFakerConfig FakerConfig { get; }

        public FakerImpl(IFakerConfig fakerConfig)
        {
            FakerConfig = fakerConfig;
        }

        public T Create<T>()
        {
            return (T) Create(typeof(T));
        }

        private object Create(Type type)
        {
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            object resultObject = type.IsValueType ? Activator.CreateInstance(type) : null;

            if (constructorInfos.Length <= 0)
            {
                return resultObject;
            }

            foreach (ConstructorInfo constructorInfo in constructorInfos)
            {
                ParameterInfo[] parameterInfos = constructorInfo.GetParameters();
                List<object> parameterValues = GenerateParameterValues(parameterInfos);
                resultObject = constructorInfo.Invoke(parameterValues.ToArray());
                PropertyInfo[] propertyInfos = type.GetProperties();
                SetObjectProperties(propertyInfos, resultObject);
            }

            return resultObject;
        }

        private void SetObjectProperties(PropertyInfo[] propertyInfos, object resultObject)
        {
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                SetObjectProperty(propertyInfo, resultObject);
            }
        }

        private void SetObjectProperty(PropertyInfo propertyInfo, object resultObject)
        {
            if (propertyInfo.CanWrite)
            {
                Type propertyType = propertyInfo.PropertyType;
                object value = GenerateParameterValue(propertyType);
                propertyInfo.SetValue(resultObject, value);
            }
        }

        private List<object> GenerateParameterValues(ParameterInfo[] parameterInfos)
        {
            List<object> parameters = new List<object>();
            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                Type parameterType = parameterInfo.ParameterType;
                object value = GenerateParameterValue(parameterType);
                parameters.Add(value);
            }

            return parameters;
        }

        private object GenerateParameterValue(Type type)
        {
            object value;
            if (FakerConfig.ContainsTypeGenerator(type))
            {
                IValueGenerator valueGenerator = FakerConfig.GetGenerator(type);
                value = valueGenerator.GenerateValue();
            }
            else
            {
                value = type.IsValueType ? Activator.CreateInstance(type) : null;
            }
            return value;
        }
    }
}