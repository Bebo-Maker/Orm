﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Otis.Reflection
{
  public delegate object GetValueDelegate(object instance);
  public delegate void SetValueDelegate(object instance, object value);

  /// <summary>
  /// Custom <see cref="PropertyInfo"/> that wraps an existing property and provides
  /// <c>Reflection.Emit</c>-generated <see cref="GetValue"/> and <see cref="SetValue"/>
  /// implementations for drastically improved performance over default late-bind
  /// invoke.
  /// http://www.codeproject.com/csharp/Fast_Dynamic_Properties.asp
  /// </summary>
  public class DelegateFactory
  {
    private static readonly Dictionary<Type, OpCode> TypeILCodeCache = new()
    {
      [typeof(sbyte)] = OpCodes.Ldind_I1,
      [typeof(byte)] = OpCodes.Ldind_U1,
      [typeof(char)] = OpCodes.Ldind_U2,
      [typeof(short)] = OpCodes.Ldind_I2,
      [typeof(ushort)] = OpCodes.Ldind_U2,
      [typeof(int)] = OpCodes.Ldind_I4,
      [typeof(uint)] = OpCodes.Ldind_U4,
      [typeof(long)] = OpCodes.Ldind_I8,
      [typeof(ulong)] = OpCodes.Ldind_I8,
      [typeof(bool)] = OpCodes.Ldind_I1,
      [typeof(double)] = OpCodes.Ldind_R8,
      [typeof(float)] = OpCodes.Ldind_R4
    };

    public GetValueDelegate CreatePropertyGetter(PropertyInfo property)
    {
      if (!property.CanRead)
        throw new InvalidOperationException($"Property {property} has to be readable.");

      var dm = new DynamicMethod("GetValueImpl", typeof(object), new Type[] { typeof(object) }, GetType().Module, false);
      var ilgen = dm.GetILGenerator();

      //.locals init (
      //      object obj1)
      LocalBuilder result = ilgen.DeclareLocal(typeof(object));
      //L_0000: nop
      ilgen.Emit(OpCodes.Nop);
      //L_0001: ldarg.0
      ilgen.Emit(OpCodes.Ldarg_0);
      //L_0002: castclass [declaringType]
      ilgen.Emit(OpCodes.Castclass, property.DeclaringType);
      //L_0007: callvirt instance [declaringType] get_[B]()
      ilgen.EmitCall(OpCodes.Callvirt, property.GetGetMethod(), null);

      //Box if necessary:Yiyi
      if (property.PropertyType.IsValueType)
        ilgen.Emit(OpCodes.Box, property.PropertyType);

      //L_000c: stloc.0
      ilgen.Emit(OpCodes.Stloc_0, result);
      //L_000f: ldloc.0
      ilgen.Emit(OpCodes.Ldloc_0);
      //L_0010: ret
      ilgen.Emit(OpCodes.Ret);

      return (GetValueDelegate)dm.CreateDelegate(typeof(GetValueDelegate));
    }

    public SetValueDelegate CreatePropertySetter(PropertyInfo property)
    {
      if (!property.CanWrite)
        throw new InvalidOperationException($"Property {property} has to be writable.");

      var dm = new DynamicMethod("SetValueImpl", null, new Type[] { typeof(object), typeof(object) }, GetType().Module, false);
      var ilgen = dm.GetILGenerator();

      //L_0000: nop
      ilgen.Emit(OpCodes.Nop);
      //L_0001: ldarg.0
      ilgen.Emit(OpCodes.Ldarg_0);
      //L_0002: castclass [declaringType]
      ilgen.Emit(OpCodes.Castclass, property.DeclaringType);
      //L_0007: ldarg.1
      ilgen.Emit(OpCodes.Ldarg_1);

      //UnBox if necessary:Yiyi
      if (property.PropertyType.IsValueType)
      {
        ilgen.Emit(OpCodes.Unbox, property.PropertyType); //Unbox it

        if (TypeILCodeCache.ContainsKey(property.PropertyType)) //and load
        {
          OpCode load = TypeILCodeCache[property.PropertyType];
          ilgen.Emit(load);
        }
        else
        {
          ilgen.Emit(OpCodes.Ldobj, property.PropertyType);
        }
      }
      else
      {
        //L_0008: castclass [propertyType]
        ilgen.Emit(OpCodes.Castclass, property.PropertyType);
      }

      //L_000d: callvirt instance void [instanceType]::set_[propertyName](propertyType)
      ilgen.EmitCall(OpCodes.Callvirt, property.GetSetMethod(), null);
      //L_0012: nop
      ilgen.Emit(OpCodes.Nop);
      //L_0013: ret
      ilgen.Emit(OpCodes.Ret);

      return (SetValueDelegate)dm.CreateDelegate(typeof(SetValueDelegate));
    }
  }
}
