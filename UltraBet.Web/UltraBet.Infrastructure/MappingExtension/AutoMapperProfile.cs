﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Common.Mapping;
namespace UltraBet.Infrastructure.MappingExtension
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            var allTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.GetName()
                                    .Name
                                    .Contains("UltraBet"))
                .SelectMany(a => a.GetTypes());

            var mappings = allTypes
                .Where(t => t.IsClass
                         && !t.IsAbstract
                         && t.GetInterfaces()
                                  .Where(i => i.IsGenericType)
                                  .Select(i => i.GetGenericTypeDefinition())
                                  .Contains(typeof(IMapFrom<>)))
                .Select(t => new
                {
                    Destination = t,
                    Source = t
                           .GetInterfaces()
                           .Where(i => i.IsGenericType)
                           .Select(i => new
                           {
                               Definition = i.GetGenericTypeDefinition(),
                               Arguments = i.GetGenericArguments()
                           })
                           .Where(i => i.Definition == typeof(IMapFrom<>))
                           .SelectMany(i => i.Arguments)
                           .First()
                })
                .ToList();

            foreach (var mapping in mappings)
            {
                this.CreateMap(mapping.Source, mapping.Destination);
            }

            var customMappings = allTypes
                  .Where(t => t.IsClass
                           && !t.IsAbstract
                           && typeof(ICustomMapperProfile).IsAssignableFrom(t))
                  .Select(Activator.CreateInstance)
                  .Cast<ICustomMapperProfile>()
                  .ToList();

            foreach (var custmap in customMappings)
            {
                custmap.ConfigureMapping(this);
            }
        }
    }
}