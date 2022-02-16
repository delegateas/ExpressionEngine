﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExpressionEngine
{
    [JsonConverter(typeof(ValueContainerConverter))]
    public partial class ValueContainer : IComparable, IEquatable<ValueContainer>
    {
        private readonly dynamic _value; // TODO: Consider changing this to object - CLR TYPE
        private readonly ValueType _type;

        public ValueContainer(string value, bool tryToParse = false)
        {
            if (value == null)
            {
                _value = null;
                _type = ValueType.Null;
                return;
            }

            if (tryToParse)
            {
                if (value.Contains('.') && double.TryParse(value, out var fValue))
                {
                    _value = fValue;
                    _type = ValueType.Float;
                }
                else if (int.TryParse(value, out var iValue))
                {
                    _value = iValue;
                    _type = ValueType.Integer;
                }
                else if (bool.TryParse(value, out var bValue))
                {
                    _value = bValue;
                    _type = ValueType.Boolean;
                }
                else
                {
                    _value = value;
                    _type = ValueType.String;
                }
            }
            else
            {
                _value = value;
                _type = ValueType.String;
            }
        }

        public ValueContainer(string stringValue)
        {
            _value = stringValue;
            _type = ValueType.String;
        }
        public ValueContainer(DateTimeOffset dateTime)
        {
            _value = dateTime;
            _type = ValueType.Date;
        }

        public ValueContainer(float floatValue)
        {
            _value = Convert.ToDouble(floatValue);
            _type = ValueType.Float;
        }

        public ValueContainer(double floatValue)
        {
            _value = floatValue;
            _type = ValueType.Float;
        }

        public ValueContainer(int intValue)
        {
            _value = intValue;
            _type = ValueType.Integer;
        }

        public ValueContainer(bool boolValue)
        {
            _value = boolValue;
            _type = ValueType.Boolean;
        }

        public ValueContainer(IEnumerable<ValueContainer> arrayValue)
        {
            _value = arrayValue;
            _type = ValueType.Array;
        }

        internal ValueContainer(Dictionary<string, ValueContainer> objectValue, bool normalize)
        {
            _value = normalize ? objectValue.Normalize() : objectValue;

            _type = ValueType.Object;
        }

        public ValueContainer(Dictionary<string, ValueContainer> objectValue)
        {
            _value = objectValue.Normalize();
            _type = ValueType.Object;
        }

        public ValueContainer(ValueContainer valueContainer)
        {
            _type = valueContainer._type;
            _value = valueContainer._value;
        }

        public ValueContainer()
        {
            _type = ValueType.Null;
            _value = null;
        }

        public ValueType Type()
        {
            return _type;
        }

        public T GetValue<T>()
        {
            return _value;
        }

        public ValueContainer this[int i]
        {
            get
            {
                if (_type != ValueType.Array)
                {
                    throw new InvalidOperationException("Index operations can only be performed on arrays.");
                }

                return _value[i];
            }
            set
            {
                if (_type != ValueType.Array)
                {
                    throw new InvalidOperationException("Index operations can only be performed on arrays.");
                }

                ((List<ValueContainer>) _value)[i] = value;
            }
        }

        public ValueContainer this[string key]
        {
            get
            {
                if (_type != ValueType.Object)
                {
                    throw new InvalidOperationException("Index operations can only be performed on objects.");
                }

                var keyPath = key.Split('/');

                var current = GetValue<Dictionary<string, ValueContainer>>()[keyPath.First()];
                foreach (var xKey in keyPath.Skip(1))
                {
                    current = current.GetValue<Dictionary<string, ValueContainer>>()[xKey]; // Does not
                }

                return current;
            }
            set
            {
                if (_type != ValueType.Object)
                {
                    throw new InvalidOperationException("Index operations can only be performed on objects.");
                }

                var keyPath = key.Split('/');
                var finalKey = keyPath.Last();

                var current = _value;
                foreach (var xKey in keyPath.Take(keyPath.Length - 1))
                {
                    var dict = GetValue<Dictionary<string, ValueContainer>>();
                    var success = dict.TryGetValue(xKey, out var temp);

                    if (success)
                    {
                        current = temp;
                    }
                    else
                    {
                        dict[xKey] = new ValueContainer(new Dictionary<string, ValueContainer>());
                        current = dict[xKey].GetValue<Dictionary<string, ValueContainer>>();
                    }
                }

                current[finalKey] = value;
            }
        }

        public Dictionary<string, ValueContainer> AsDict()
        {
            if (_type == ValueType.Object)
            {
                return GetValue<Dictionary<string, ValueContainer>>();
            }

            throw new ExpressionEngineException("Can't get none object value container as dict.");
        }

        public override string ToString()
        {
            /*
             * This is called when debugging to display text in the variable overview.
             * This is also used when parsing.
             */
            return _type switch
            {
                ValueType.Boolean => _value.ToString(),
                ValueType.Integer => _value.ToString(),
                ValueType.Float => _value.ToString(),
                ValueType.String => _value,
                ValueType.Object => "{" + string.Join(",", GetValue<Dictionary<string, ValueContainer>>()
                    .Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}",
                ValueType.Array => "[" + string.Join(", ", GetValue<IEnumerable<ValueContainer>>().ToList()) + "]",
                ValueType.Null => "<null>",
                ValueType.Date => _value.ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public bool IsNull()
        {
            return _type == ValueType.Null;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
                throw new InvalidOperationException("Cannot compare these two...");

            var other = (ValueContainer) obj;

            switch (_value)
            {
                case bool b:
                    return b.CompareTo(other._value);
                case int i:
                    return i.CompareTo(other._value);
                case float f:
                    return f.CompareTo(other._value);
                case double f:
                    return f.CompareTo(other._value);
                case decimal f:
                    return f.CompareTo(other._value);
                case string s:
                    return s.CompareTo(other._value);
                case Dictionary<string, ValueContainer> d:
                    var d2 = (Dictionary<string, ValueContainer>) other._value;
                    return d.Count - d2.Count;
                case IEnumerable<ValueContainer> l:
                    var l2 = (IEnumerable<ValueContainer>) other._value;
                    return l.Count() - l2.Count();
                default:
                    return -1;
            }
        }

        public bool Equals(ValueContainer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            switch (_type)
            {
                case ValueType.Array when other._type == ValueType.Array:
                {
                    var thisArray = (IEnumerable<ValueContainer>) _value;
                    var otherArray = other.GetValue<IEnumerable<ValueContainer>>();

                    return thisArray.SequenceEqual(otherArray);
                }
                case ValueType.Object when other._type == ValueType.Object:
                {
                    var thisDict = (Dictionary<string, ValueContainer>) _value;
                    var otherDict = other.GetValue<Dictionary<string, ValueContainer>>();

                    return thisDict.Count == otherDict.Count && !thisDict.Except(otherDict).Any();
                }
                case ValueType.Integer when other._type == ValueType.Float:
                    var v = (double) _value;
                    return Math.Abs(Math.Floor(v) - other._value) < double.Epsilon;
                case ValueType.Float when other._type == ValueType.Integer:
                {
                    return Math.Abs(Math.Floor(_value) - other._value) < double.Epsilon;
                }
                case ValueType.Float:
                {
                    // TODO: Figure out how to handle comparison and in general how to handle float/double..
                    // assignee: thygesteffensen
                    return Math.Abs(_value - other._value) < 0.01;
                }
                default:
                    return Equals(_value, other._value) && _type == other._type;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ValueContainer) obj);
        }

        public override int GetHashCode()
        {
            return new {_type, _value}.GetHashCode();
        }

        public bool ContainsKey(string key)
        {
            if (_type != ValueType.Object) return false;

            var current = AsDict();
            var keys = key.Split('/');
            for (var i = 0; i < keys.Length - 1; i++)
            {
                if (current.ContainsKey(keys[i]))
                {
                    current = current[keys[i]].AsDict();
                }
                else
                {
                    return false;
                }
            }

            return current.ContainsKey(keys[keys.Length - 1]);
        }
    }

    public static class ValueContainerExtension
    {
        public static async ValueTask<ValueContainer> CreateValueContainerFromJToken(JToken json,
            IExpressionEngine expressionEngine = null)
        {
            if (json == null)
            {
                return new ValueContainer();
            }

            var v = await JsonToValueContainer(json, expressionEngine);
            return v;
        }

        private static async ValueTask<ValueContainer> JsonToValueContainer(JToken json,
            IExpressionEngine expressionEngine)
        {
            switch (json)
            {
                case JObject _:
                {
                    var dictionary = json.ToDictionary(pair => ((JProperty) pair).Name, async token =>
                    {
                        if (token.Children().Count() != 1)
                        {
                            var t1 = await JsonToValueContainer(token.Children().First(), expressionEngine);
                            return t1;
                        }

                        var t = token.First;
                        var result = t.Type switch
                        {
                            JTokenType.String when expressionEngine != null => expressionEngine.ParseToValueContainer(
                                t.Value<string>()),
                            JTokenType.String => new ValueTask<ValueContainer>(new ValueContainer(t.Value<string>())),
                            JTokenType.Boolean => new ValueTask<ValueContainer>(new ValueContainer(t.Value<bool>())),
                            JTokenType.Integer => new ValueTask<ValueContainer>(new ValueContainer(t.Value<int>())),
                            JTokenType.Float => new ValueTask<ValueContainer>(new ValueContainer(t.Value<float>())),
                            JTokenType.Date => new ValueTask<ValueContainer>(new ValueContainer(t.Value<DateTimeOffset>())),
                            _ => JsonToValueContainer(token.Children().First(), expressionEngine)
                        };

                        return new ValueContainer(await result);
                    });

                    var pairs = await Task.WhenAll
                    (
                        dictionary.Select
                        (
                            async pair => new {pair.Key, Value = await pair.Value}
                        )
                    );

                    return new ValueContainer(pairs.ToDictionary(pair => pair.Key, pair => pair.Value));
                }
                case JArray jArray:
                    return jArray.Count > 0 ? new ValueContainer() : JArrayToValueContainer(jArray);
                case JValue jValue:
                    if (jValue.HasValues)
                    {
                        throw new ExpressionEngineException(
                            "When parsing JToken to ValueContainer, the JToken as JValue can only contain one value.");
                    }

                    return jValue.Type switch
                    {
                        JTokenType.Boolean => new ValueContainer(jValue.Value<bool>()),
                        JTokenType.Integer => new ValueContainer(jValue.Value<int>()),
                        JTokenType.Float => new ValueContainer(jValue.Value<float>()),
                        JTokenType.Null => new ValueContainer(),
                        JTokenType.String when expressionEngine != null => await expressionEngine.ParseToValueContainer(
                            jValue.Value<string>()),
                        JTokenType.String => new ValueContainer(jValue.Value<string>()),
                        JTokenType.None => new ValueContainer(),
                        JTokenType.Guid => new ValueContainer(jValue.Value<Guid>().ToString()),
                        JTokenType.Date => new ValueContainer(jValue.Value<DateTimeOffset>()),
                        _ => throw new ExpressionEngineException(
                            $"{jValue.Type} is not yet supported in ValueContainer conversion")
                    };
                default:
                    throw new ExpressionEngineException("Could not parse JToken to ValueContainer.");
            }
        }

        private static ValueContainer JArrayToValueContainer(JArray json)
        {
            var list = new List<ValueContainer>();

            foreach (var jToken in json)
            {
                if (jToken.GetType() != typeof(JValue))
                {
                    throw new ExpressionEngineException("Json can only contain arrays of primitive types.");
                }

                var t = (JValue) jToken;
                switch (t.Value)
                {
                    case int i:
                        list.Add(new ValueContainer(i));
                        break;
                    case string s:
                        list.Add(new ValueContainer(s));
                        break;
                    case bool b:
                        list.Add(new ValueContainer(b));
                        break;
                    case double d:
                        list.Add(new ValueContainer(d));
                        break;
                    default:
                        throw new ExpressionEngineException(
                            $"Type {t.Value.GetType()} is not recognized when converting Json to ValueContainer.");
                }
            }

            return new ValueContainer(list);
        }
    }

    public class ValueContainerComparer : EqualityComparer<ValueContainer>
    {
        public override bool Equals(ValueContainer x, ValueContainer y)
        {
            if (x == null || y == null)
            {
                return x == null && y == null;
            }

            return x.Equals(y);
        }

        public override int GetHashCode(ValueContainer obj)
        {
            return obj.GetHashCode();
        }
    }
}