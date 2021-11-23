namespace WebApiWithGenerics.WebApi.Services
{
    using System;
    using System.Data;

    using Dapper;

    public class SqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }
    }
}
