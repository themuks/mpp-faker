using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using faker.generator;

namespace faker.entity.impl
{
    public class FakerImpl : IFaker
    {
        private const string PluginsPath = "plugins";
        private readonly Stack<Type> _constructionFlowStack = new Stack<Type>();
        private readonly List<IValueGenerator> _valueGenerators = new List<IValueGenerator>();

        public FakerImpl()
        {
            Initialize();
        }

        public int MaxCircularDepth { get; set; }

        public T Create<T>()
        {
            var type = typeof(T);
            return (T) Create(type);
        }

        private void Initialize()
        {
            LoadPluginGenerators(PluginsPath);
            LoadExistGenerators();
        }

        private object Create(Type type)
        {
            var resultObject = type.IsValueType ? Activator.CreateInstance(type) : null;

            if (type.IsPrimitive)
            {
                resultObject = GenerateParameterValue(type);
                return resultObject;
            }

            if (_constructionFlowStack.Count(t => t == type) > MaxCircularDepth) return resultObject;

            _constructionFlowStack.Push(type);

            if (IsGeneratorExist(type))
            {
                resultObject = GenerateParameterValue(type);
                return resultObject;
            }

            var constructorInfos = type.GetConstructors();
            if (type.IsValueType && constructorInfos.Length == 0)
            {
                var propertyInfos = type.GetProperties();
                SetObjectProperties(propertyInfos, resultObject);
                var fieldInfos = type.GetFields();
                SetObjectFields(fieldInfos, resultObject);
                return resultObject;
            }

            if (constructorInfos.Length == 0) return resultObject;

            foreach (var constructorInfo in constructorInfos)
            {
                var parameterInfos = constructorInfo.GetParameters();
                var parameterValues = GenerateParameterValues(parameterInfos);
                resultObject = constructorInfo.Invoke(parameterValues.ToArray());
                var propertyInfos = type.GetProperties();
                SetObjectProperties(propertyInfos, resultObject);
                var fieldInfos = type.GetFields();
                SetObjectFields(fieldInfos, resultObject);
                break;
            }

            _constructionFlowStack.Pop();
            return resultObject;
        }

        private void SetObjectProperties(PropertyInfo[] propertyInfos, object resultObject)
        {
            foreach (var propertyInfo in propertyInfos) SetObjectProperty(propertyInfo, resultObject);
        }

        private void SetObjectProperty(PropertyInfo propertyInfo, object resultObject)
        {
            if (propertyInfo.CanWrite)
            {
                var propertyType = propertyInfo.PropertyType;
                var value = GenerateParameterValue(propertyType);
                propertyInfo.SetValue(resultObject, value);
            }
        }

        private void SetObjectFields(FieldInfo[] fieldInfos, object resultObject)
        {
            foreach (var fieldInfo in fieldInfos) SetObjectField(fieldInfo, resultObject);
        }

        private void SetObjectField(FieldInfo fieldInfo, object resultObject)
        {
            var fieldType = fieldInfo.FieldType;
            var value = GenerateParameterValue(fieldType);
            fieldInfo.SetValue(resultObject, value);
        }

        private List<object> GenerateParameterValues(ParameterInfo[] parameterInfos)
        {
            var parameters = new List<object>();
            foreach (var parameterInfo in parameterInfos)
            {
                var parameterType = parameterInfo.ParameterType;
                var value = GenerateParameterValue(parameterType);
                parameters.Add(value);
            }

            return parameters;
        }

        private object GenerateParameterValue(Type type)
        {
            var value = type.IsValueType ? Activator.CreateInstance(type) : null;

            IValueGenerator valueGenerator;
            if (type.GetGenericArguments().Length > 0)
            {
                var genericArgument = type.GetGenericArguments()[0];
                valueGenerator = _valueGenerators.FirstOrDefault(g => g.Type == type || g.Type == genericArgument);
            }
            else
            {
                valueGenerator = _valueGenerators.FirstOrDefault(g => g.Type == type);
            }

            if (valueGenerator != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                    value = valueGenerator.GenerateList();
                else
                    value = valueGenerator.GenerateValue();
            }
            else
            {
                if (!type.IsPrimitive) value = Create(type);
            }

            return value;
        }
        
        private bool IsGeneratorExist(Type type)
        {
            return _valueGenerators.Count(g => g.Type == type) > 0;
        }

        private void LoadPluginGenerators(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists) return;

            var files = Directory.GetFiles(path, "*.dll");
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                var types = assembly.GetTypes()
                    .Where(t =>
                        t.GetInterfaces().Any(i => i == typeof(IValueGenerator)) && !t.IsAbstract);
                foreach (var type in types)
                {
                    var valueGenerator = assembly.CreateInstance(type.FullName) as IValueGenerator;
                    _valueGenerators.Add(valueGenerator);
                }
            }
        }

        private void LoadExistGenerators()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes()
                .Where(t =>
                    t.GetInterfaces().Any(i => i == typeof(IValueGenerator)) && !t.IsAbstract);
            foreach (var type in types)
            {
                var valueGenerator = assembly.CreateInstance(type.FullName) as IValueGenerator;
                _valueGenerators.Add(valueGenerator);
            }
        }
    }
}