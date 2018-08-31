﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DataTool;
using DataTool.Helper;
using TankLib;
using TankLib.STU;
using TankTonka.Formatters;
using TankTonka.Models;
using Utf8Json;
using Utf8Json.Resolvers;

namespace TankTonka {
    internal static class Program {
        private static string _outputDirectory;
        
        public static void Main(string[] args) {
            const string locale = "enUS";
            
            DataTool.Program.Flags = new ToolFlags {
                OverwatchDirectory = args[0],
                Language = locale,
                SpeechLanguage = locale,
                UseCache = true,
                CacheCDNData = true
            };
            
            _outputDirectory = args[1];
            ushort[] types = args.Skip(2).Select(x => ushort.Parse(x, NumberStyles.HexNumber)).ToArray();
            if (types.Length == 0) types = null;

            DataTool.Program.InitStorage();
            
            CompositeResolver.RegisterAndSetAsDefault(new IJsonFormatter[] {
                new AssetRepoTypeFormatter(), 
                new ResourceGUIDFormatter()
            }, new[] {
                StandardResolver.Default
            });
            
            if (types == null) {
                foreach (KeyValuePair<ushort, HashSet<ulong>> type in DataTool.Program.TrackedFiles) {
                    ProcessType(type.Key);
                }
            } else {
                foreach (ushort type in types) {
                    ProcessType(type);
                }
            }
        }

        private static void ProcessType(ushort type) {
            TypeManifest manifest = new TypeManifest {
                Type = (Common.AssetRepoType) type
            };

            if (TypeClassifications.STUv2.Contains(type)) {
                ProcessTypeSTUv2(type, manifest);
            }
        }

        private static void ProcessTypeSTUv2(ushort type, TypeManifest manifest) {
            manifest.StructuredDataInfo = new Common.StructuredDataInfo();
            HashSet<Common.AssetRepoType> referenceTypes = new HashSet<Common.AssetRepoType>();
            
            string typeDirectory = Path.Combine(_outputDirectory, type.ToString("X3"));
            string assetDirectory = Path.Combine(typeDirectory, "assets");
            IO.CreateDirectorySafe(assetDirectory);

            Parallel.ForEach(DataTool.Program.TrackedFiles[type], x => ProcessAssetSTUv2(x, assetDirectory));

            manifest.GUIDReferenceTypes = referenceTypes;
        }

        private static void ProcessAssetSTUv2(ulong guid, string typeDir) {
            teStructuredData structuredData = STUHelper.OpenSTUSafe(guid);
            if (structuredData == null) return;

            AssetRecord record = new AssetRecord {
                GUID = (teResourceGUID) guid,
                StructuredDataInfo = new Common.StructuredDataInfo(),
                References = new HashSet<teResourceGUID>()
            };

            foreach (STUInstance instance in structuredData.Instances) {
                if (instance == null) continue;
                STUv2ProcessInstance(record, instance);
            }

            using (Stream outputFile = File.OpenWrite(Path.Combine(typeDir, $"{teResourceGUID.AsString(guid)}.json"))) {
                outputFile.SetLength(0);
                byte[] buf = JsonSerializer.PrettyPrintByteArray(JsonSerializer.Serialize(record));
                outputFile.Write(buf, 0, buf.Length);
            }
        }

        private static void STUv2ProcessInstance(AssetRecord record, STUInstance instance) {
            var fields = GetFields(instance.GetType(), true);
                
            foreach (FieldInfo field in fields) {
                object fieldValue = field.GetValue(instance);
                Type fieldType = field.FieldType;
                if (fieldValue == null) continue;

                var fieldAttribute = field.GetCustomAttribute<STUFieldAttribute>();
                if (fieldAttribute != null) {
                    if (fieldAttribute.ReaderType == typeof(InlineInstanceFieldReader)) {
                        if (!fieldType.IsArray) {
                            STUv2ProcessInstance(record, (STUInstance)fieldValue);
                        } else {
                            IEnumerable enumerable = (IEnumerable) fieldValue;
                            foreach (object val in enumerable) {
                                STUv2ProcessInstance(record, (STUInstance)val);
                            }
                        }
                        return;
                    }
                }

                if (fieldType.IsArray) {
                    Type elementType = fieldType.GetElementType();
                    IEnumerable enumerable = (IEnumerable) fieldValue;
                    foreach (object val in enumerable) {
                        STUv2ProcessField(record, val, elementType);
                    }
                } else {
                    STUv2ProcessField(record, fieldValue, fieldType);
                }
            }
        }

        private static void STUv2ProcessField(AssetRecord record, object value, Type fieldType) {
            if (value is teResourceGUID resGuid) {
                record.References.Add(resGuid);
                return;
            }

            if (fieldType.IsGenericType) {
                Type genericBase = fieldType.GetGenericTypeDefinition();
                Type[] genericParams = fieldType.GetGenericArguments();
                if (genericBase == typeof(teStructuredDataAssetRef<>)) {

                    MethodInfo method = typeof(Program).GetMethod(nameof(GetAssetRefGUID));
                    method = method.MakeGenericMethod(genericParams);

                    record.References.Add((teResourceGUID)method.Invoke(null, new []{value}));
                    
                    // fuck reflection
                }
                return;
            }

            if (value is ulong @ulong) {
                if (@ulong == 0) return;
                ushort type = teResourceGUID.Type(@ulong);
                if (type > 1 && type < 0xFF) {
                    record.References.Add((teResourceGUID) @ulong);
                }
            }
        } 
        
        internal static FieldInfo[] GetFields(Type type, bool doParent=false) {
            FieldInfo[] parent = new FieldInfo[0];
            if (type.BaseType != null && type.BaseType.Namespace != null &&
                !type.BaseType.Namespace.StartsWith("System.") && doParent) parent = GetFields(type.BaseType);
            return parent.Concat(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                BindingFlags.DeclaredOnly)).ToArray();
        }

        public static teResourceGUID GetAssetRefGUID<T>(teStructuredDataAssetRef<T> val) {
            return val.GUID;
        }
    }
}