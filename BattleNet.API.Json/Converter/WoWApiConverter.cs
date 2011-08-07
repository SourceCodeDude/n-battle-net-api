using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace BattleNet.API.Converter
{
    using System.Reflection;
    using BattleNet.API.WoW;
    using System.Collections.ObjectModel;

    public class WowAPIConverter : JavaScriptConverter
    {
        static MethodInfo method;
        static WowAPIConverter()
        {            
            MethodInfo match = typeof(JavaScriptSerializer).GetMethod(
                "ConvertToType",
                new Type[] { typeof(object) },
                new ParameterModifier[] { new ParameterModifier(1) });
            method = match;
            
            /**
             * in 3.5 there is only one ConvertToType method..  ConvertToType<T>(Object)
             * in 4.0 there is two.  ConvertToType<T> and ConverToType(Object,Type)
             * 
             * we one the single parameter one
             */            
        }
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {            
            object obj = Activator.CreateInstance(type);
            foreach (KeyValuePair<string, object> kvp in dictionary)
            {
                string memberName = Translate(type, kvp.Key);
                MemberInfo[] mi = type.GetMember(memberName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (mi.Length > 0)
                {
                    object v = null;
                    switch (mi[0].MemberType)
                    {
                        case MemberTypes.Property:
                            PropertyInfo pi = mi[0] as PropertyInfo;
                            //TODO: May want to Cache these for performance..  adds ~200ms onto tests
                            v = method.MakeGenericMethod(pi.PropertyType).Invoke(serializer, new object[]{kvp.Value} );
                            // 4.0 only :-/
                            //v = serializer.ConvertToType(kvp.Value , pi.PropertyType);
                            pi.SetValue(obj, v, null);
                            break;
                        case MemberTypes.Field:
                            FieldInfo fi = mi[0] as FieldInfo;
                            //TODO: May want to Cache these for performance
                            v = method.MakeGenericMethod(fi.FieldType).Invoke(serializer, new object[] { kvp.Value });
                            // 4.0 only :-/
                            //v = serializer.ConvertToType(kvp.Value, fi.FieldType);
                            fi.SetValue(obj, v);
                            break;
                        default:
                            MyJavaScriptSerializer mjss = serializer as MyJavaScriptSerializer;
                            if (mjss != null)
                            {
                                mjss.OnError("Member " + kvp.Key + " is not a field or property in type " + type.FullName);
                            }
                            break;
                    }
                }
                else
                {
                    MyJavaScriptSerializer mjss = serializer as MyJavaScriptSerializer;
                    if (mjss != null)
                    {
                        mjss.OnError("Member " + kvp.Key + " was not found in type " + type.FullName + " but found in json");
                    }                    
                }
            }            
            return obj;
        }

        public string Translate(Type t, string key)
        {
            if (t == typeof(Stats))
            {
                switch(key)
                {
                    case "str": return "strength";
                    case "agi": return "agility";
                    case "sta": return "stamina";
                    case "int": return "Intelect";
                    case "spr": return "Spirit";
                    case "crit": return "CritPercent";
                    case "mainHandDmgMin": return "MainHandDamageMin";
                    case "mainHandDmgMax": return "MainHandDamageMax";
                    case "offHandDmgMin": return "OffHandDamageMin";
                    case "offHandDmgMax": return "OffHandDamageMax";
                    case "rangedDmgMin": return "RangedDamageMin";
                    case "rangedDmgMax": return "RangedDamageMax";
                    default:
                        return key;
                }
            }            
            else if (typeof(Character) == t)
            {
                switch (key)
                {
                    case "stats": return "Statistics";
                    default:
                        return key;
                }
            }
            else
            {
                return key;
            }
        }
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get {
                return new ReadOnlyCollection<Type>(new List<Type>(
                    new Type[] {
                        typeof(Character),
                        typeof(GuildInfo),
                        typeof(Emblem),
                        typeof(Standing),
                        typeof(Reputation),
                        typeof(Progression),
                        typeof(RaidProgression),
                        typeof(BossProgression),
                        typeof(Pet),
                        typeof(Professions),
                        typeof(Profession),
                        typeof(Appearance),                        
                        typeof(Stats),
                        typeof(Talent),
                        typeof(Tree),
                        typeof(Glyphs),
                        typeof(CharacterItems),
                        typeof(Title),
                        typeof(ItemToolTip),   
                     
                        typeof(ClassCollection),
                        typeof(Class),
                        typeof(PowerType),
                    }));
            }
        }
    }
}
