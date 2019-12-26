using System;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace cat.DB {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DefaultValueAttribute : Attribute {
        public object DefaultValue { get; set; }
    }

    public class DefaultValueAttributeConvention: PrimitivePropertyAttributeConfigurationConvention<DefaultValueAttribute> {
        public override void Apply(ConventionPrimitivePropertyConfiguration configuration, DefaultValueAttribute attribute) {
            configuration.HasColumnAnnotation("DefaultValue", attribute.DefaultValue);
        }
    }

}
