﻿
using System.IO;
using System.Linq;
using STULib;
using STULib.Types;
using STULib.Types.STUUnlock;
using static DataTool.Program;
using static DataTool.Helper.IO;
using static STULib.Types.Generic.Common;
using DataTool.DataModels;

namespace DataTool.Helper {
    public static class STUHelper {
        public static string GetDescriptionString(ulong key) {
            STUDescription description = GetInstance<STUDescription>(key);
            return GetString(description?.String);
        }
        
        public static ISTU OpenSTUSafe(ulong key) {
            using (Stream stream = OpenFile(key)) {
                return stream == null ? null : ISTU.NewInstance(stream, BuildVersion);
            }
        }

        public static T[] GetInstances<T>(ulong key) where T : STUInstance  {
            ISTU stu = OpenSTUSafe(key);
            return stu?.Instances.OfType<T>().ToArray() ?? new T[0];
        }

        public static T GetInstance<T>(ulong key) where T : STUInstance  {
            ISTU stu = OpenSTUSafe(key);
            return stu?.Instances.OfType<T>().FirstOrDefault();
        }

        public static ItemInfo GatherUnlock(ulong key) {
            Cosmetic unlock = GetInstance<Cosmetic>(key);
            if (unlock == null) return null;

            string name = GetString(unlock.CosmeticName);
            string description = GetDescriptionString(unlock.CosmeticDescription);
            string availableIn = GetString(unlock.CosmeticAvailableIn);

            if (unlock is Currency) {
                name = $"{(unlock as Currency).Amount} Credits";
            } else if (unlock is Portrait) {
                Portrait portrait = unlock as Portrait;
                name = $"{portrait.Tier} Star: {portrait.Star} Level: {portrait.Level}";
            }

            if (name == null)
                name = $"{OWLib.GUID.LongKey(key):X12}";

            return new ItemInfo(name, unlock.CosmeticRarity.ToString(), unlock.GetType().Name, description, availableIn, unlock, key);
        }
    }
}